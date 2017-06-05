using System;

namespace DetectPublicApiChanges.Report.Models
{
    /// <summary>
    /// The ChangeLogViewModel contains the change log
    /// </summary>
    public class ChangeLogViewModel
    {
        /// <summary>
        /// Gets the author.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        public string Author { get; set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets the time stamp.
        /// </summary>
        /// <value>
        /// The time stamp.
        /// </value>
        public DateTime TimeStamp { get; set; }
    }
}
