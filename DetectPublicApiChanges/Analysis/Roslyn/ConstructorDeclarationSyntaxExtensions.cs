using System;
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
        /// Gets the identifier.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        public static string GetId(this ConstructorDeclarationSyntax syntax)
        {
            var parameterCount = syntax.ParameterList.ChildNodes().Count();

            return parameterCount == 0 ? "Default" : Guid.NewGuid().ToString();
        }

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
    }
}
