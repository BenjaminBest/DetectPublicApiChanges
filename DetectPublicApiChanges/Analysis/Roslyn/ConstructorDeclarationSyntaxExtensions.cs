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
        /// Gets the full name.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        public static string GetFullName(this ConstructorDeclarationSyntax syntax)
        {
            var parentNameSpace = string.Empty;
            var classStructure = syntax.Parent as ClassDeclarationSyntax;
            if (classStructure != null)
                parentNameSpace = classStructure.GetFullName();
            else if (syntax.Parent is InterfaceDeclarationSyntax)
                parentNameSpace = ((InterfaceDeclarationSyntax)syntax.Parent).GetFullName();

            return parentNameSpace + "." + syntax.Identifier;
        }
    }
}
