using Microsoft.CodeAnalysis;

namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The interface IPublicModifierDetector detects if the structure is public and should be analyzing for change detection
    /// </summary>
    public interface IPublicModifierDetector
    {
        /// <summary>
        /// Determines whether the specified structure is public.
        /// </summary>
        /// <param name="syntaxNode">The syntax node.</param>
        /// <returns>
        ///   <c>true</c> if the specified structure is public; otherwise, <c>false</c>.
        /// </returns>
        bool IsPublic(SyntaxNode syntaxNode);

        /// <summary>
        /// Determines weather all parents of this node are public.
        /// </summary>
        /// <param name="syntaxNode">The syntax node.</param>
        /// <returns></returns>
        bool IsHierarchyPublic(SyntaxNode syntaxNode);
    }
}