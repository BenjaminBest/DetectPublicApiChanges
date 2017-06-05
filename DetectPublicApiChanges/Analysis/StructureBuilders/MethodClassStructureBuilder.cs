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
    public class MethodClassStructureBuilder : ITypeStructureBuilder<ClassDeclarationSyntax, MethodStructure>
    {
        /// <summary>
        /// The parameter builder
        /// </summary>
        private readonly IBaseMethodStructureBuilder<MethodDeclarationSyntax, ParameterStructure> _parameterBuilder;

        /// <summary>
        /// The modifier builder
        /// </summary>
        private readonly IBaseMethodStructureBuilder<MethodDeclarationSyntax, ModifierStructure> _modifierBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodClassStructureBuilder"/> class.
        /// </summary>
        /// <param name="parameterBuilder">The parameter builder.</param>
        /// <param name="modifierBuilder">The modifier builder.</param>
        public MethodClassStructureBuilder(
            IBaseMethodStructureBuilder<MethodDeclarationSyntax, ParameterStructure> parameterBuilder,
            IBaseMethodStructureBuilder<MethodDeclarationSyntax, ModifierStructure> modifierBuilder)
        {
            _parameterBuilder = parameterBuilder;
            _modifierBuilder = modifierBuilder;
        }

        /// <summary>
        /// Builds the specified syntax.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        public IEnumerable<MethodStructure> Build(ClassDeclarationSyntax syntax)
        {
            return syntax.GetMethods()
                .Select(m => new MethodStructure(m.Identifier.Text, m.ReturnType.ToString())
                {
                    Modifiers = _modifierBuilder.Build(m) ?? Enumerable.Empty<ModifierStructure>(),
                    Parameters = _parameterBuilder.Build(m) ?? Enumerable.Empty<ParameterStructure>()
                });
        }
    }
}
