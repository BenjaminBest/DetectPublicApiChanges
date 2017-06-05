using System.Collections.Generic;
using System.Linq;
using DetectPublicApiChanges.Analysis.Roslyn;
using DetectPublicApiChanges.Analysis.Structures;
using DetectPublicApiChanges.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.StructureBuilders
{
    /// <summary>
    /// The ParameterMethodStructureBuilder creates a structure based on a parameter syntax
    /// </summary>
    public class ConstructorStructureBuilder : ITypeStructureBuilder<ClassDeclarationSyntax, ConstructorStructure>
    {
        /// <summary>
        /// The parameter builder
        /// </summary>
        private readonly IBaseMethodStructureBuilder<ConstructorDeclarationSyntax, ParameterStructure> _parameterBuilder;

        /// <summary>
        /// The modifier builder
        /// </summary>
        private readonly IBaseMethodStructureBuilder<ConstructorDeclarationSyntax, ModifierStructure> _modifierBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorStructureBuilder"/> class.
        /// </summary>
        /// <param name="parameterBuilder">The parameter builder.</param>
        /// <param name="modifierBuilder">The modifier builder.</param>
        public ConstructorStructureBuilder(
            IBaseMethodStructureBuilder<ConstructorDeclarationSyntax, ParameterStructure> parameterBuilder,
            IBaseMethodStructureBuilder<ConstructorDeclarationSyntax, ModifierStructure> modifierBuilder)
        {
            _parameterBuilder = parameterBuilder;
            _modifierBuilder = modifierBuilder;
        }

        /// <summary>
        /// Builds the specified syntax.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        public IEnumerable<ConstructorStructure> Build(ClassDeclarationSyntax syntax)
        {
            return syntax.GetConstructors().Select(c => new ConstructorStructure()
            {
                Parameters = _parameterBuilder.Build(c) ?? Enumerable.Empty<ParameterStructure>(),
                Modifiers = _modifierBuilder.Build(c) ?? Enumerable.Empty<ModifierStructure>()
            });
        }
    }
}
