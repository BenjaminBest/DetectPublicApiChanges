using System;
using System.Diagnostics;
using log4net;

namespace DetectPublicApiChanges.Common
{
    /// <summary>
    /// The Timer uses the disposable pattern to log the time
    /// </summary>
    /// <seealso cref="IDisposable" />
    public class Timer : IDisposable
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILog _logger;

        /// <summary>
        /// The action
        /// </summary>
        private readonly Action<Stopwatch> _action;

        /// <summary>
        /// The stopwatch
        /// </summary>
        private readonly Stopwatch _stopwatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="Timer"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public Timer(ILog logger)
            : this()
        {
            _logger = logger;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Timer"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        public Timer(Action<Stopwatch> action)
            : this()
        {
            _action = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Timer"/> class.
        /// </summary>
        public Timer()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _stopwatch.Stop();

            _action?.Invoke(_stopwatch);

            _logger?.Info($"Elapsted time: {_stopwatch.Elapsed}");
        }
    }
}
