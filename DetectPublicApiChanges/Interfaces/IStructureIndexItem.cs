using Microsoft.CodeAnalysis;

namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The interface IIndexItem defines an item to hold a syntax node and some id to identify
    /// </summary>
    public interface IIndexItem
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        string Key { get; }

        /// <summary>
        /// Gets or sets the syntax node.
        /// </summary>
        /// <value>
        /// The syntax node.
        /// </value>
        SyntaxNode SyntaxNode { get; }

        /// <summary>
        /// Gets the project of the SyntaxNode.
        /// </summary>
        /// <value>
        /// The project.
        /// </value>
        Project Project { get; set; }
    }
}