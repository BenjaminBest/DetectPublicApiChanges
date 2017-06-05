using System;
using System.IO;
using DetectPublicApiChanges.Common;
using DetectPublicApiChanges.Interfaces;
using log4net;

namespace DetectPublicApiChanges.Steps
{
    /// <summary>
    /// Step for persisting the annalyzed results
    /// </summary>
    /// <seealso cref="Common.StepBase{ReportCreationStep}" />
    /// <seealso cref="IStep" />
    public class ReportCreationStep : StepBase<ReportCreationStep>, IStep
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
        /// The renderer
        /// </summary>
        private readonly IRenderer _renderer;

        /// <summary>
        /// The file service
        /// </summary>
        private readonly IFileService _fileService;

        /// <summary>
        /// The report view model creator
        /// </summary>
        private readonly IReportViewModelCreator _reportViewModelCreator;

        /// <summary>
        /// The options
        /// </summary>
        private readonly IOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportCreationStep" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="store">The store.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="fileService">The file service.</param>
        /// <param name="reportViewModelCreator">The report view model creator.</param>
        /// <param name="options">The options.</param>
        public ReportCreationStep(
            ILog logger,
            IStore store,
            IRenderer renderer,
            IFileService fileService,
            IReportViewModelCreator reportViewModelCreator,
            IOptions options)
            : base(logger)
        {
            _logger = logger;
            _store = store;
            _renderer = renderer;
            _fileService = fileService;
            _reportViewModelCreator = reportViewModelCreator;
            _options = options;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            ExecuteSafe(() =>
            {
                _logger.Info("Create report based on resultset");

                //Create report
                var results = _store.GetItem<IStructureIndexComparisonResult>(StoreKeys.IndexComparisonResult);
                _renderer.Configure(_store.GetItem<DirectoryInfo>(StoreKeys.ViewsFolder));

                var changeLog = _store.GetItem<ISourceControlChangeLog>(StoreKeys.RepositoryChangeLog);
                var reportTime = _store.GetItem<DateTime>(StoreKeys.ReportTime);
                var title = $"{reportTime:yyyy-MM-dd HH:mm:ss} {_options.Title}";

                var subtitle = string.Empty;

                if (_options.RepositorySourceRevision != default(int) &&
                    _options.RepositoryTargetRevision != default(int) &&
                    !string.IsNullOrEmpty(_options.RepositoryUrl))
                {
                    subtitle = $"Repository: {_options.RepositoryUrl}</br>{_options.SolutionPathSource}(Rev {_options.RepositorySourceRevision}) - {_options.SolutionPathTarget}(Rev {_options.RepositoryTargetRevision})";
                }
                else
                {
                    subtitle = $"{_options.SolutionPathSource}</br>{_options.SolutionPathTarget}";
                }

                var html = _renderer.Render("Report.cshtml", _reportViewModelCreator.Create(title, subtitle, results, changeLog));

                //Save report
                var htmlFileName = _store.GetItem<string>(StoreKeys.ReportFullFileName);
                _logger.Info($"Report was created here:{htmlFileName}");

                _fileService.WriteAllText(htmlFileName, html);
            });
        }

    }

}