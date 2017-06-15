using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;

namespace DetectPublicApiChanges.Analysis.Filters
{
    /// <summary>
    /// ProjectRegexFilterExtensions defines extentions methods for filtering
    /// </summary>
    public static class ProjectRegexFilterExtensions
    {


        /// <summary>
        /// Filters the specified filter.
        /// </summary>
        /// <param name="projects">The projects.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<Project> Filter(this IEnumerable<Project> projects, string filter)
        {
            return string.IsNullOrEmpty(filter) ? projects : projects.Where(p => !Regex.IsMatch(p.Name, filter));
        }
    }
}
