using System.Collections.Generic;
using System.Linq;
using DetectPublicApiChanges.Interfaces;
using log4net;

namespace DetectPublicApiChanges.Analysis.StructureIndex
{
    /// <summary>
    /// The StructureIndexComparator uses a simple comparison method to get all index items which might have changed or got deleted.
    /// </summary>
    /// <seealso cref="IStructureIndexComparator" />
    public class StructureIndexComparator : IStructureIndexComparator
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILog _logger;

        /// <summary>
        /// The modifier detectors
        /// </summary>
        private readonly IEnumerable<IPublicModifierDetector> _modifierDetectors;

        /// <summary>
        /// The source comparators
        /// </summary>
        private readonly IEnumerable<IStructureIndexSourceItemComparator> _sourceComparators;

        /// <summary>
        /// The target comparators
        /// </summary>
        private readonly IEnumerable<IStructureIndexTargetItemComparator> _targetComparators;

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureIndexComparator" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="modifierDetectors">The modifier detectors.</param>
        /// <param name="sourceComparators">The source comparators</param>
        /// <param name="targetComparators">The target comparators</param>
        public StructureIndexComparator(ILog logger,
            IEnumerable<IPublicModifierDetector> modifierDetectors,
            IEnumerable<IStructureIndexSourceItemComparator> sourceComparators,
            IEnumerable<IStructureIndexTargetItemComparator> targetComparators)
        {
            _logger = logger;
            _modifierDetectors = modifierDetectors;
            _sourceComparators = sourceComparators;
            _targetComparators = targetComparators;
        }

        /// <summary>
        /// Compares the specified <paramref name="sourceIndex" /> index with the given <paramref name="targetIndex" /> index
        /// </summary>
        /// <param name="sourceIndex">The source index.</param>
        /// <param name="targetIndex">The target index.</param>
        /// <returns>
        /// Result of the comparison
        /// </returns>
        public IStructureIndexComparisonResult Compare(IStructureIndex sourceIndex, IStructureIndex targetIndex)
        {
            var differences = new List<IIndexItem>();

            //Source vs target
            foreach (var sourceIndexItem in sourceIndex.Items)
            {
                //Filter non public structures
                if (!_modifierDetectors.Any(m => m.IsPublic(sourceIndexItem.Value.SyntaxNode)))
                {
                    _logger.Warn($"Index item '{sourceIndexItem.Key}' of type '{sourceIndexItem.Value.SyntaxNode.GetType()} is not public')");
                    continue;
                }

                if (_sourceComparators.Any(s => s.HasBreakingChanges(sourceIndexItem.Value, targetIndex.Exists(sourceIndexItem.Key) ? targetIndex.Items[sourceIndexItem.Key] : null)))
                {
                    differences.Add(sourceIndexItem.Value);
                    _logger.Warn($"The index item with key '{sourceIndexItem.Key}' of type '{sourceIndexItem.Value.SyntaxNode.GetType()}' was not found");
                }
            }

            //Target vs source
            foreach (var targetIndexItem in targetIndex.Items)
            {
                //Filter non public structures
                if (!_modifierDetectors.Any(m => m.IsPublic(targetIndexItem.Value.SyntaxNode)))
                {
                    _logger.Warn($"Index item '{targetIndexItem.Key}' of type '{targetIndexItem.Value.SyntaxNode.GetType()} is not public')");
                    continue;
                }

                if (_targetComparators.Any(s => s.HasBreakingChanges(targetIndexItem.Value, sourceIndex.Exists(targetIndexItem.Key) ? sourceIndex.Items[targetIndexItem.Key] : null)))
                {
                    differences.Add(targetIndexItem.Value);
                    _logger.Warn($"The index item with key '{targetIndexItem.Key}' of type '{targetIndexItem.Value.SyntaxNode.GetType()}' was not found");
                }
            }

            return new StructureIndexComparisonResult(differences);
        }
    }
}
