using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace DetectPublicApiChanges.Analysis.Roslyn
{
    /// <summary>
    /// Helpers for using the roslyn workspace
    /// </summary>
    public class WorkspaceHelper
    {
        /// <summary>
        /// Gets the compilations.
        /// </summary>
        /// <param name="solutionPath">The solution path.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Compilation>> GetCompilations(string solutionPath)
        {
            var workspace = MSBuildWorkspace.Create();
            var solution = await workspace.OpenSolutionAsync(solutionPath);
            var compilations = await Task.WhenAll(solution.Projects.Select(x => x.GetCompilationAsync()));            

            return compilations;
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="solutionPath">The solution path.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Project>> GetProjects(string solutionPath)
        {
            var workspace = MSBuildWorkspace.Create();
            var solution = await workspace.OpenSolutionAsync(solutionPath);

            return solution.Projects;            
        }

        /// <summary>
        /// Gets the solution.
        /// </summary>
        /// <param name="solutionPath">The solution path.</param>
        /// <returns></returns>
        public static async Task<Solution> GetSolution(string solutionPath)
        {
            var workspace = MSBuildWorkspace.Create();
            var solution = await workspace.OpenSolutionAsync(solutionPath);

            return solution;
        }
    }
}
