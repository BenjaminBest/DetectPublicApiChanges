using Microsoft.CodeAnalysis;

namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The interface ISyntaxNodeAnalyzer defines an analyzer which gets a syntax node and creates a unique key which represents some information.
    /// </summary>
    public interface ISyntaxNodeAnalyzer
    {
        /// <summary>
        /// Creates the item used in an index based on information gathered with the given <paramref name="syntaxNode"/>
        /// </summary>
        /// <param name="syntaxNode">The syntax node.</param>
        /// <returns></returns>
        IIndexItem CreateItem(SyntaxNode syntaxNode);

        /// <summary>
        /// Determines whether the declaration syntax type is supported.
        /// </summary>
        /// <param name="syntaxNode">The syntax node.</param>
        /// <returns>
        ///   <c>true</c> if the declaration syntax type is supported; otherwise, <c>false</c>.
        /// </returns>
        bool IsDeclarationSyntaxTypeSupported(SyntaxNode syntaxNode);
    }
}