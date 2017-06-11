namespace DetectPublicApiChanges.SourceControl.Interfaces
{
    /// <summary>
    /// The interface ISourceControlFactory is a factory to create the concrete client based on the configuration
    /// </summary>
    public interface ISourceControlFactory
    {
        /// <summary>
        /// Creates the client.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        ISourceControlClient CreateClient(ISourceControlConfiguration configuration);
    }
}
