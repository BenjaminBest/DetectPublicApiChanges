namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The interface IStructureIndexTargetItemComparator defines a class which is used to compare index items and used a specific method for doing that.
    /// </summary>
    public interface IStructureIndexTargetItemComparator
    {
        /// <summary>
        /// Determines whether there are breaking changes between the <paramref name="source"/> and the <paramref name="target"/>
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <returns>
        ///   <c>true</c> if the target has breaking changes, otherwise, <c>false</c>.
        /// </returns>
        bool HasBreakingChanges(IIndexItem target, IIndexItem source);
    }
}