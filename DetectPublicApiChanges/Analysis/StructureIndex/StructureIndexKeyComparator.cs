using System.Collections.Generic;
using System.Linq;
using DetectPublicApiChanges.Interfaces;
using log4net;

namespace DetectPublicApiChanges.Analysis.StructureIndex
{
    /// <summary>
    /// The StructureIndexKeyComparator uses simple index kex comparison to get all index items which might have changed or got deleted.
    /// </summary>
    /// <seealso cref="IStructureIndexComparator" />
    public class StructureIndexKeyComparator : IStructureIndexComparator
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
        /// Initializes a new instance of the <see cref="StructureIndexKeyComparator" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="modifierDetectors">The modifier detectors.</param>
        public StructureIndexKeyComparator(ILog logger, IEnumerable<IPublicModifierDetector> modifierDetectors)
        {
            _logger = logger;
            _modifierDetectors = modifierDetectors;
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

            foreach (var sourceIndexItem in sourceIndex.Items)
            {
                //Filter non public structures
                if (!_modifierDetectors.Any(m => m.IsPublic(sourceIndexItem.Value.SyntaxNode)))
                {
                    _logger.Warn($"Index item '{sourceIndexItem.Key}' of type '{sourceIndexItem.Value.SyntaxNode.GetType()} is not public')");
                    continue;
                }

                if (!targetIndex.Exists(sourceIndexItem.Key))
                {
                    differences.Add(sourceIndexItem.Value);
                    _logger.Warn($"The index item with key '{sourceIndexItem.Key}' of type '{sourceIndexItem.Value.SyntaxNode.GetType()}' was not found");
                }
            }

            return new StructureIndexComparisonResult(differences);
        }
    }
}
