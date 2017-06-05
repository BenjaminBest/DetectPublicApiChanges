using System;

namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The interface ISourceControlChangeLogItem defines one log item for a changelog
    /// </summary>
    public interface ISourceControlChangeLogItem
    {
        /// <summary>
        /// Gets the author.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        string Author { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        string Message { get; }

        /// <summary>
        /// Gets the time stamp.
        /// </summary>
        /// <value>
        /// The time stamp.
        /// </value>
        DateTime TimeStamp { get; }
    }
}