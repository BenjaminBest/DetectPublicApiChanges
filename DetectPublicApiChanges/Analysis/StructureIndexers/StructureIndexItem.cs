using DetectPublicApiChanges.Interfaces;

namespace DetectPublicApiChanges.Analysis.StructureIndexers
{
    /// <summary>
    /// The StructureIndexItem is used to hold a structure and some id to identify
    /// </summary>
    public class StructureIndexItem : IStructureIndexItem
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key
        {
            get;
        }

        /// <summary>
        /// Gets or sets the structure.
        /// </summary>
        /// <value>
        /// The structure.
        /// </value>
        public IStructure Structure
        {
            get;
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public IStructure Parent
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureIndexItem"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="structure">The structure.</param>
        /// <param name="parent">The parent structure</param>
        public StructureIndexItem(string key, IStructure structure, IStructure parent)
        {
            Key = key;
            Structure = structure;
            Parent = parent;
        }
    }
}
