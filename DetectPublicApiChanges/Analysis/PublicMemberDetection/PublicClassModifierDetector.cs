using System.Linq;
using DetectPublicApiChanges.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.PublicMemberDetection
{
    /// <summary>
    /// The PublicClassModifierDetector detects if the structure is of type <see cref="ClassDeclarationSyntax"/> and is public
    /// </summary>
    public class PublicClassModifierDetector : IPublicModifierDetector
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
            var item = syntaxNode as ClassDeclarationSyntax;

            return item != null && item.Modifiers.Any(m => m.ValueText.ToLower().Equals("public") || m.ValueText.ToLower().Equals("protected"));
        }
    }
}
