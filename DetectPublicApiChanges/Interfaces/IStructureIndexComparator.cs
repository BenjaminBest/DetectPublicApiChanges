namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The interface IStructureIndexComparator defined a comparator which compares two indexes
    /// </summary>
    public interface IStructureIndexComparator
    {
        /// <summary>
        /// Compares the specified <paramref name="sourceIndex" /> index with the given <paramref name="targetIndex" /> index
        /// </summary>
        /// <param name="sourceIndex">Index of the source.</param>
        /// <param name="targetIndex">Index of the target.</param>
        /// <returns>Result of the comparison</returns>
        IStructureIndexComparisonResult Compare(IStructureIndex sourceIndex, IStructureIndex targetIndex);
    }
}