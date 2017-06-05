﻿using System;
using System.IO;

namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The interface ISourceControlClient defines a source control client
    /// </summary>
    public interface ISourceControlClient
    {
        /// <summary>
        /// Checks out the source code by using credentials.
        /// </summary>
        /// <param name="repositoryUrl">The repository URL.</param>
        /// <param name="localFolder">The local folder.</param>
        /// <param name="revision">The revision.</param>
        /// <param name="credentials">The credentials.</param>
        void CheckOut(Uri repositoryUrl, DirectoryInfo localFolder, int revision, ISourceControlCredentials credentials);

        /// <summary>
        /// Gets the change log.
        /// </summary>
        /// <param name="repositoryUrl">The repository URL.</param>
        /// <param name="startRevision">The start revision.</param>
        /// <param name="endRevision">The end revision.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        ISourceControlChangeLog GetChangeLog(Uri repositoryUrl, int startRevision, int endRevision, ISourceControlCredentials credentials);
    }
}