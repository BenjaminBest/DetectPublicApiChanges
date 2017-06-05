using System.Collections.Generic;
using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Analysis.Structures
{
    /// <summary>
    /// Constructor
    /// </summary>
    public class ConstructorStructure : IStructure
    {
        /// <summary>
        /// Gets the parameters.
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
        /// Initializes a new instance of the <see cref="ConstructorStructure"/> class.
        /// </summary>
        public ConstructorStructure()
        {
            Parameters = new List<ParameterStructure>();
            Modifiers = new List<ModifierStructure>();
        }
    }
}
