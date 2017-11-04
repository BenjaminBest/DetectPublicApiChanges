namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The interface IStructureIndexSourceItemComparator defines a class which is used to compare index items and used a specific method for doing that.
    /// </summary>
    public interface IStructureIndexSourceItemComparator
    {
        /// <summary>
        /// Determines whether there are breaking changes between the <paramref name="source"/> and the <paramref name="target"/>
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <returns>
        ///   <c>true</c> if the source has breaking changes, otherwise, <c>false</c>.
        /// </returns>
        bool HasBreakingChanges(IIndexItem source, IIndexItem target);
    }
}