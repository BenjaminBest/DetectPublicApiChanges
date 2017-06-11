using System;
using System.IO;
using DetectPublicApiChanges.SourceControl.Common;
using DetectPublicApiChanges.SourceControl.Interfaces;
using SharpSvn;

namespace DetectPublicApiChanges.SourceControl.Subversion
{
    /// <summary>
    /// SubversionSourceControlClient encapsulates a subversion client to access svn repositories
    /// </summary>
    /// <seealso cref="ISourceControlClient" />
    public class SubversionSourceControlClient : ISourceControlClient
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public SourceControlType Type => SourceControlType.Svn;

        /// <summary>
        /// Checks out the source code by using credentials.
        /// </summary>
        /// <param name="repositoryUrl">The repository URL.</param>
        /// <param name="localFolder">The local folder.</param>
        /// <param name="revision">The revision.</param>
        /// <param name="credentials">The credentials.</param>
        public void CheckOut(Uri repositoryUrl, DirectoryInfo localFolder, string revision, ISourceControlCredentials credentials = null)
        {
            using (var client = new SvnClient())
            {
                if (credentials != null)
                {
                    client.Authentication.ForceCredentials(credentials.User, credentials.Password);
                }

                client.Authentication.SslServerTrustHandlers += Authentication_SslServerTrustHandlers;

                // Checkout the code to the specified directory
                client.CheckOut(repositoryUrl, localFolder.FullName,
                    new SvnCheckOutArgs { Revision = new SvnRevision(int.Parse(revision)) });
            }
        }

        /// <summary>
        /// Gets the change log.
        /// </summary>
        /// <param name="repositoryUrl">The repository URL.</param>
        /// <param name="localFolder">The local folder.</param>
        /// <param name="startRevision">The start revision.</param>
        /// <param name="endRevision">The end revision.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public ISourceControlChangeLog GetChangeLog(Uri repositoryUrl, DirectoryInfo localFolder, string startRevision, string endRevision,
            ISourceControlCredentials credentials = null)
        {
            var log = new SourceControlChangeLog(startRevision, endRevision);

            using (var client = new SvnClient())
            {
                if (credentials != null)
                {
                    client.Authentication.ForceCredentials(credentials.User, credentials.Password);
                }

                client.Authentication.SslServerTrustHandlers += Authentication_SslServerTrustHandlers;

                client.Log(
                    repositoryUrl,
                    new SvnLogArgs
                    {
                        Range = new SvnRevisionRange(int.Parse(startRevision), int.Parse(endRevision))
                    },
                    (o, e) =>
                    {
                        log.AddItem(new SourceControlChangeLogItem(e.Author, e.LogMessage, e.Time));
                    });
            }

            return log;
        }

        /// <summary>
        /// Handles the SslServerTrustHandlers event of the Authentication control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SharpSvn.Security.SvnSslServerTrustEventArgs"/> instance containing the event data.</param>
        private static void Authentication_SslServerTrustHandlers(object sender, SharpSvn.Security.SvnSslServerTrustEventArgs e)
        {
            // If accept:
            e.AcceptedFailures = e.Failures;
            e.Save = true; // Save acceptance to authentication store
        }
    }
}
