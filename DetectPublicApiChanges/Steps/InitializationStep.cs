using System;
using System.IO;
using DetectPublicApiChanges.Analysis.Roslyn;
using DetectPublicApiChanges.Common;
using DetectPublicApiChanges.Interfaces;
using DetectPublicApiChanges.SourceControl.Common;
using log4net;

namespace DetectPublicApiChanges.Steps
{
    /// <summary>
    /// Step for initialization
    /// </summary>
    /// <seealso cref="Common.StepBase{InitializationStep}" />
    /// <seealso cref="IStep" />
    /// <seealso cref="InitializationStep" />
    public class InitializationStep : StepBase<InitializationStep>, IStep
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
        /// Initializes a new instance of the <see cref="InitializationStep" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="store">The store.</param>
        /// <param name="options">The options.</param>
        public InitializationStep(ILog logger, IStore store, IOptions options)
            : base(logger)
        {
            _logger = logger;
            _store = store;
            _options = options;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            ExecuteSafe(() =>
            {
                EnumerableStructureLogExtensions.Logger = _logger;

                _store.SetOrAddItem(StoreKeys.ViewsFolder, new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Views")));
                var reportTime = DateTime.Now;
                _store.SetOrAddItem(StoreKeys.ReportTime, reportTime);
                _store.SetOrAddItem(StoreKeys.ReportFullFileName, Path.Combine(_store.GetItem<DirectoryInfo>(StoreKeys.WorkPath).FullName, $"{reportTime:yyyy-MM-dd_HH_mm_ss}_Report.html"));

                _store.SetOrAddItem(StoreKeys.SolutionPathSource, _options.SolutionPathSource);
                _store.SetOrAddItem(StoreKeys.SolutionPathTarget, _options.SolutionPathTarget);

                if (!string.IsNullOrEmpty(_options.RepositoryConnectionString))
                    _store.SetOrAddItem(StoreKeys.RepositoryConnection, SourceControlConfiguration.Parse(_options.RepositoryConnectionString));
            });
        }
    }
}