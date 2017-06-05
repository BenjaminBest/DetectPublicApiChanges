﻿using System.Collections.Generic;
using System.Linq;
using DetectPublicApiChanges.Analysis.Structures;
using DetectPublicApiChanges.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.StructureBuilders
{
    /// <summary>
    /// The ModifierClassStructureBuilder creates a structure based on a property syntax
    /// </summary>
    public class ModifierClassStructureBuilder : ITypeStructureBuilder<ClassDeclarationSyntax, ModifierStructure>
    {
        /// <summary>
        /// Builds the specified syntax.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        public IEnumerable<ModifierStructure> Build(ClassDeclarationSyntax syntax)
        {
            return syntax.Modifiers
                .Select(p => new ModifierStructure(p.ValueText));
        }
    }
}