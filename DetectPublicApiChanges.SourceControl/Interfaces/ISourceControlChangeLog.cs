using System.Collections.Generic;

namespace DetectPublicApiChanges.SourceControl.Interfaces
{
    /// <summary>
    /// The interface ISourceControlChangeLog defines a change log for revisions
    /// </summary>
    public interface ISourceControlChangeLog
    {
        /// <summary>
        /// Gets the start revision.
        /// </summary>
        /// <value>
        /// The start revision.
        /// </value>
        string StartRevision { get; }

        /// <summary>
        /// Gets the end revision.
        /// </summary>
        /// <value>
        /// The end revision.
        /// </value>
        string EndRevision { get; }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        IReadOnlyList<ISourceControlChangeLogItem> Items { get; }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="item">The item.</param>
        void AddItem(ISourceControlChangeLogItem item);
    }
}