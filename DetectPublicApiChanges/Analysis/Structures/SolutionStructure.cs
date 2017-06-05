using System.Collections.Generic;
using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Analysis.Structures
{
    /// <summary>
    /// Solution
    /// </summary>
    public class SolutionStructure : IStructure
    {
        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        public IEnumerable<ProjectStructure> Projects
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>

        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionStructure"/> class.
        /// </summary>
        public SolutionStructure()
            : this(string.Empty, string.Empty)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionStructure" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="path">The path.</param>
        public SolutionStructure(string name, string path)
        {
            Name = name;
            Path = path;
            Projects = new List<ProjectStructure>();
        }

        /// <summary>
        /// Adds the project.
        /// </summary>
        /// <param name="projectStructure">The project structure.</param>
        public void AddProject(ProjectStructure projectStructure)
        {
            ((List<ProjectStructure>)Projects).Add(projectStructure);
        }
    }
}
