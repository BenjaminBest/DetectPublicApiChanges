using DetectPublicApiChanges.Interfaces;
using Microsoft.CodeAnalysis;

namespace DetectPublicApiChanges.Analysis.StructureIndex
{
    /// <summary>
    /// The IndexItem is used to hold a syntax node and some id to identify
    /// </summary>
    public class IndexItem : IIndexItem
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
        /// Gets or sets the syntax node.
        /// </summary>
        /// <value>
        /// The syntax node.
        /// </value>
        public SyntaxNode SyntaxNode
        {
            get;
        }

        /// <summary>
        /// Gets the project of the SyntaxNode.
        /// </summary>
        /// <value>
        /// The project.
        /// </value>
        public Project Project
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexItem" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="syntaxNode">The syntax node.</param>
        public IndexItem(string key, SyntaxNode syntaxNode)
        {
            Key = key;
            SyntaxNode = syntaxNode;
        }
    }
}
