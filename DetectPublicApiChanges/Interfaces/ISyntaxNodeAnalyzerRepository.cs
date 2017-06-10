using Microsoft.CodeAnalysis;

namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The interface ISyntaxNodeAnalyzerRepository defines a repository for syntax analyzers
    /// </summary>
    public interface ISyntaxNodeAnalyzerRepository
    {
        /// <summary>
        /// Analyzes the specified syntax node and creates a unique key
        /// </summary>
        /// <param name="syntaxNode">The syntax node.</param>
        /// <returns>An index item with the generated key</returns>
        IIndexItem Analyze(SyntaxNode syntaxNode);

        /// <summary>
        /// Determines whether the syntax declaration type is supported.
        /// </summary>
        /// <param name="syntaxNode">The syntax node.</param>
        /// <returns>
        ///   <c>true</c> if the syntax declaration type is supported; otherwise, <c>false</c>.
        /// </returns>
        bool IsSyntaxDeclarationTypeSupported(SyntaxNode syntaxNode);
    }
}
