using System.Collections.Generic;
using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Analysis.Structures
{
    /// <summary>
    /// Project
    /// </summary>
    public class ProjectStructure : IStructure
    {
        /// <summary>
        /// Gets or sets the classes.
        /// </summary>
        /// <value>
        /// The classes.
        /// </value>
        public IEnumerable<ClassStructure> Classes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the interfaces.
        /// </summary>
        /// <value>
        /// The interfaces.
        /// </value>
        public IEnumerable<InterfaceStructure> Interfaces
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
        /// Initializes a new instance of the <see cref="ProjectStructure" /> class.
        /// </summary>
        public ProjectStructure()
                    : this(string.Empty)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectStructure" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ProjectStructure(string name)
        {
            Name = name;
            Classes = new List<ClassStructure>();
            Interfaces = new List<InterfaceStructure>();
        }

        /// <summary>
        /// Adds the classes.
        /// </summary>
        /// <param name="classStructures">The class structures.</param>
        public void AddClasses(IEnumerable<ClassStructure> classStructures)
        {
            ((List<ClassStructure>)Classes).AddRange(classStructures);
        }

        /// <summary>
        /// Adds the interfaces.
        /// </summary>
        /// <param name="interfaceStructures">The interface structures.</param>
        public void AddInterfaces(IEnumerable<InterfaceStructure> interfaceStructures)
        {
            ((List<InterfaceStructure>)Interfaces).AddRange(interfaceStructures);
        }
    }
}
