using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Analysis.StructureIndex
{
    /// <summary>
    /// The class ItemSourceKeyComparator used a simple index item key comparison
    /// </summary>
    /// <seealso cref="IStructureIndexTargetItemComparator" />
    public class ItemSourceKeyComparator : IStructureIndexSourceItemComparator
    {
        /// <summary>
        /// Determines whether there are breaking changes between the <paramref name="source"/> and the <paramref name="target"/>
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <returns>
        ///   <c>true</c> if the target has breaking changes, otherwise <c>false</c>.
        /// </returns>
        public bool HasBreakingChanges(IIndexItem source, IIndexItem target)
        {
            return target == null;
        }
    }
}