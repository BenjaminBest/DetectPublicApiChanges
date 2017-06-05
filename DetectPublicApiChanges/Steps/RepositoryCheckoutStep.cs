using System;
using System.IO;
using DetectPublicApiChanges.Common;
using DetectPublicApiChanges.Interfaces;
using log4net;

namespace DetectPublicApiChanges.Steps
{
    /// <summary>
    /// Step for checking out the code
    /// </summary>
    /// <seealso cref="Common.StepBase{RepositoryCheckoutStep}" />
    /// <seealso cref="IStep" />
    /// <seealso cref="RepositoryCheckoutStep" />
    public class RepositoryCheckoutStep : StepBase<RepositoryCheckoutStep>, IStep
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILog _logger;

        /// <summary>
        /// The store
        /// </summary>
        private readonly IStore _store;

        /// <summary>
        /// The options
        /// </summary>
        private readonly IOptions _options;

        /// <summary>
        /// The source control client
        /// </summary>
        private readonly ISourceControlClient _sourceControlClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryCheckoutStep" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="store">The store.</param>
        /// <param name="options">The options.</param>
        /// <param name="sourceControlClient">The source control client.</param>
        public RepositoryCheckoutStep(
            ILog logger,
            IStore store,
            IOptions options,
            ISourceControlClient sourceControlClient)
            : base(logger)
        {
            _logger = logger;
            _store = store;
            _options = options;
            _sourceControlClient = sourceControlClient;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            ExecuteSafe(() =>
            {
                var connection = _store.GetItem<ISourceControlConnection>(StoreKeys.RepositoryConnection);

                if (connection == null)
                {
                    _logger.Info("No source control system is used");
                    return;
                }

                var checkoutFolderSource = new DirectoryInfo(Path.Combine(_store.GetItem<DirectoryInfo>(StoreKeys.WorkPath).FullName, "Source"));
                var checkoutFolderTarget = new DirectoryInfo(Path.Combine(_store.GetItem<DirectoryInfo>(StoreKeys.WorkPath).FullName, "Target"));

                //Checkout
                _sourceControlClient.CheckOut(new Uri(connection.RepositoryUrl), checkoutFolderSource, connection.StartRevision, connection.Credentials);
                _sourceControlClient.CheckOut(new Uri(connection.RepositoryUrl), checkoutFolderTarget, connection.EndRevision, connection.Credentials);

                //Set global folders
                _store.SetOrAddItem(StoreKeys.SolutionPathSource, Path.Combine(checkoutFolderSource.FullName, _options.SolutionPathSource));
                _store.SetOrAddItem(StoreKeys.SolutionPathTarget, Path.Combine(checkoutFolderTarget.FullName, _options.SolutionPathTarget));

                //Get Changelog
                _store.SetOrAddItem(StoreKeys.RepositoryChangeLog,
                    _sourceControlClient.GetChangeLog(new Uri(connection.RepositoryUrl), connection.StartRevision, connection.EndRevision, connection.Credentials));
            });
        }
    }
}