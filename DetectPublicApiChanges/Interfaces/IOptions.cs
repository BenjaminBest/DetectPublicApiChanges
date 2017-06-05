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
        /// Gets or sets the repository source revision.
        /// </summary>
        /// <value>
        /// The repository source revision.
        /// </value>
        int RepositorySourceRevision { get; set; }

        /// <summary>
        /// Gets or sets the repository target revision.
        /// </summary>
        /// <value>
        /// The repository target revision.
        /// </value>
        int RepositoryTargetRevision { get; set; }

        /// <summary>
        /// Gets or sets the repository URL.
        /// </summary>
        /// <value>
        /// The repository URL.
        /// </value>
        string RepositoryUrl { get; set; }

        /// <summary>
        /// Gets or sets the repository user.
        /// </summary>
        /// <value>
        /// The repository user.
        /// </value>
        string RepositoryUser { get; set; }

        /// <summary>
        /// Gets or sets the repository password.
        /// </summary>
        /// <value>
        /// The repository password.
        /// </value>
        string RepositoryPassword { get; set; }

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
