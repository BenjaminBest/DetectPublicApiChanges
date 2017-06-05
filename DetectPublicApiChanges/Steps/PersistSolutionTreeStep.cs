using System.Collections.Generic;
using System.IO;
using System.Linq;
using DetectPublicApiChanges.Analysis.Structures;
using DetectPublicApiChanges.Common;
using DetectPublicApiChanges.Interfaces;
using log4net;

namespace DetectPublicApiChanges.Steps
{
    /// <summary>
    /// Step for persisting the annalyzed results
    /// </summary>
    /// <seealso cref="Common.StepBase{PersistSolutionTreeStep}" />
    /// <seealso cref="IStep" />
    public class PersistSolutionTreeStep : StepBase<PersistSolutionTreeStep>, IStep
    {
        private readonly ILog _logger;
        private readonly IStore _store;
        private readonly IFileSerializer _serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistSolutionTreeStep"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="store">The store.</param>
        /// <param name="serializer">The serializer.</param>
        public PersistSolutionTreeStep(ILog logger, IStore store, IFileSerializer serializer)
            : base(logger)
        {
            _logger = logger;
            _store = store;
            _serializer = serializer;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            ExecuteSafe(() =>
            {
                var workDir = _store.GetItem<DirectoryInfo>(StoreKeys.WorkPath);
                var solutions = _store.GetItem<List<SolutionStructure>>(StoreKeys.Solutions).ToList();

                for (var s = 0; s < solutions.Count; s++)
                {
                    var solution = solutions[s];

                    var fileName = new FileInfo(Path.Combine(workDir.FullName, solution.Name + $"_analysis{s}.json"));

                    _serializer.Serialize(fileName, solution);
                    _logger.Info($"Persisted results for solution '{solution.Name}' to file '{fileName.FullName}'");
                }
            });
        }
    }
}
