using System;
using System.IO;
using DetectPublicApiChanges.Interfaces;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace DetectPublicApiChanges.Report.Mvc
{
    /// <summary>
    /// Renderer is used to compile and render MVC Razor views with razor engine
    /// </summary>
    /// <seealso cref="IRenderer" />
    public class RazorEngineRenderer : IRenderer
    {
        /// <summary>
        /// The file service
        /// </summary>
        private readonly IFileService _fileService;

        /// <summary>
        /// The razor engine service
        /// </summary>
        private IRazorEngineService _razorEngineService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RazorEngineRenderer"/> class.
        /// </summary>
        /// <param name="fileService">The file service.</param>
        public RazorEngineRenderer(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Configures the specified views folder.
        /// </summary>
        /// <param name="viewsFolder">The views folder.</param>
        public void Configure(DirectoryInfo viewsFolder)
        {
            if (!viewsFolder.Exists)
                throw new ArgumentException("The views folder must exist");

            var config = new TemplateServiceConfiguration
            {
                TemplateManager = new ResolvePathTemplateManager(new[]
                {
                    viewsFolder.FullName
                }),
                BaseTemplateType = typeof(HtmlTemplateBase<>),
                DisableTempFileLocking = true,
                CachingProvider = new DefaultCachingProvider(t => { })
            };

            _razorEngineService = RazorEngineService.Create(config);

            foreach (var views in _fileService.GetFileNamesInDirectory(viewsFolder, "*.cshtml"))
            {
                _razorEngineService.Compile(views);
            }
        }

        /// <summary>
        /// Renders the specified view.
        /// </summary>
        /// <typeparam name="TViewModelType">The type of the view model type.</typeparam>
        /// <param name="view">The view.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public string Render<TViewModelType>(string view, TViewModelType viewModel) where TViewModelType : class
        {
            if (_razorEngineService == null)
                throw new InvalidOperationException("The razor-engine must be initialized to render");

            return _razorEngineService.Run(view, typeof(TViewModelType), viewModel);
        }
    }
}
