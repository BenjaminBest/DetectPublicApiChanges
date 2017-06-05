namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The interface IStructureIndexItem defines an item to hold a structure and some id to identify
    /// </summary>
    public interface IStructureIndexItem
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        string Key { get; }

        /// <summary>
        /// Gets or sets the structure.
        /// </summary>
        /// <value>
        /// The structure.
        /// </value>
        IStructure Structure { get; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        IStructure Parent { get; }
    }
}