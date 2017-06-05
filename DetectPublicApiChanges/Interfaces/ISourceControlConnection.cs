using DetectPublicApiChanges.Common;

namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The interface ISourceControlConnection defines all information needed to connect to a repository
    /// </summary>
    public interface ISourceControlConnection
    {
        /// <summary>
        /// Gets or sets the repository source revision.
        /// </summary>
        /// <value>
        /// The repository source revision.
        /// </value>
        int StartRevision { get; }

        /// <summary>
        /// Gets or sets the repository target revision.
        /// </summary>
        /// <value>
        /// The repository target revision.
        /// </value>
        int EndRevision { get; }

        /// <summary>
        /// Gets or sets the repository URL.
        /// </summary>
        /// <value>
        /// The repository URL.
        /// </value>
        string RepositoryUrl { get; }

        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        /// <value>
        /// The credentials.
        /// </value>
        ISourceControlCredentials Credentials { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        SourceControlType Type { get; }
    }
}