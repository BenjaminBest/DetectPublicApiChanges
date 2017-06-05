using System;
using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Common
{
    /// <summary>
    /// SourceControlConnection defines all information needed to connect to a repository
    /// </summary>
    /// <seealso cref="ISourceControlConnection" />
    public class SourceControlConnection : ISourceControlConnection
    {
        /// <summary>
        /// Gets or sets the repository source revision.
        /// </summary>
        /// <value>
        /// The repository source revision.
        /// </value>
        public int StartRevision { get; }

        /// <summary>
        /// Gets or sets the repository target revision.
        /// </summary>
        /// <value>
        /// The repository target revision.
        /// </value>
        public int EndRevision { get; }

        /// <summary>
        /// Gets or sets the repository URL.
        /// </summary>
        /// <value>
        /// The repository URL.
        /// </value>
        public string RepositoryUrl { get; }

        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        /// <value>
        /// The credentials.
        /// </value>
        public ISourceControlCredentials Credentials { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public SourceControlType Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceControlConnection" /> class.
        /// </summary>
        /// <param name="startRevision">The start revision.</param>
        /// <param name="endRevision">The end revision.</param>
        /// <param name="repositoryUrl">The repository URL.</param>
        /// <param name="type">The type.</param>
        public SourceControlConnection(int startRevision, int endRevision, string repositoryUrl, SourceControlType type)
        {
            StartRevision = startRevision;
            EndRevision = endRevision;
            RepositoryUrl = repositoryUrl;
            Type = type;
        }


        /// <summary>
        /// Parses the specified connection string.
        /// </summary>
        /// <remarks>
        /// Must be in format: sourceControlType;repositoryUrl;startRevision;endRevision,User;Password
        /// </remarks>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        public static SourceControlConnection Parse(string connectionString)
        {
            var parts = connectionString.Split(';');

            if (parts.Length < 4)
                throw new ArgumentException("The connectionString must at least contain sourceControlType,repositoryUrl,startRevision and endRevision");

            var connection = new SourceControlConnection(
                int.Parse(parts[2]),
                int.Parse(parts[3]),
                parts[1],
                (SourceControlType)Enum.Parse(typeof(SourceControlType), parts[0]));

            if (parts.Length > 4)
                connection.Credentials = new SourceControlCredentials(parts[4], parts[5]);

            return connection;
        }
    }
}
