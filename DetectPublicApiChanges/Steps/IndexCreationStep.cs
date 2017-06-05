using System.Collections.Generic;
using System.Linq;
using DetectPublicApiChanges.Analysis.Structures;
using DetectPublicApiChanges.Common;
using DetectPublicApiChanges.Interfaces;
using log4net;

namespace DetectPublicApiChanges.Steps
{
    /// <summary>
    /// Step for persisting the annalyzed results
    /// </summary>
    /// <seealso cref="Common.StepBase{IndexCreationStep}" />
    /// <seealso cref="IStep" />
    public class IndexCreationStep : StepBase<IndexCreationStep>, IStep
    {
        private readonly ILog _logger;
        private readonly IStore _store;
        private readonly IStructureIndex _sourceStructureIndex;
        private readonly IStructureIndex _targetStructureIndex;
        private readonly IStructureToIndexItemConverter<ClassStructure> _classConverter;
        private readonly IStructureToIndexItemConverter<InterfaceStructure> _interfaceConverter;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexCreationStep" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="store">The store.</param>
        /// <param name="sourceStructureIndex">Index of the source structure.</param>
        /// <param name="targetStructureIndex">Index of the target structure.</param>
        /// <param name="classConverter">The class converter.</param>
        /// <param name="interfaceConverter">The interface converter.</param>
        public IndexCreationStep(
            ILog logger,
            IStore store,
            IStructureIndex sourceStructureIndex,
            IStructureIndex targetStructureIndex,
            IStructureToIndexItemConverter<ClassStructure> classConverter,
            IStructureToIndexItemConverter<InterfaceStructure> interfaceConverter)
            : base(logger)
        {
            _logger = logger;
            _store = store;
            _sourceStructureIndex = sourceStructureIndex;
            _targetStructureIndex = targetStructureIndex;
            _classConverter = classConverter;
            _interfaceConverter = interfaceConverter;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            ExecuteSafe(() =>
            {
                var solutions = _store.GetItem<List<SolutionStructure>>(StoreKeys.Solutions).ToList();

                _logger.Info($"Analyzing '{solutions.Count}' solutions");

                //Add first solution
                AddStructuresToIndex(solutions[0], _sourceStructureIndex);
                _store.SetOrAddItem(StoreKeys.SolutionIndexSource, _sourceStructureIndex);

                //Add second solution
                AddStructuresToIndex(solutions[1], _targetStructureIndex);
                _store.SetOrAddItem(StoreKeys.SolutionIndexTarget, _targetStructureIndex);
            });
        }

        /// <summary>
        /// Adds the index of the structures to.
        /// </summary>
        /// <param name="solution">The solution.</param>
        /// <param name="index">The index.</param>
        private void AddStructuresToIndex(SolutionStructure solution, IStructureIndex index)
        {
            foreach (var project in solution.Projects)
            {
                _logger.Info($"Analyzing project '{project.Name}'");

                //Classes
                foreach (var item in project.Classes.SelectMany(c => _classConverter.Convert(c, project)))
                {
                    index.AddOrUpdateItem(item);
                }

                //Interfaces
                foreach (var item in project.Interfaces.SelectMany(c => _interfaceConverter.Convert(c, project)))
                {
                    index.AddOrUpdateItem(item);
                }

                _logger.Info($"Index now has {index.Items.Count} items");
            }
        }
    }
}