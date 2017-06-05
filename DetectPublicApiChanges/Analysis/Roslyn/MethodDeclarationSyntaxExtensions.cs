using System;
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
        /// Gets the identifier.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        public static string GetId(this MethodDeclarationSyntax syntax)
        {
            return Guid.NewGuid().ToString();
        }

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
    }
}
