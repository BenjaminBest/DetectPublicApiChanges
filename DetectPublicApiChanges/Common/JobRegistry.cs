using DetectPublicApiChanges.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DetectPublicApiChanges.Common
{
    /// <summary>
    /// Registry for all available jobs
    /// </summary>
    /// <seealso cref="DetectPublicApiChanges.Interfaces.IJobRegistry" />
    public class JobRegistry : IJobRegistry
    {
        private readonly IEnumerable<IJob> _jobs;

        /// <summary>
        /// Initializes a new instance of the <see cref="JobRegistry"/> class.
        /// </summary>
        /// <param name="jobs">The jobs.</param>
        public JobRegistry(IEnumerable<IJob> jobs)
        {
            _jobs = jobs;
        }

        /// <summary>
        /// Gets the jobs.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IJob> GetJobs()
        {
            return _jobs;
        }

        /// <summary>
        /// Gets the job.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public IJob GetJob(string name)
        {
            return _jobs.FirstOrDefault(j => j.JobName.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
