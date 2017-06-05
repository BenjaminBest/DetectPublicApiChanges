using System.Collections.Generic;
using DetectPublicApiChanges.Common;
using DetectPublicApiChanges.Interfaces;
using log4net;

namespace DetectPublicApiChanges.Jobs
{
    /// <summary>
    /// Starting Self diagnosis
    /// </summary>
    /// <seealso cref="Common.JobBase{DetectChanges}" />
    /// <seealso cref="IJob" />
    public class DetectChangesJob : JobBase<DetectChangesJob>, IJob
    {
        private readonly IEnumerable<IStep> _steps;

        /// <summary>
        /// Gets the name of the job.
        /// </summary>
        /// <value>
        /// The name of the job.
        /// </value>
        public string JobName => "DetectChanges";

        /// <summary>
        /// Initializes a new instance of the <see cref="DetectChangesJob"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="steps">The steps.</param>
        public DetectChangesJob(ILog logger, IEnumerable<IStep> steps)
            : base(logger)
        {
            _steps = steps;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            ExecuteSafe(() =>
            {
                foreach (var step in _steps)
                {
                    step.Run();
                }
            });
        }
    }
}
