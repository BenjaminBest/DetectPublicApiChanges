using System.Collections.Generic;
using System.IO;
using DetectPublicApiChanges.Analysis.Roslyn;
using DetectPublicApiChanges.Analysis.Structures;
using DetectPublicApiChanges.Common;
using DetectPublicApiChanges.Extensions;
using DetectPublicApiChanges.Interfaces;
using log4net;
using Microsoft.CodeAnalysis;

namespace DetectPublicApiChanges.Steps
{
    /// <summary>
    /// Step for analyzing projects
    /// </summary>
    /// <seealso cref="Common.StepBase{AnalyzeProjectsStep}" />
    /// <seealso cref="IStep" />
    public class AnalyzeDocumentTreeStep : StepBase<AnalyzeDocumentTreeStep>, IStep
    {
        /// <summary>
        /// The store
        /// </summary>
        private readonly IStore _store;

        /// <summary>
        /// The options
        /// </summary>
        private readonly IOptions _options;

        /// <summary>
        /// The class root type structure builder
        /// </summary>
        private readonly IRootTypeStructureBuilder<SyntaxTree, ClassStructure> _classRootTypeStructureBuilder;

        /// <summary>
        /// The interface root type structure builder
        /// </summary>
        private readonly IRootTypeStructureBuilder<SyntaxTree, InterfaceStructure> _interfaceRootTypeStructureBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyzeDocumentTreeStep" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="store">The store.</param>
        /// <param name="options">The options.</param>
        /// <param name="classRootTypeStructureBuilder">The class root type structure builder.</param>
        /// <param name="interfaceRootTypeStructureBuilder">The interface root type structure builder.</param>
        public AnalyzeDocumentTreeStep(
            ILog logger,
            IStore store,
            IOptions options,
            IRootTypeStructureBuilder<SyntaxTree, ClassStructure> classRootTypeStructureBuilder,
            IRootTypeStructureBuilder<SyntaxTree, InterfaceStructure> interfaceRootTypeStructureBuilder)
            : base(logger)
        {
            _store = store;
            _options = options;
            _classRootTypeStructureBuilder = classRootTypeStructureBuilder;
            _interfaceRootTypeStructureBuilder = interfaceRootTypeStructureBuilder;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            ExecuteSafe(() =>
            {
                var solutions = new List<SolutionStructure>
                {
                    GetSolution(_store.GetItem<string>(StoreKeys.SolutionPathSource)),
                    GetSolution(_store.GetItem<string>(StoreKeys.SolutionPathTarget))
                };

                _store.SetOrAddItem(StoreKeys.Solutions, solutions);
            });
        }

        /// <summary>
        /// Gets the solution.
        /// </summary>
        /// <param name="solutionPath">The solution path.</param>
        /// <returns></returns>
        private SolutionStructure GetSolution(string solutionPath)
        {
            var solution = WorkspaceHelper.GetSolution(solutionPath);
            var projects = WorkspaceHelper.GetProjects(solutionPath);
            projects.Wait();
            solution.Wait();


            //Create solution
            var solutionInfo = new SolutionStructure(GetSolutionName(solution.Result.FilePath), solutionPath).Log();

            foreach (var projectId in solution.Result.ProjectIds)
            {
                var projectInfo = solution.Result.GetProject(projectId);

                //Add project
                var project = new ProjectStructure(projectInfo.Name).Log();
                solutionInfo.AddProject(project);

                foreach (var documentId in projectInfo.DocumentIds)
                {
                    var document = solution.Result.GetDocument(documentId);
                    if (document.SupportsSyntaxTree)
                    {
                        //Syntax Tree
                        var syntaxTree = document.GetSyntaxTreeAsync().Result;

                        //Classes
                        project.AddClasses(_classRootTypeStructureBuilder.Build(syntaxTree));

                        //Interfaces
                        project.AddInterfaces(_interfaceRootTypeStructureBuilder.Build(syntaxTree));
                    }
                }
            }

            return solutionInfo;
        }

        /// <summary>
        /// Gets the name of the solution.
        /// </summary>
        /// <param name="solutionPath">The solution path.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        private static string GetSolutionName(string solutionPath)
        {
            var file = new FileInfo(solutionPath);

            if (!file.Exists)
                throw new FileNotFoundException($"The solution file {solutionPath} does not exits");

            return file.Name;
        }
    }
}