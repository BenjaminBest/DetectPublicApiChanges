using System.Collections.Generic;
using DetectPublicApiChanges.Analysis.BreakingChangeDetection;
using DetectPublicApiChanges.Analysis.StructureBuilders;
using DetectPublicApiChanges.Analysis.StructureIndexers;
using DetectPublicApiChanges.Analysis.Structures;
using DetectPublicApiChanges.Common;
using DetectPublicApiChanges.Interfaces;
using DetectPublicApiChanges.Jobs;
using DetectPublicApiChanges.Report;
using DetectPublicApiChanges.Report.Mvc;
using DetectPublicApiChanges.Steps;
using log4net;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            Bind<ISourceControlClient>().To<SubversionSourceControlClient>();

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
            Bind<IReportViewModelCreator>().To<ReportViewModelCreator>();

            //Structure Builders
            Bind<IRootTypeStructureBuilder<SyntaxTree, ClassStructure>>().To<ClassStructureBuilder>();
            Bind<IRootTypeStructureBuilder<SyntaxTree, InterfaceStructure>>().To<InterfaceStructureBuilder>();
            Bind<ITypeStructureBuilder<ClassDeclarationSyntax, ConstructorStructure>>().To<ConstructorStructureBuilder>();
            Bind<ITypeStructureBuilder<ClassDeclarationSyntax, MethodStructure>>().To<MethodClassStructureBuilder>();
            Bind<ITypeStructureBuilder<InterfaceDeclarationSyntax, MethodStructure>>().To<MethodInterfaceStructureBuilder>();
            Bind<ITypeStructureBuilder<ClassDeclarationSyntax, ModifierStructure>>().To<ModifierClassStructureBuilder>();
            Bind<IBaseMethodStructureBuilder<ConstructorDeclarationSyntax, ModifierStructure>>().To<ModifierConstructorStructureBuilder>();
            Bind<ITypeStructureBuilder<InterfaceDeclarationSyntax, ModifierStructure>>().To<ModifierInterfaceStructureBuilder>();
            Bind<IBaseMethodStructureBuilder<MethodDeclarationSyntax, ModifierStructure>>().To<ModifierMethodStructureBuilder>();
            Bind<IBaseMethodStructureBuilder<ConstructorDeclarationSyntax, ParameterStructure>>().To<ParameterConstructorStructureBuilder>();
            Bind<IBaseMethodStructureBuilder<MethodDeclarationSyntax, ParameterStructure>>().To<ParameterMethodStructureBuilder>();
            Bind<ITypeStructureBuilder<ClassDeclarationSyntax, PropertyStructure>>().To<PropertyClassStructureBuilder>();
            Bind<ITypeStructureBuilder<InterfaceDeclarationSyntax, PropertyStructure>>().To<PropertyInterfaceStructureBuilder>();
            Bind<IBasePropertyStructureBuilder<PropertyDeclarationSyntax, ModifierStructure>>().To<ModifierPropertyStructureBuilder>();

            //Breaking change detection
            Bind<IPublicModifierDetector>().To<PublicClassModifierDetector>();
            Bind<IPublicModifierDetector>().To<PublicInterfaceModifierDetector>();
            Bind<IPublicModifierDetector>().To<PublicConstructorModifierDetector>();
            Bind<IPublicModifierDetector>().To<PublicMethodModifierDetector>();
            Bind<IPublicModifierDetector>().To<PublicPropertyModifierDetector>();

            //Indexing
            Bind<IStructureToIndexItemConverter<ClassStructure>>().To<ClassStructureToIndexItemConverter>();
            Bind<IStructureToIndexItemConverter<PropertyStructure>>().To<PropertyStructureToIndexItemConverter>();
            Bind<IStructureToIndexItemConverter<ConstructorStructure>>().To<ConstructorStructureToIndexItemConverter>();
            Bind<IStructureToIndexItemConverter<MethodStructure>>().To<MethodStructureToIndexItemConverter>();
            Bind<IStructureToIndexItemConverter<InterfaceStructure>>().To<InterfaceStructureToIndexItemConverter>();
            Bind<IStructureIndexComparator>().To<StructureIndexKeyComparator>();
            Bind<IStructureIndex>().To<StructureIndex>();

            //Jobs & Steps
            Bind<IJob>().To<DetectChangesJob>().WithConstructorArgument("steps", new List<IStep>
            {
                (InitializationStep) Kernel.GetService(typeof(InitializationStep)),
                (RepositoryCheckoutStep) Kernel.GetService(typeof(RepositoryCheckoutStep)),
                (AnalyzeDocumentTreeStep) Kernel.GetService(typeof(AnalyzeDocumentTreeStep)),
                (PersistSolutionTreeStep) Kernel.GetService(typeof(PersistSolutionTreeStep)),
                (IndexCreationStep) Kernel.GetService(typeof(IndexCreationStep)),
                (IndexComparisonStep) Kernel.GetService(typeof(IndexComparisonStep)),
                (ReportCreationStep) Kernel.GetService(typeof(ReportCreationStep)),
                (CleanupStep) Kernel.GetService(typeof(CleanupStep))
            });
        }
    }
}