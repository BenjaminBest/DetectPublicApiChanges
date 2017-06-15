using System;
using System.IO;
using System.Reflection;
using DetectPublicApiChanges.Common;
using DetectPublicApiChanges.Extensions;
using DetectPublicApiChanges.Interfaces;
using log4net;
using Ninject;

namespace DetectPublicApiChanges
{
    /// <summary>
    /// Entry point
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <exception cref="System.ArgumentException"></exception>
        static void Main(string[] args)
        {
            //Registering
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            //Logging
            log4net.Config.XmlConfigurator.Configure();

            //Get options
            var options = kernel.Get<IOptions>();
            options.ParseFromArguments(args);

            //Enviroment
            SetEnvironment(options, kernel.Get<IStore>(), kernel.Get<ILog>());

            //Run Job
            var registry = kernel.Get<IJobRegistry>();
            var job = registry.GetJob(options.Job);

            if (job == null)
                throw new ArgumentException($"Job with name {options.Job} was not found");

            job.Run();

            //The end
            Console.WriteLine("Press any key ..");
            Console.ReadKey();
        }

        /// <summary>
        /// Sets the environment.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="store">The store.</param>
        /// <param name="logger">The logger.</param>
        private static void SetEnvironment(IOptions options, IStore store, ILog logger)
        {
            //Create working directory
            var name = Path.Combine(options.WorkPath, $@"{DateTime.Now.ToFileTime()}");
            var dir = new DirectoryInfo(name);
            dir.EnsureExits();

            //Set log4net logging directory
            var logfile = Path.Combine(dir.FullName, "log.txt");
            foreach (var appender in LogManager.GetRepository().GetAppenders())
            {
                var fileAppender = appender as log4net.Appender.FileAppender;

                if (fileAppender == null)
                    continue;

                fileAppender.File = logfile;
                fileAppender.ActivateOptions();

                logger.Info($"Using {logfile} for logging");
            }

            //Add working directory
            store.SetOrAddItem(StoreKeys.WorkPath, dir);
            logger.Info($"Using {dir.FullName} as working directory");
        }
    }
}
