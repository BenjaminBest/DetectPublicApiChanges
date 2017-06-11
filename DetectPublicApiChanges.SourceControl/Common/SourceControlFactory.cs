using System;
using System.Collections.Generic;
using System.Linq;
using DetectPublicApiChanges.SourceControl.Interfaces;
using log4net;

namespace DetectPublicApiChanges.SourceControl.Common
{
    /// <summary>
    /// The SourceControlFactory is used to create the concrete source control client based on the configuration used.
    /// </summary>
    /// <seealso cref="ISourceControlFactory" />
    public class SourceControlFactory : ISourceControlFactory
    {
        /// <summary>
        /// The clients
        /// </summary>
        private readonly IEnumerable<ISourceControlClient> _clients;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILog _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceControlFactory"/> class.
        /// </summary>
        /// <param name="clients">The clients.</param>
        /// <param name="logger">The logger.</param>
        public SourceControlFactory(IEnumerable<ISourceControlClient> clients, ILog logger)
        {
            _clients = clients;
            _logger = logger;
        }

        /// <summary>
        /// Creates the client.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public ISourceControlClient CreateClient(ISourceControlConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var client = _clients.FirstOrDefault(c => c.Type.Equals(configuration.Type));

            if (client == null)
                throw new ArgumentException($"The source control type '{configuration.Type}' is unknown. The source control client cannot be created.");

            _logger.Info($"Client for {configuration.Type} was created.");

            return client;
        }
    }
}
