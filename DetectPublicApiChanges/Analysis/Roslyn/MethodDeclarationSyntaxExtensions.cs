using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.Roslyn
{
    /// <summary>
    /// Extension methods for ConstructorDeclarationSyntax
    /// </summary>
    public static class MethodDeclarationSyntaxExtensions
    {
        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public static IEnumerable<ParameterSyntax> GetParameters(this MethodDeclarationSyntax method)
        {
            var count = method.ParameterList.ChildNodes().Count();

            if (count == 0)
                return Enumerable.Empty<ParameterSyntax>();

            var parameters = method.ParameterList
                .ChildNodes()
                .OfType<ParameterSyntax>();

            return parameters;
        }

        /// <summary>
        /// Determines whether this instance is generic.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns>
        ///   <c>true</c> if the specified syntax is generic; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsGeneric(this MethodDeclarationSyntax syntax)
        {
            return syntax.TypeParameterList != null && syntax.TypeParameterList.Parameters.Count > 0;
        }

        /// <summary>
        /// Determines whether this instance is static.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns>
        ///   <c>true</c> if the specified syntax is static; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsStatic(this MethodDeclarationSyntax syntax)
        {
            return syntax.Modifiers.Any(m => m.ValueText.Equals("static"));
        }
    }
}
