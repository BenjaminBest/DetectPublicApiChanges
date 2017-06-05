using System;
using System.IO;
using DetectPublicApiChanges.Interfaces;
using log4net;
using SharpSvn;

namespace DetectPublicApiChanges.Common
{
    /// <summary>
    /// SubversionSourceControlClient encapsulates a subversion client to access svn repositories
    /// </summary>
    /// <seealso cref="ISourceControlClient" />
    public class SubversionSourceControlClient : ISourceControlClient
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILog _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubversionSourceControlClient"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public SubversionSourceControlClient(ILog logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Checks out the source code by using credentials.
        /// </summary>
        /// <param name="repositoryUrl">The repository URL.</param>
        /// <param name="localFolder">The local folder.</param>
        /// <param name="revision">The revision.</param>
        /// <param name="credentials">The credentials.</param>
        public void CheckOut(Uri repositoryUrl, DirectoryInfo localFolder, int revision, ISourceControlCredentials credentials = null)
        {
            _logger.Info($"Checkout of '{repositoryUrl.AbsolutePath}' to '{localFolder.FullName}'");

            using (var client = new SvnClient())
            {
                if (credentials != null)
                {
                    _logger.Info("Not using credentials");
                    client.Authentication.ForceCredentials(credentials.User, credentials.Password);
                }

                client.Authentication.SslServerTrustHandlers += Authentication_SslServerTrustHandlers;

                // Checkout the code to the specified directory
                client.CheckOut(repositoryUrl, localFolder.FullName,
                    new SvnCheckOutArgs() { Revision = new SvnRevision(revision) });

                _logger.Info("Checkout finished");
            }
        }

        /// <summary>
        /// Gets the change log.
        /// </summary>
        /// <param name="repositoryUrl">The repository URL.</param>
        /// <param name="startRevision">The start revision.</param>
        /// <param name="endRevision">The end revision.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public ISourceControlChangeLog GetChangeLog(Uri repositoryUrl, int startRevision, int endRevision,
            ISourceControlCredentials credentials = null)
        {
            _logger.Info($"Get log of '{repositoryUrl.AbsolutePath}' of revisions {startRevision}-{endRevision}");

            var log = new SourceControlChangeLog(startRevision, endRevision);

            using (var client = new SvnClient())
            {
                if (credentials != null)
                {
                    _logger.Info("Not using credentials");
                    client.Authentication.ForceCredentials(credentials.User, credentials.Password);
                }

                client.Authentication.SslServerTrustHandlers += Authentication_SslServerTrustHandlers;

                client.Log(
                    repositoryUrl,
                    new SvnLogArgs
                    {
                        Range = new SvnRevisionRange(startRevision, endRevision)
                    },
                    (o, e) =>
                    {
                        log.AddItem(new SourceControlChangeLogItem(e.Author, e.LogMessage, e.Time));
                    });
            }

            _logger.Info("Showlog finished");

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
