using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The  interface ITypeStructureBuilder defines methods to convert or build a declaration syntax into the internal object structure
    /// </summary>
    /// <typeparam name="TTypeDeclarationSyntax">The type of the declaration syntax.</typeparam>
    /// <typeparam name="TStructure">The type of the property structure.</typeparam>
    public interface ITypeStructureBuilder<in TTypeDeclarationSyntax, out TStructure>
        where TTypeDeclarationSyntax : TypeDeclarationSyntax
        where TStructure : class
    {
        IEnumerable<TStructure> Build(TTypeDeclarationSyntax syntax);
    }
}