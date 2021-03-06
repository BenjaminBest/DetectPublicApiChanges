﻿using System.Collections.Generic;
using DetectPublicApiChanges.Analysis.PublicMemberDetection;
using DetectPublicApiChanges.Analysis.StructureIndex;
using DetectPublicApiChanges.Analysis.SyntaxNodeAnalyzers;
using DetectPublicApiChanges.Common;
using DetectPublicApiChanges.Interfaces;
using DetectPublicApiChanges.Jobs;
using DetectPublicApiChanges.Report;
using DetectPublicApiChanges.Report.Mvc;
using DetectPublicApiChanges.SourceControl.Common;
using DetectPublicApiChanges.SourceControl.Git;
using DetectPublicApiChanges.SourceControl.Interfaces;
using DetectPublicApiChanges.SourceControl.Subversion;
using DetectPublicApiChanges.Steps;
using log4net;
using Ninject.Modules;

namespace DetectPublicApiChanges
{
    public class NinjectBindings : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            //Common
            Bind<IOptions>().To<Options>().InSingletonScope();
            Bind<IFileService>().To<FileService>();
            Bind<IJobRegistry>().To<JobRegistry>().InSingletonScope();

            //Source control
            Bind<ISourceControlClient>().To<SubversionSourceControlClient>();
            Bind<ISourceControlClient>().To<GitSourceControlClient>();
            Bind<ISourceControlFactory>().To<SourceControlFactory>();

            Bind<ILog>().ToMethod(context =>
            {
                if (context.Request?.ParentContext == null)
                    return LogManager.GetLogger(typeof(Program));

                return LogManager.GetLogger(context.Request.ParentContext.Plan.Type);
            });

            Bind<IStore>().To<Store>().InSingletonScope();
            Bind<IFileSerializer>().To<NetJsonSerializer>();

            //Rendering
            Bind<IRenderer>().To<RazorEngineRenderer>().InSingletonScope();
            Bind<IReportViewModelCreator>().To<BreakingChangesReportViewModelCreator>().WhenInjectedInto<BreakingChangesReportCreationStep>();
            Bind<IReportViewModelCreator>().To<ObsoletesReportViewModelCreator>().WhenInjectedInto<ObsoletesReportCreationStep>();

            //Public modifier detection
            Bind<IPublicModifierDetector>().To<PublicClassModifierDetector>();
            Bind<IPublicModifierDetector>().To<PublicInterfaceModifierDetector>();
            Bind<IPublicModifierDetector>().To<PublicConstructorModifierDetector>();
            Bind<IPublicModifierDetector>().To<PublicMethodModifierDetector>();
            Bind<IPublicModifierDetector>().To<PublicPropertyModifierDetector>();
            Bind<IPublicModifierDetector>().To<PublicStructModifierDetector>();

            //SyntaxNode analyzer
            Bind<ISyntaxNodeAnalyzer>().To<ClassSyntaxNodeAnalyzer>();
            Bind<ISyntaxNodeAnalyzer>().To<GenericClassSyntaxNodeAnalyzer>();
            Bind<ISyntaxNodeAnalyzer>().To<InterfaceSyntaxNodeAnalyzer>();
            Bind<ISyntaxNodeAnalyzer>().To<GenericInterfaceSyntaxNodeAnalyzer>();
            Bind<ISyntaxNodeAnalyzer>().To<PropertySyntaxNodeAnalyzer>();
            Bind<ISyntaxNodeAnalyzer>().To<MethodSyntaxNodeAnalyzer>();
            Bind<ISyntaxNodeAnalyzer>().To<ConstructorSyntaxNodeAnalyzer>();
            Bind<ISyntaxNodeAnalyzer>().To<StaticConstructorSyntaxNodeAnalyzer>();
            Bind<ISyntaxNodeAnalyzer>().To<PartialClassSyntaxNodeAnalyzer>();
            Bind<ISyntaxNodeAnalyzer>().To<StructSyntaxNodeAnalyzer>();
            Bind<ISyntaxNodeAnalyzer>().To<GenericStructSyntaxNodeAnalyzer>();
            Bind<ISyntaxNodeAnalyzerRepository>().To<SyntaxNodeAnalyzerRepository>();
            Bind<ISyntaxNodeAnalyzer>().To<StaticMethodSyntaxNodeAnalyzer>();

            //Indexing
            Bind<IStructureIndexComparator>().To<StructureIndexComparator>();
            Bind<IStructureIndex>().To<StructureIndex>();
            Bind<IIndexItemFactory>().To<IndexItemFactory>().InSingletonScope();
            Bind<IStructureIndexSourceItemComparator>().To<ItemSourceKeyComparator>().InSingletonScope();
            Bind<IStructureIndexTargetItemComparator>().To<ItemTargetInterfacePropertyComparator>().InSingletonScope();
            Bind<IStructureIndexTargetItemComparator>().To<ItemTargetInterfaceMethodComparator>().InSingletonScope();

            //Jobs & Steps
            Bind<IJob>().To<DetectChangesJob>().WithConstructorArgument("steps", new List<IStep>
            {
                (InitializationStep) Kernel.GetService(typeof(InitializationStep)),
                (RepositoryCheckoutStep) Kernel.GetService(typeof(RepositoryCheckoutStep)),
                (AnalyzeDocumentTreeStep) Kernel.GetService(typeof(AnalyzeDocumentTreeStep)),
                (IndexComparisonStep) Kernel.GetService(typeof(IndexComparisonStep)),
                (BreakingChangesReportCreationStep) Kernel.GetService(typeof(BreakingChangesReportCreationStep)),
                (CleanupStep) Kernel.GetService(typeof(CleanupStep))
            });

            Bind<IJob>().To<DetectObsoletesJob>().WithConstructorArgument("steps", new List<IStep>
            {
                (InitializationStep) Kernel.GetService(typeof(InitializationStep)),
                (RepositoryCheckoutStep) Kernel.GetService(typeof(RepositoryCheckoutStep)),
                (AnalyzeDocumentTreeStep) Kernel.GetService(typeof(AnalyzeDocumentTreeStep)),
                (IndexComparisonStep) Kernel.GetService(typeof(IndexComparisonStep)),
                (BreakingChangesReportCreationStep) Kernel.GetService(typeof(BreakingChangesReportCreationStep)),
                (ObsoletesReportCreationStep) Kernel.GetService(typeof(ObsoletesReportCreationStep)),
                (CleanupStep) Kernel.GetService(typeof(CleanupStep))
            });
        }
    }
}