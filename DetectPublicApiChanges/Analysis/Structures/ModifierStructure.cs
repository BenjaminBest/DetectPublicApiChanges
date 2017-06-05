using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Analysis.Structures
{
    /// <summary>
    /// Parameter
    /// </summary>
    public class ModifierStructure : IStructure
    {
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
        /// Initializes a new instance of the <see cref="ModifierStructure"/> class.
        /// </summary>
        public ModifierStructure()
            : this(string.Empty)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModifierStructure"/> class.
        /// </summary>
        public ModifierStructure(string name)
        {
            Name = name;
        }
    }
}
