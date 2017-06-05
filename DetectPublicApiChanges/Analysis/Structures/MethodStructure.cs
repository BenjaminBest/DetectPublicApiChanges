using System.Collections.Generic;
using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Analysis.Structures
{
    /// <summary>
    /// Method
    /// </summary>
    public class MethodStructure : IStructure
    {
        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public IEnumerable<ParameterStructure> Parameters
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
        /// Gets the type of the return.
        /// </summary>
        /// <value>
        /// The type of the return.
        /// </value>
        public string ReturnType
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
        /// Initializes a new instance of the <see cref="MethodStructure"/> class.
        /// </summary>
        public MethodStructure()
            : this(string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodStructure" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="returnType">Type of the return.</param>
        public MethodStructure(string name, string returnType)
        {
            Name = name;
            ReturnType = returnType;
            Parameters = new List<ParameterStructure>();
            Modifiers = new List<ModifierStructure>();
        }
    }
}