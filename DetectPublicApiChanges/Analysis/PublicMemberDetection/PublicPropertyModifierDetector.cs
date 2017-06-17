﻿using System.Linq;
using DetectPublicApiChanges.Analysis.Roslyn;
using DetectPublicApiChanges.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.PublicMemberDetection
{
    /// <summary>
    /// The PublicPropertyModifierDetector detects if the structure is of type <see cref="PropertyDeclarationSyntax" /> and is public
    /// </summary>
    /// <seealso cref="IPublicModifierDetector" />
    public class PublicPropertyModifierDetector : IPublicModifierDetector
    {
        /// <summary>
        /// Determines whether the specified structure is public.
        /// </summary>
        /// <param name="syntaxNode">The syntax node.</param>
        /// <returns>
        ///   <c>true</c> if the specified structure is public; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPublic(SyntaxNode syntaxNode)
        {
            var item = syntaxNode as PropertyDeclarationSyntax;

            return item != null && item.Modifiers.Select(m => m.ValueText).Any(m => m.Equals("public") || m.Equals("protected"));
        }

        /// <summary>
        /// Determines weather all parents of this node are public.
        /// </summary>
        /// <param name="syntaxNode">The syntax node.</param>
        /// <returns></returns>
        public bool IsHierarchyPublic(SyntaxNode syntaxNode)
        {
            return IsPublic(syntaxNode) && SyntaxNodeHelper.IsHierarchyPublic(syntaxNode.Parent);
        }
    }
}
