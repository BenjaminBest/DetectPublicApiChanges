using System.Collections.Generic;
using System.Linq;
using DetectPublicApiChanges.Analysis.Structures;
using DetectPublicApiChanges.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.StructureBuilders
{
    /// <summary>
    /// The ModifierConstructorStructureBuilder creates a structure based on a parameter syntax
    /// </summary>
    public class ModifierConstructorStructureBuilder : IBaseMethodStructureBuilder<ConstructorDeclarationSyntax, ModifierStructure>
    {
        /// <summary>
        /// Builds the specified syntax.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        public IEnumerable<ModifierStructure> Build(ConstructorDeclarationSyntax syntax)
        {
            return syntax.Modifiers.Select(p => new ModifierStructure(p.ValueText));
        }
    }
}
