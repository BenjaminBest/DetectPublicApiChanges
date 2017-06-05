using DetectPublicApiChanges.Common;
using DetectPublicApiChanges.Interfaces;
using log4net;

namespace DetectPublicApiChanges.Steps
{
    /// <summary>
    /// Step for persisting the annalyzed results
    /// </summary>
    /// <seealso cref="Common.StepBase{CleanupStep}" />
    /// <seealso cref="IStep" />
    public class CleanupStep : StepBase<CleanupStep>, IStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CleanupStep" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public CleanupStep(ILog logger) : base(logger)
        {
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            ExecuteSafe(() =>
            {

            });
        }

    }

}