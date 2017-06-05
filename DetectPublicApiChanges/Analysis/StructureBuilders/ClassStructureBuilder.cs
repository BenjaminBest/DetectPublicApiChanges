using System.Collections.Generic;
using System.Linq;
using DetectPublicApiChanges.Analysis.Roslyn;
using DetectPublicApiChanges.Analysis.Structures;
using DetectPublicApiChanges.Extensions;
using DetectPublicApiChanges.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.StructureBuilders
{
    /// <summary>
    /// The ClassStructureBuilder creates a structure based on a property syntax
    /// </summary>
    public class ClassStructureBuilder : IRootTypeStructureBuilder<SyntaxTree, ClassStructure>
    {
        /// <summary>
        /// The modifier builder
        /// </summary>
        private readonly ITypeStructureBuilder<ClassDeclarationSyntax, ModifierStructure> _modifierBuilder;

        /// <summary>
        /// The method builder
        /// </summary>
        private readonly ITypeStructureBuilder<ClassDeclarationSyntax, MethodStructure> _methodBuilder;

        /// <summary>
        /// The property builder
        /// </summary>
        private readonly ITypeStructureBuilder<ClassDeclarationSyntax, PropertyStructure> _propertyBuilder;

        /// <summary>
        /// The constructor builder
        /// </summary>
        private readonly ITypeStructureBuilder<ClassDeclarationSyntax, ConstructorStructure> _constructorBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassStructureBuilder"/> class.
        /// </summary>
        /// <param name="modifierBuilder">The modifier builder.</param>
        /// <param name="methodBuilder">The method builder.</param>
        /// <param name="propertyBuilder">The property builder.</param>
        /// <param name="constructorBuilder">The con structure builder.</param>
        public ClassStructureBuilder(
            ITypeStructureBuilder<ClassDeclarationSyntax, ModifierStructure> modifierBuilder,
            ITypeStructureBuilder<ClassDeclarationSyntax, MethodStructure> methodBuilder,
            ITypeStructureBuilder<ClassDeclarationSyntax, PropertyStructure> propertyBuilder,
            ITypeStructureBuilder<ClassDeclarationSyntax, ConstructorStructure> constructorBuilder)
        {
            _modifierBuilder = modifierBuilder;
            _methodBuilder = methodBuilder;
            _propertyBuilder = propertyBuilder;
            _constructorBuilder = constructorBuilder;
        }

        /// <summary>
        /// Builds the specified tree.
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <param name="syntax"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<ClassStructure> Build(SyntaxTree tree)
        {
            //Syntax Node
            var syntaxNode = tree?.GetRoot();

            if (syntaxNode == null)
                return Enumerable.Empty<ClassStructure>();

            var items = syntaxNode.DescendantNodes().OfType<ClassDeclarationSyntax>()
                .Select(c => new ClassStructure(c.GetName(), c.GetFullName())
                {
                    Modifiers = _modifierBuilder.Build(c) ?? Enumerable.Empty<ModifierStructure>(),
                    Methods = _methodBuilder.Build(c) ?? Enumerable.Empty<MethodStructure>(),
                    Constructors = _constructorBuilder.Build(c) ?? Enumerable.Empty<ConstructorStructure>(),
                    Properties = _propertyBuilder.Build(c) ?? Enumerable.Empty<PropertyStructure>()
                }).Log();

            return items;
        }
    }
}