using System.Linq;
using DetectPublicApiChanges.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.PublicMemberDetection
{
    /// <summary>
    /// The PublicMethodModifierDetector detects if the structure is of type <see cref="MethodDeclarationSyntax"/> and is public
    /// </summary>
    public class PublicMethodModifierDetector : IPublicModifierDetector
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
            var item = syntaxNode as MethodDeclarationSyntax;

            return item != null && item.Modifiers.Select(m => m.ValueText).Any(m => m.ToLower().Equals("public") || m.ToLower().Equals("protected"));
        }
    }
}
