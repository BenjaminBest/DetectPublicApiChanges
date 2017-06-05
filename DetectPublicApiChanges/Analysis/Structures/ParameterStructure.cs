using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Analysis.Structures
{
    /// <summary>
    /// Parameter
    /// </summary>
    public class ParameterStructure : IStructure
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
        /// Initializes a new instance of the <see cref="ParameterStructure"/> class.
        /// </summary>
        public ParameterStructure()
            : this(string.Empty, string.Empty)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterStructure"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        public ParameterStructure(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }
}
