using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.Roslyn
{
    /// <summary>
    /// Extension methods for StructDeclarationSyntax
    /// </summary>
    public static class StructDeclarationSyntaxExtensions
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        public static string GetName(this StructDeclarationSyntax syntax)
        {
            return syntax.Identifier.ValueText;
        }

        /// <summary>
        /// Determines whether this instance is generic.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns>
        ///   <c>true</c> if the specified syntax is generic; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsGeneric(this StructDeclarationSyntax syntax)
        {
            return syntax.TypeParameterList != null && syntax.TypeParameterList.Parameters.Count > 0;
        }
    }
}
