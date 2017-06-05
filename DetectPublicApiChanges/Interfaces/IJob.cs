namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// Interface for jobs
    /// </summary>
    public interface IJob
    {
        /// <summary>
        /// Gets the name of the job.
        /// </summary>
        /// <value>
        /// The name of the job.
        /// </value>
        string JobName { get; }

        /// <summary>
        /// Runs the job.
        /// </summary>
        void Run();
    }
}
