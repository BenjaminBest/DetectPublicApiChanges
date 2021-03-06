﻿using DetectPublicApiChanges.SourceControl.Common;

namespace DetectPublicApiChanges.SourceControl.Interfaces
{
    /// <summary>
    /// The interface ISourceControlConfiguration defines all information needed to connect to a repository
    /// </summary>
    public interface ISourceControlConfiguration
    {
        /// <summary>
        /// Gets or sets the repository source revision.
        /// </summary>
        /// <value>
        /// The repository source revision.
        /// </value>
        string StartRevision { get; }

        /// <summary>
        /// Gets or sets the repository target revision.
        /// </summary>
        /// <value>
        /// The repository target revision.
        /// </value>
        string EndRevision { get; }

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