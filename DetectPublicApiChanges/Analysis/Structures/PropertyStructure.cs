using System.Collections.Generic;
using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Analysis.Structures
{
    /// <summary>
    /// Property
    /// </summary>
    public class PropertyStructure : IStructure
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type
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
        /// Gets or sets the modifiers.
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
        /// Initializes a new instance of the <see cref="PropertyStructure"/> class.
        /// </summary>
        public PropertyStructure()
            : this(string.Empty, string.Empty)
        {
            Modifiers = new List<ModifierStructure>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyStructure"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        public PropertyStructure(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }
}
