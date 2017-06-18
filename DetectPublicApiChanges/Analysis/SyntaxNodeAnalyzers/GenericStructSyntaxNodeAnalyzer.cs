using System;
using DetectPublicApiChanges.Analysis.Roslyn;
using DetectPublicApiChanges.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.SyntaxNodeAnalyzers
{
    /// <summary>
    /// The ClassSyntaxNodeAnalyzer analyzes a specific information in the syntax tree and creates a unique key which represents this information.
    /// </summary>
    public class GenericStructSyntaxNodeAnalyzer : ISyntaxNodeAnalyzer
    {
        /// <summary>
        /// The index item factory
        /// </summary>
        private readonly IIndexItemFactory _indexItemFactory;

        /// <summary>
        /// Gets the descriptor.
        /// </summary>
        /// <value>
        /// The descriptor.
        /// </value>
        private static IDiagnosticAnalyzerDescriptor Descriptor => new DiagnosticAnalyzerDescriptor()
        {
            DiagnosticId = "GenericStructMissing",
            Category = "Struct"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="StructSyntaxNodeAnalyzer"/> class.
        /// </summary>
        /// <param name="indexItemFactory">The index item factory.</param>
        public GenericStructSyntaxNodeAnalyzer(IIndexItemFactory indexItemFactory)
        {
            _indexItemFactory = indexItemFactory;
        }

        /// <summary>
        /// Creates the item used in an index based on information gathered with the given <paramref name="syntaxNode"/>
        /// </summary>
        /// <param name="syntaxNode">The syntax node.</param>
        /// <returns></returns>
        public IIndexItem CreateItem(SyntaxNode syntaxNode)
        {
            var node = syntaxNode as StructDeclarationSyntax;

            if (node == null)
                throw new ArgumentException("syntaxNode has not the correct type to be analyzed.");

            return _indexItemFactory.CreateItem(CreateKey(node), syntaxNode, Descriptor.AddDescription($"The generic struct {node.Identifier.ValueText} seems to be have been changed or removed"));
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
            var item = syntaxNode as StructDeclarationSyntax;

            return item != null && item.IsGeneric();
        }

        /// <summary>
        /// Creates the key.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        private static string CreateKey(StructDeclarationSyntax syntax)
        {
            return syntax.GetFullName() + syntax.TypeParameterList.ToFullString();
        }
    }
}
