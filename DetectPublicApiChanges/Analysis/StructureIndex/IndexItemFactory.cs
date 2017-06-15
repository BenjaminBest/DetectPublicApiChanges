using DetectPublicApiChanges.Interfaces;
using Microsoft.CodeAnalysis;

namespace DetectPublicApiChanges.Analysis.StructureIndex
{
    /// <summary>
    /// The IndexItemFactory is a factory for the index item creation
    /// </summary>
    /// <seealso cref="IIndexItemFactory" />
    public class IndexItemFactory : IIndexItemFactory
    {

        /// <summary>
        /// Creates the specified item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="syntaxNode">The syntax node.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public IIndexItem CreateItem(string key, SyntaxNode syntaxNode, IDiagnosticAnalyzerDescriptor description)
        {
            return new IndexItem(key, syntaxNode, description);
        }
    }
}
