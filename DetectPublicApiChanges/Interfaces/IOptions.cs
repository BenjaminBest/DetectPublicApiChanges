namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The interface IOptions defines command line options
    /// </summary>
    public interface IOptions
    {
        /// <summary>
        /// Gets or sets the solution path source.
        /// </summary>
        /// <value>
        /// The solution path source.
        /// </value>
        string SolutionPathSource { get; set; }

        /// <summary>
        /// Gets or sets the solution path target.
        /// </summary>
        /// <value>
        /// The solution path target.
        /// </value>
        string SolutionPathTarget { get; set; }

        /// <summary>
        /// Gets or sets the job.
        /// </summary>
        /// <value>
        /// The job.
        /// </value>
        string Job { get; set; }

        /// <summary>
        /// Gets or sets the regex filter.
        /// </summary>
        /// <value>
        /// The regex filter.
        /// </value>
        string RegexFilter { get; set; }

        /// <summary>
        /// Gets or sets the repository connection string.
        /// </summary>
        /// <value>
        /// The repository connection string.
        /// </value>
        string RepositoryConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the work path.
        /// </summary>
        /// <value>
        /// The work path.
        /// </value>
        string WorkPath { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        string Title { get; set; }
    }
}
