﻿using System.Collections.Generic;
using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Common
{
    /// <summary>
    /// SourceControlChangeLog defines a change log for revisions
    /// </summary>
    /// <seealso cref="ISourceControlChangeLog" />
    public class SourceControlChangeLog : ISourceControlChangeLog
    {
        /// <summary>
        /// The items
        /// </summary>
        private readonly List<ISourceControlChangeLogItem> _items = new List<ISourceControlChangeLogItem>();

        /// <summary>
        /// Gets the start revision.
        /// </summary>
        /// <value>
        /// The start revision.
        /// </value>
        public int StartRevision { get; }

        /// <summary>
        /// Gets the end revision.
        /// </summary>
        /// <value>
        /// The end revision.
        /// </value>
        public int EndRevision { get; }

        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        public IReadOnlyList<ISourceControlChangeLogItem> Items => _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceControlChangeLog"/> class.
        /// </summary>
        /// <param name="startRevision">The start revision.</param>
        /// <param name="endRevision">The end revision.</param>
        public SourceControlChangeLog(int startRevision, int endRevision)
        {
            StartRevision = startRevision;
            EndRevision = endRevision;
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void AddItem(ISourceControlChangeLogItem item)
        {
            if (item == null)
                return;

            _items.Add(item);
        }
    }
}
