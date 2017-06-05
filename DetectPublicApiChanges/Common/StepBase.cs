using System;
using System.Threading.Tasks;
using log4net;

namespace DetectPublicApiChanges.Common
{
    /// <summary>
    /// Base class for steps
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StepBase<T> where T : class
    {
        private ILog _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="StepBase{T}"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public StepBase(ILog logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Excecutes the action with logging asynchronous
        /// </summary>
        /// <param name="action"></param>
        public async Task ExecuteSafeAsync (Func<Task> action)
        {
            _logger.Debug($"Start execution of step {typeof(T)}");

            try
            {
                await action();
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
                throw ex;
            }

            _logger.Debug($"Finished execution of step {typeof(T)}");
        }

        /// <summary>
        /// Excecutes the action with logging
        /// </summary>
        /// <param name="action"></param>
        public void ExecuteSafe(Action action)
        {
            _logger.Debug($"Start execution of step {typeof(T)}");

            try
            {
                action();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw ex;
            }

            _logger.Debug($"Finished execution of step {typeof(T)}");
        }
    }
}
