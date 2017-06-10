using System.Collections.Generic;
using System.Linq;
using DetectPublicApiChanges.Interfaces;
using Microsoft.CodeAnalysis;

namespace DetectPublicApiChanges.Analysis.SyntaxNodeAnalyzers
{
    /// <summary>
    /// The SyntaxNodeAnalyzerRepository holds all syntax analyzers
    /// </summary>
    public class SyntaxNodeAnalyzerRepository : ISyntaxNodeAnalyzerRepository
    {
        /// <summary>
        /// The syntax node analyzers
        /// </summary>
        private readonly IEnumerable<ISyntaxNodeAnalyzer> _syntaxNodeAnalyzers;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyntaxNodeAnalyzerRepository"/> class.
        /// </summary>
        /// <param name="syntaxNodeAnalyzers">The syntax node analyzers.</param>
        public SyntaxNodeAnalyzerRepository(IEnumerable<ISyntaxNodeAnalyzer> syntaxNodeAnalyzers)
        {
            _syntaxNodeAnalyzers = syntaxNodeAnalyzers;
        }

        /// <summary>
        /// Analyzes the specified syntax node.
        /// </summary>
        /// <param name="syntaxNode">The syntax node.</param>
        /// <returns>
        /// An index item with the generated key
        /// </returns>
        public IIndexItem Analyze(SyntaxNode syntaxNode)
        {
            foreach (var analyzer in _syntaxNodeAnalyzers)
            {
                if (analyzer.IsDeclarationSyntaxTypeSupported(syntaxNode))
                    return analyzer.CreateItem(syntaxNode);
            }

            return null;
        }

        /// <summary>
        /// Determines whether the syntax declaration type is supported.
        /// </summary>
        /// <param name="syntaxNode">The syntax node.</param>
        /// <returns>
        ///   <c>true</c> if the syntax declaration type is supported; otherwise, <c>false</c>.
        /// </returns>
        public bool IsSyntaxDeclarationTypeSupported(SyntaxNode syntaxNode)
        {
            return _syntaxNodeAnalyzers.Any(a => a.IsDeclarationSyntaxTypeSupported(syntaxNode));
        }
    }
}
