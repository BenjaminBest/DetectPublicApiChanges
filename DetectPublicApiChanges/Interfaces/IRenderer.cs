using System.IO;

namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The interface IRenderer defines a renderer which is used to render MVC Razor templates
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// Configures the specified views folder.
        /// </summary>
        /// <param name="viewsFolder">The views folder.</param>
        void Configure(DirectoryInfo viewsFolder);

        /// <summary>
        /// Renders the specified view.
        /// </summary>
        /// <typeparam name="TViewModelType">The type of the view model type.</typeparam>
        /// <param name="view">The view.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        string Render<TViewModelType>(string view, TViewModelType viewModel) where TViewModelType : class;
    }
}