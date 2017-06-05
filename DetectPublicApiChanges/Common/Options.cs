using CommandLine;
using CommandLine.Text;
using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Common
{
    /// <summary>
    /// Configuration settings which are set via commandline options
    /// </summary>
    /// <seealso cref="IOptions" />
    /// <remarks>
    /// Sample for folder based comparison: 
    /// --job DetectChanges 
    /// --title "Public API Changes" 
    /// --solutionPathSource "C:\Development\OLD\DetectPublicApiChanges\DetectPublicApiChanges\DetectPublicApiChanges.sln" 
    /// --solutionPathTarget "C:\Development\DetectPublicApiChanges\DetectPublicApiChanges\DetectPublicApiChanges.sln"
    /// Sample for subversion based comparison: 
    /// --job DetectChanges 
    /// --title "Public API Changes" 
    /// --repositorySourceRevision "28" 
    /// --repositoryTargetRevision "29" 
    /// --repositoryUrl "https://xyz/svn/DetectPublicApiChanges/trunk" 
    /// --repositoryUser "api" 
    /// --repositoryPassword "api" 
    /// --workPath "C:\Work"
    /// --solutionPathSource "DetectPublicApiChanges\DetectPublicApiChanges.sln" 
    /// --solutionPathTarget "DetectPublicApiChanges\DetectPublicApiChanges.sln"
    /// </remarks>
    public class Options : IOptions
    {
        /// <summary>
        /// Gets or sets the solution path.
        /// </summary>
        /// <value>
        /// The solution path.
        /// </value>
        [Option("solutionPathSource", HelpText = "The source solution file with path to be analyzed. When using a repository this should be a relative path with the solution file.", DefaultValue = @"..\DetectPublicApiChanges.sln")]
        public string SolutionPathSource { get; set; }

        /// <summary>
        /// Gets or sets the solution path.
        /// </summary>
        /// <value>
        /// The solution path.
        /// </value>
        [Option("solutionPathTarget", HelpText = "The target solution file with path to be analyzed. When using a repository this should be a relative path with the solution file.", DefaultValue = @"..\DetectPublicApiChanges.sln")]
        public string SolutionPathTarget { get; set; }

        /// <summary>
        /// Gets or sets the job.
        /// </summary>
        /// <value>
        /// The job.
        /// </value>
        [Option("job", Required = true, HelpText = "Name of the job")]
        public string Job { get; set; }

        /// <summary>
        /// Gets or sets the repository source revision.
        /// </summary>
        /// <value>
        /// The repository source revision.
        /// </value>
        [Option("repositorySourceRevision", HelpText = "The source revision to checkout")]
        public int RepositorySourceRevision { get; set; }

        /// <summary>
        /// Gets or sets the repository target revision.
        /// </summary>
        /// <value>
        /// The repository target revision.
        /// </value>
        [Option("repositoryTargetRevision", HelpText = "The target revision to checkout")]
        public int RepositoryTargetRevision { get; set; }

        /// <summary>
        /// Gets or sets the repository URL.
        /// </summary>
        /// <value>
        /// The repository URL.
        /// </value>
        [Option("repositoryUrl", HelpText = "The repository URL to checkout from")]
        public string RepositoryUrl { get; set; }

        /// <summary>
        /// Gets or sets the repository user.
        /// </summary>
        /// <value>
        /// The repository user.
        /// </value>
        [Option("repositoryUser", HelpText = "The repository user to login with")]
        public string RepositoryUser { get; set; }

        /// <summary>
        /// Gets or sets the repository password.
        /// </summary>
        /// <value>
        /// The repository password.
        /// </value>
        [Option("repositoryPassword", HelpText = "The repository password to use")]
        public string RepositoryPassword { get; set; }

        /// <summary>
        /// Gets or sets the work path.
        /// </summary>
        /// <value>
        /// The work path.
        /// </value>
        [Option("workPath", HelpText = "The path for temporary files and the results", DefaultValue = @"Work")]
        public string WorkPath { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Option("title", HelpText = "The title shown in the report", DefaultValue = @"Public API Changes")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the last state of the parser.
        /// </summary>
        /// <value>
        /// The last state of the parser.
        /// </value>
        [ParserState]
        public IParserState LastParserState { get; set; }

        /// <summary>
        /// Gets the usage.
        /// </summary>
        /// <returns></returns>
        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
