using System;
using System.Collections;
using System.Linq;
using DetectPublicApiChanges.Analysis.Roslyn;
using DetectPublicApiChanges.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.SyntaxNodeAnalyzers
{
    /// <summary>
    /// The ClassSyntaxNodeAnalyzer analyzes a specific information in the syntax tree and creates a unique key which represents this information.
    /// </summary>
    public class PartialClassSyntaxNodeAnalyzer : ISyntaxNodeAnalyzer
    {
        /// <summary>
        /// Contains all found partial classes to make sure the class syntax itself is only reported once
        /// </summary>
        private static readonly Hashtable _partialClasses = new Hashtable();

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
            DiagnosticId = "PartialClassMissing",
            Category = "Class"
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassSyntaxNodeAnalyzer"/> class.
        /// </summary>
        /// <param name="indexItemFactory">The index item factory.</param>
        public PartialClassSyntaxNodeAnalyzer(IIndexItemFactory indexItemFactory)
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
            var node = syntaxNode as ClassDeclarationSyntax;

            if (node == null)
                throw new ArgumentException("syntaxNode has not the correct type to be analyzed.");

            AddPartial(node);

            return _indexItemFactory.CreateItem(CreateKey(node), syntaxNode, Descriptor.AddDescription($"The partial class {node.Identifier.ValueText} seems to be have been changed or removed"));
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
            var classSyntax = syntaxNode as ClassDeclarationSyntax;

            return IsNewPartialClass(classSyntax);
        }

        /// <summary>
        /// Creates the key.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        private static string CreateKey(ClassDeclarationSyntax syntax)
        {
            return syntax.GetFullName();
        }

        /// <summary>
        /// Determines whether [is new partial class] [the specified syntax].
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns>
        ///   <c>true</c> if [is new partial class] [the specified syntax]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsNewPartialClass(ClassDeclarationSyntax syntax)
        {
            var isPartial = syntax != null && syntax.Modifiers.Any(m => m.ValueText.Equals("partial"));

            var id = CreateKey(syntax);

            if (!isPartial)
                return false;

            return !_partialClasses.ContainsKey(id);
        }

        /// <summary>
        /// Adds the partial.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        private static void AddPartial(ClassDeclarationSyntax syntax)
        {
            _partialClasses.Add(CreateKey(syntax), string.Empty);
        }
    }
}
