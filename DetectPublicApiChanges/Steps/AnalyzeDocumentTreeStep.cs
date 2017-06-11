using System.Collections.Generic;
using DetectPublicApiChanges.Analysis.Filters;
using DetectPublicApiChanges.Analysis.Roslyn;
using DetectPublicApiChanges.Analysis.StructureIndex;
using DetectPublicApiChanges.Common;
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
        /// The logger
        /// </summary>
        private readonly ILog _logger;

        /// <summary>
        /// The store
        /// </summary>
        private readonly IStore _store;

        /// <summary>
        /// The options
        /// </summary>
        private readonly IOptions _options;

        /// <summary>
        /// The syntax node analyzers
        /// </summary>
        private readonly ISyntaxNodeAnalyzerRepository _syntaxNodeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyzeDocumentTreeStep" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="store">The store.</param>
        /// <param name="options">The options.</param>
        /// <param name="syntaxNodeRepository">The syntax node repository.</param>
        public AnalyzeDocumentTreeStep(
            ILog logger,
            IStore store,
            IOptions options,
            ISyntaxNodeAnalyzerRepository syntaxNodeRepository)
            : base(logger)
        {
            _logger = logger;
            _store = store;
            _options = options;
            _syntaxNodeRepository = syntaxNodeRepository;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            ExecuteSafe(() =>
            {
                var sourceStructureIndex = new StructureIndex(_logger);
                var targetStructureIndex = new StructureIndex(_logger);

                var solutions = new List<Solution>
                {
                    GetSolutionAndCreateIndex(_store.GetItem<string>(StoreKeys.SolutionPathSource),sourceStructureIndex),
                    GetSolutionAndCreateIndex(_store.GetItem<string>(StoreKeys.SolutionPathTarget),targetStructureIndex)
                };

                _store.SetOrAddItem(StoreKeys.Solutions, solutions);
                _store.SetOrAddItem(StoreKeys.SolutionIndexSource, sourceStructureIndex);
                _store.SetOrAddItem(StoreKeys.SolutionIndexTarget, targetStructureIndex);
            });
        }

        /// <summary>
        /// Gets the solution syntax tree and creates the index
        /// </summary>
        /// <param name="solutionPath">The solution path.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private Solution GetSolutionAndCreateIndex(string solutionPath, IStructureIndex index)
        {
            var solution = WorkspaceHelper.GetSolution(solutionPath);
            var projects = WorkspaceHelper.GetProjects(solutionPath);
            projects.Wait();
            solution.Wait();

            solution.Result.Log();

            foreach (var project in solution.Result.Projects.Filter(_options.RegexFilter))
            {
                _logger.Info($"Analyzing project '{project.Name}'");

                project.Log();

                foreach (var documentId in project.DocumentIds)
                {
                    var document = solution.Result.GetDocument(documentId);
                    if (document.SupportsSyntaxTree)
                    {
                        //Syntax Tree
                        var syntaxTree = document.GetSyntaxTreeAsync().Result;

                        var syntaxNode = syntaxTree?.GetRoot();

                        if (syntaxNode == null)
                            continue;

                        var items = syntaxNode.DescendantNodesAndSelf();

                        foreach (var item in items)
                        {
                            if (_syntaxNodeRepository.IsSyntaxDeclarationTypeSupported(item))
                            {
                                //TODO: Project should not be writable, should be automatically determined
                                var indexItem = _syntaxNodeRepository.Analyze(item);
                                indexItem.Project = project;

                                index.AddOrUpdateItem(indexItem);
                            }
                        }
                    }
                }
            }

            _logger.Info($"Index now has {index.Items.Count} items");

            return solution.Result;
        }
    }
}