using DetectPublicApiChanges.Common;
using DetectPublicApiChanges.Interfaces;
using log4net;

namespace DetectPublicApiChanges.Steps
{
    /// <summary>
    /// Step for persisting the annalyzed results
    /// </summary>
    /// <seealso cref="Common.StepBase{IndexComparisonStep}" />
    /// <seealso cref="IStep" />
    public class IndexComparisonStep : StepBase<IndexComparisonStep>, IStep
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
        /// The structure index comparator
        /// </summary>
        private readonly IStructureIndexComparator _structureIndexComparator;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexComparisonStep" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="store">The store.</param>
        /// <param name="structureIndexComparator">The structure index comparator.</param>
        public IndexComparisonStep(
            ILog logger,
            IStore store,
            IStructureIndexComparator structureIndexComparator)
            : base(logger)
        {
            _logger = logger;
            _store = store;
            _structureIndexComparator = structureIndexComparator;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            ExecuteSafe(() =>
            {
                var sourceIndex = _store.GetItem<IStructureIndex>(StoreKeys.SolutionIndexSource);
                var targetIndex = _store.GetItem<IStructureIndex>(StoreKeys.SolutionIndexTarget);

                _logger.Info($"Compare source index [{sourceIndex.Items.Count} items] vs target index {targetIndex.Items.Count} items]");

                var result = _structureIndexComparator.Compare(sourceIndex, targetIndex);
                _store.SetOrAddItem(StoreKeys.IndexComparisonResult, result);

                _logger.Info(result.HasDifferences
                    ? $"Found {result.Differences} in index"
                    : "Found no differences between the indexes");
            });
        }
    }
}