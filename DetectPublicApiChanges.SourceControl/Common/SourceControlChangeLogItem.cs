using System;
using DetectPublicApiChanges.SourceControl.Interfaces;

namespace DetectPublicApiChanges.SourceControl.Subversion
{
    /// <summary>
    /// SourceControlChangeLogItem defines one log item for a changelog
    /// </summary>
    /// <seealso cref="ISourceControlChangeLogItem" />
    public class SourceControlChangeLogItem : ISourceControlChangeLogItem
    {
        /// <summary>
        /// Gets the author.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        public string Author { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; }

        /// <summary>
        /// Gets the time stamp.
        /// </summary>
        /// <value>
        /// The time stamp.
        /// </value>
        public DateTime TimeStamp { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceControlChangeLogItem" /> class.
        /// </summary>
        /// <param name="author">The author.</param>
        /// <param name="message">The author.</param>
        /// <param name="timeStamp">The message.</param>
        public SourceControlChangeLogItem(string author, string message, DateTime timeStamp)
        {
            Author = author;
            Message = message;
            TimeStamp = timeStamp;
        }
    }
}
