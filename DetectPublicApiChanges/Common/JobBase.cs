using System;
using log4net;

namespace DetectPublicApiChanges.Common
{
    /// <summary>
    /// Base class for jobs
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JobBase<T> where T : class
    {
        private ILog _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="JobBase{T}"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public JobBase(ILog logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Executes the action with logging
        /// </summary>
        /// <param name="action">The action.</param>
        public void ExecuteSafe (Action action)
        {
            _logger.Debug($"Start execution of job {typeof(T)}");

            try
            {
                action();
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
                throw ex;
            }

            _logger.Debug($"Finished execution of job {typeof(T)}");
        }
    }
}
