using System.Collections.Generic;
using System.Linq;
using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Analysis.StructureIndex
{
    /// <summary>
    /// StructureIndexComparisonResult is a result of an structure index comparison
    /// </summary>
    /// <seealso cref="IStructureIndexComparisonResult" />
    public class StructureIndexComparisonResult : IStructureIndexComparisonResult
    {
        /// <summary>
        /// Gets the amount of differences.
        /// </summary>
        /// <value>
        /// The differences.
        /// </value>
        public int AmountOfDifferences { get; }

        /// <summary>
        /// Gets a value indicating whether the indexes have diferences.
        /// </summary>
        /// <value>
        /// <c>true</c> if the indexes have diferences; otherwise, <c>false</c>.
        /// </value>
        public bool HasDifferences => AmountOfDifferences != 0;

        /// <summary>
        /// Gets the differences.
        /// </summary>
        /// <value>
        /// The differences.
        /// </value>
        public IReadOnlyList<IIndexItem> Differences { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureIndexComparisonResult" /> class.
        /// </summary>
        /// <param name="differences">The differences.</param>
        public StructureIndexComparisonResult(IEnumerable<IIndexItem> differences)
        {
            Differences = differences.ToList();
            AmountOfDifferences = Differences.Count;
        }
    }
}
