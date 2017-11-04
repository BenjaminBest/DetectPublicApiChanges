using CommandLine;
using CommandLine.Text;
using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Common
{
	/// <summary>
	/// Configuration settings which are set via commandline options
	/// </summary>
	/// <seealso cref="IOptions" />
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
		[Option("job", HelpText = "Name of the job", DefaultValue = "DetectObsoletes")]
		public string Job { get; set; }

		/// <summary>
		/// Gets or sets the regex filter.
		/// </summary>
		/// <value>
		/// The regex filter.
		/// </value>
		[Option("regexFilter", HelpText = "Filter applied on the namepace/name of projects, classes and interfaces to filter. Matching structures will not analyzed.", DefaultValue = @"\.Tests")]
		public string RegexFilter { get; set; }

		/// <summary>
		/// Gets or sets the repository connection string.
		/// </summary>
		/// <value>
		/// The repository connection string.
		/// </value>
		[Option("repositoryConnectionString", HelpText = "The connection string used for the repository, e.g: sourceControlType;repositoryUrl;startRevision;endRevision,User;Password")]
		public string RepositoryConnectionString { get; set; }

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
