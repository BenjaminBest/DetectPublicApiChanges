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
        /// Gets the full name.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        public static string GetFullName(this MethodDeclarationSyntax syntax)
        {
            var name = string.Empty;
            var classStructure = syntax.Parent as ClassDeclarationSyntax;
            if (classStructure != null)
                name = classStructure.GetFullName();
            else if (syntax.Parent is InterfaceDeclarationSyntax)
                name = ((InterfaceDeclarationSyntax)syntax.Parent).GetFullName();
            else if (syntax.Parent is StructDeclarationSyntax)
                name = ((StructDeclarationSyntax)syntax.Parent).GetFullName();

            name = name + "." + syntax.Identifier;

            if (syntax.IsGeneric())
                name = name + syntax.TypeParameterList.ToFullString();

            return name;
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
    }
}
