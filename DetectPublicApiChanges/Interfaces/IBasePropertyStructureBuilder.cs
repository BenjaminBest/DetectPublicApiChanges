using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The  interface IBasePropertyStructureBuilder defines methods to convert or build a declaration syntax into the internal object structure
    /// </summary>
    /// <typeparam name="TBasePropertyDeclarationSyntax">The type of the declaration syntax.</typeparam>
    /// <typeparam name="TStructure">The type of the property structure.</typeparam>
    public interface IBasePropertyStructureBuilder<in TBasePropertyDeclarationSyntax, out TStructure>
        where TBasePropertyDeclarationSyntax : BasePropertyDeclarationSyntax
        where TStructure : class
    {
        IEnumerable<TStructure> Build(TBasePropertyDeclarationSyntax syntax);
    }
}