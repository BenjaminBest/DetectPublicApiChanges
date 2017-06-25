using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.Roslyn
{
    /// <summary>
    /// Extension methods for ConstructorDeclarationSyntax
    /// </summary>
    public static class ConstructorDeclarationSyntaxExtensions
    {
        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        public static IEnumerable<ParameterSyntax> GetParameters(this ConstructorDeclarationSyntax syntax)
        {
            var count = syntax.ParameterList.ChildNodes().Count();

            if (count == 0)
                return Enumerable.Empty<ParameterSyntax>();

            var parameters = syntax.ParameterList
                .ChildNodes()
                .OfType<ParameterSyntax>();

            return parameters;
        }

        /// <summary>
        /// Determines whether this instance is static.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns>
        ///   <c>true</c> if the specified syntax is static; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsStatic(this ConstructorDeclarationSyntax syntax)
        {
            return syntax.Modifiers.Any(m => m.ValueText.Equals("static"));
        }
    }
}
