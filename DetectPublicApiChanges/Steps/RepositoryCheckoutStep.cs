using System;
using System.IO;
using DetectPublicApiChanges.Common;
using DetectPublicApiChanges.Interfaces;
using DetectPublicApiChanges.SourceControl.Interfaces;
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
        /// The source control factory
        /// </summary>
        private readonly ISourceControlFactory _sourceControlFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryCheckoutStep" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="store">The store.</param>
        /// <param name="options">The options.</param>
        /// <param name="sourceControlFactory">The source control factory.</param>
        public RepositoryCheckoutStep(
            ILog logger,
            IStore store,
            IOptions options,
            ISourceControlFactory sourceControlFactory)
            : base(logger)
        {
            _logger = logger;
            _store = store;
            _options = options;
            _sourceControlFactory = sourceControlFactory;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            ExecuteSafe(() =>
            {
                var configuration = _store.GetItem<ISourceControlConfiguration>(StoreKeys.RepositoryConnection);

                if (configuration == null)
                {
                    _logger.Info("No source control system is used");
                    return;
                }

                var client = _sourceControlFactory.CreateClient(configuration);

                var checkoutFolderSource = new DirectoryInfo(Path.Combine(_store.GetItem<DirectoryInfo>(StoreKeys.WorkPath).FullName, "Source"));
                var checkoutFolderTarget = new DirectoryInfo(Path.Combine(_store.GetItem<DirectoryInfo>(StoreKeys.WorkPath).FullName, "Target"));

                //Checkout
                client.CheckOut(new Uri(configuration.RepositoryUrl), checkoutFolderSource, configuration.StartRevision, configuration.Credentials);
                client.CheckOut(new Uri(configuration.RepositoryUrl), checkoutFolderTarget, configuration.EndRevision, configuration.Credentials);

                //Set global folders
                _store.SetOrAddItem(StoreKeys.SolutionPathSource, Path.Combine(checkoutFolderSource.FullName, _options.SolutionPathSource));
                _store.SetOrAddItem(StoreKeys.SolutionPathTarget, Path.Combine(checkoutFolderTarget.FullName, _options.SolutionPathTarget));

                //Get Changelog
                _store.SetOrAddItem(StoreKeys.RepositoryChangeLog,
                    client.GetChangeLog(new Uri(configuration.RepositoryUrl), checkoutFolderTarget, configuration.StartRevision, configuration.EndRevision, configuration.Credentials));
            });
        }
    }
}