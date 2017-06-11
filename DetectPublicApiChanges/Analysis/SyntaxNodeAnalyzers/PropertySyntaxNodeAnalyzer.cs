using System;
using System.Text;
using DetectPublicApiChanges.Analysis.Roslyn;
using DetectPublicApiChanges.Analysis.StructureIndex;
using DetectPublicApiChanges.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.SyntaxNodeAnalyzers
{
    /// <summary>
    /// The PropertySyntaxNodeAnalyzer analyzes a specific information in the syntax tree and creates a unique key which represents this information.
    /// </summary>
    public class PropertySyntaxNodeAnalyzer : ISyntaxNodeAnalyzer
    {
        /// <summary>
        /// Gets the descriptor.
        /// </summary>
        /// <value>
        /// The descriptor.
        /// </value>
        private static IDiagnosticAnalyzerDescriptor Descriptor => new DiagnosticAnalyzerDescriptor()
        {
            DiagnosticId = "PropertyMissing",
            Category = "Property"
        };

        /// <summary>
        /// Creates the item used in an index based on information gathered with the given <paramref name="syntaxNode"/>
        /// </summary>
        /// <param name="syntaxNode">The syntax node.</param>
        /// <returns></returns>
        public IIndexItem CreateItem(SyntaxNode syntaxNode)
        {
            var node = syntaxNode as PropertyDeclarationSyntax;

            if (node == null)
                throw new ArgumentException("syntaxNode has not the correct type to be analyzed.");

            return new IndexItem(CreateKey(node), syntaxNode, Descriptor.AddDescription($"The property {node.Identifier.ValueText} seems to be have been changed or removed"));
        }

        /// <summary>
        /// Determines whether the declaration syntax type is supported.
        /// </summary>
        /// <param name="syntaxNode">The syntax node.</param>
        /// <returns>
        ///   <c>true</c> if the declaration syntax type is supported; otherwise, <c>false</c>.
        /// </returns>
        public bool IsDeclarationSyntaxTypeSupported(SyntaxNode syntaxNode)
        {
            return syntaxNode is PropertyDeclarationSyntax;
        }

        /// <summary>
        /// Creates the key.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        private static string CreateKey(PropertyDeclarationSyntax syntax)
        {
            var key = new StringBuilder(syntax.GetFullName());

            key.Append(syntax.Type);

            return key.ToString();
        }
    }
}
