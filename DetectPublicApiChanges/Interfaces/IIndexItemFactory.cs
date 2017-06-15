using Microsoft.CodeAnalysis;

namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The interface IIndexItemFactory defines a factory for the index item creation
    /// </summary>
    public interface IIndexItemFactory
    {
        /// <summary>
        /// Creates the specified item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="syntaxNode">The syntax node.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        IIndexItem CreateItem(string key, SyntaxNode syntaxNode, IDiagnosticAnalyzerDescriptor description);
    }
}