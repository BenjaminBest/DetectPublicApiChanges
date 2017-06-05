using System.Collections.Generic;
using System.Linq;
using DetectPublicApiChanges.Analysis.Roslyn;
using DetectPublicApiChanges.Analysis.Structures;
using DetectPublicApiChanges.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.StructureBuilders
{
    /// <summary>
    /// The ClassStructureBuilder creates a structure based on a property syntax
    /// </summary>
    public class InterfaceStructureBuilder : IRootTypeStructureBuilder<SyntaxTree, InterfaceStructure>
    {
        /// <summary>
        /// The modifier builder
        /// </summary>
        private readonly ITypeStructureBuilder<InterfaceDeclarationSyntax, ModifierStructure> _modifierBuilder;

        /// <summary>
        /// The method builder
        /// </summary>
        private readonly ITypeStructureBuilder<InterfaceDeclarationSyntax, MethodStructure> _methodBuilder;

        /// <summary>
        /// The property builder
        /// </summary>
        private readonly ITypeStructureBuilder<InterfaceDeclarationSyntax, PropertyStructure> _propertyBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassStructureBuilder"/> class.
        /// </summary>
        /// <param name="modifierBuilder">The modifier builder.</param>
        /// <param name="methodBuilder">The method builder.</param>
        /// <param name="propertyBuilder">The property builder.</param>
        public InterfaceStructureBuilder(
            ITypeStructureBuilder<InterfaceDeclarationSyntax, ModifierStructure> modifierBuilder,
            ITypeStructureBuilder<InterfaceDeclarationSyntax, MethodStructure> methodBuilder,
            ITypeStructureBuilder<InterfaceDeclarationSyntax, PropertyStructure> propertyBuilder)
        {
            _modifierBuilder = modifierBuilder;
            _methodBuilder = methodBuilder;
            _propertyBuilder = propertyBuilder;
        }

        /// <summary>
        /// Builds the specified tree.
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<InterfaceStructure> Build(SyntaxTree tree)
        {
            //Syntax Nod
            var syntaxNode = tree?.GetRoot();

            if (syntaxNode == null)
                return Enumerable.Empty<InterfaceStructure>();

            return syntaxNode.DescendantNodes().OfType<InterfaceDeclarationSyntax>()
                .Select(i => new InterfaceStructure(i.GetName(), i.GetFullName())
                {
                    Modifiers = _modifierBuilder.Build(i) ?? Enumerable.Empty<ModifierStructure>(),
                    Methods = _methodBuilder.Build(i) ?? Enumerable.Empty<MethodStructure>(),
                    Properties = _propertyBuilder.Build(i) ?? Enumerable.Empty<PropertyStructure>()
                });
        }
    }
}