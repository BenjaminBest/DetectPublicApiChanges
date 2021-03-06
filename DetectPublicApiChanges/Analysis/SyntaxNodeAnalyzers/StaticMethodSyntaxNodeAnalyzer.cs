﻿using System;
using System.Text;
using DetectPublicApiChanges.Analysis.Obsoletes;
using DetectPublicApiChanges.Analysis.Roslyn;
using DetectPublicApiChanges.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.SyntaxNodeAnalyzers
{
    /// <summary>
    /// The MethodSyntaxNodeAnalyzer analyzes a specific information in the syntax tree and creates a unique key which represents this information.
    /// </summary>
    public class StaticMethodSyntaxNodeAnalyzer : ISyntaxNodeAnalyzer
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
            DiagnosticId = "StaticMethodMissing",
            Category = "Method"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodSyntaxNodeAnalyzer"/> class.
        /// </summary>
        /// <param name="indexItemFactory">The index item factory.</param>
        public StaticMethodSyntaxNodeAnalyzer(IIndexItemFactory indexItemFactory)
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
            var node = syntaxNode as MethodDeclarationSyntax;

            if (node == null)
                throw new ArgumentException("syntaxNode has not the correct type to be analyzed.");

            return _indexItemFactory.CreateItem(CreateKey(node), syntaxNode, Descriptor.AddObsoleteInformation(ObsoleteSyntaxNodeAnalyzer.GetObsoleteInformation(syntaxNode)).AddDescription($"The static method {node.Identifier.ValueText} seems to be have been changed or removed"));
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
            var item = syntaxNode as MethodDeclarationSyntax;

            return item != null && !item.IsGeneric() && item.IsStatic();
        }

        /// <summary>
        /// Creates the key.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        private static string CreateKey(MethodDeclarationSyntax syntax)
        {
            var key = new StringBuilder(syntax.ReturnType.ToString());

            key.Append(syntax.GetFullName());

            foreach (var param in syntax.GetParameters())
            {
                key.Append(param.Modifiers);
                key.Append(param.Type);
                key.Append(param.Identifier.ValueText);
            }

            return key.ToString();
        }
    }
}
