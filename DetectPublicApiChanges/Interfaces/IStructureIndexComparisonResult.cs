using System.Collections.Generic;

namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The interface IStructureIndexComparisonResult defines a result of an index comparison
    /// </summary>
    public interface IStructureIndexComparisonResult
    {
        /// <summary>
        /// Gets the amount of differences.
        /// </summary>
        /// <value>
        /// The differences.
        /// </value>
        int AmountOfDifferences { get; }

        /// <summary>
        /// Gets a value indicating whether the indexes have diferences.
        /// </summary>
        /// <value>
        /// <c>true</c> if the indexes have diferences; otherwise, <c>false</c>.
        /// </value>
        bool HasDifferences { get; }

        /// <summary>
        /// Gets the differences.
        /// </summary>
        /// <value>
        /// The differences.
        /// </value>
        IReadOnlyList<IStructureIndexItem> Differences { get; }
    }
}
