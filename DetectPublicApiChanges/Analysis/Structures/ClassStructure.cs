using System.Collections.Generic;
using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Analysis.Structures
{
    /// <summary>
    /// Class
    /// </summary>
    public class ClassStructure : IStructure
    {
        /// <summary>
        /// Gets the methods.
        /// </summary>
        /// <value>
        /// The methods.
        /// </value>
        public IEnumerable<MethodStructure> Methods
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the constructors.
        /// </summary>
        /// <value>
        /// The constructors.
        /// </value>
        public IEnumerable<ConstructorStructure> Constructors
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        public IEnumerable<PropertyStructure> Properties
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the modifiers.
        /// </summary>
        /// <value>
        /// The modifiers.
        /// </value>
        public IEnumerable<ModifierStructure> Modifiers
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
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string FullName
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassStructure"/> class.
        /// </summary>
        public ClassStructure()
            : this(string.Empty, string.Empty)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassStructure" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="fullName">The namespace.</param>
        public ClassStructure(string name, string fullName)
        {
            Name = name;
            FullName = fullName;

            Methods = new List<MethodStructure>();
            Constructors = new List<ConstructorStructure>();
            Modifiers = new List<ModifierStructure>();
            Properties = new List<PropertyStructure>();
        }
    }
}
