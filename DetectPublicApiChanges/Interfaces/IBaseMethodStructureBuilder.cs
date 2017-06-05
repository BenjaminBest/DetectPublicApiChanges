﻿using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The  interface IBaseMethodStructureBuilder defines methods to convert or build a declaration syntax into the internal object structure
    /// </summary>
    /// <typeparam name="TBaseMethodDeclarationSyntax">The type of the declaration syntax.</typeparam>
    /// <typeparam name="TStructure">The type of the property structure.</typeparam>
    public interface IBaseMethodStructureBuilder<in TBaseMethodDeclarationSyntax, out TStructure>
        where TBaseMethodDeclarationSyntax : BaseMethodDeclarationSyntax
        where TStructure : class
    {
        IEnumerable<TStructure> Build(TBaseMethodDeclarationSyntax syntax);
    }
}