using System.Collections.Generic;
using System.Linq;
using DetectPublicApiChanges.Analysis.Roslyn;
using DetectPublicApiChanges.Analysis.Structures;
using DetectPublicApiChanges.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.StructureBuilders
{
    /// <summary>
    /// The PropertyStructureBuilder creates a structure based on a property syntax
    /// </summary>
    public class PropertyInterfaceStructureBuilder : ITypeStructureBuilder<InterfaceDeclarationSyntax, PropertyStructure>
    {
        /// <summary>
        /// The modifier builder
        /// </summary>
        private readonly IBasePropertyStructureBuilder<PropertyDeclarationSyntax, ModifierStructure> _modifierBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyInterfaceStructureBuilder"/> class.
        /// </summary>
        /// <param name="modifierBuilder">The modifier builder.</param>
        public PropertyInterfaceStructureBuilder(IBasePropertyStructureBuilder<PropertyDeclarationSyntax, ModifierStructure> modifierBuilder)
        {
            _modifierBuilder = modifierBuilder;
        }

        /// <summary>
        /// Builds the specified syntax.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        public IEnumerable<PropertyStructure> Build(InterfaceDeclarationSyntax syntax)
        {
            return syntax.GetProperties()
                .Select(p => new PropertyStructure(p.Identifier.Text, p.Type.ToString())
                {
                    Modifiers = _modifierBuilder.Build(p)
                });
        }
    }
}