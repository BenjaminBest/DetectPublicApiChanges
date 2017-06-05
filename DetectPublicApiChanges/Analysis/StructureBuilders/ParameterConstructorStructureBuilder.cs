using System.Collections.Generic;
using System.Linq;
using DetectPublicApiChanges.Analysis.Roslyn;
using DetectPublicApiChanges.Analysis.Structures;
using DetectPublicApiChanges.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.StructureBuilders
{
    /// <summary>
    /// The ParameterConstructorStructureBuilder creates a structure based on a parameter syntax
    /// </summary>
    public class ParameterConstructorStructureBuilder : IBaseMethodStructureBuilder<ConstructorDeclarationSyntax, ParameterStructure>
    {
        /// <summary>
        /// Builds the specified syntax.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        public IEnumerable<ParameterStructure> Build(ConstructorDeclarationSyntax syntax)
        {
            return syntax.GetParameters().Select(p => new ParameterStructure(p.Identifier.Text, p.Type.ToString()));
        }
    }
}
