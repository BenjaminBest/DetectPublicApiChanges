using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.Roslyn
{
    /// <summary>
    /// Extension methods for ClassDeclarationSyntax
    /// </summary>
    public static class ClassDeclarationSyntaxExtensions
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        public static string GetName(this ClassDeclarationSyntax syntax)
        {
            return syntax.Identifier.ValueText;
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        public static IEnumerable<PropertyDeclarationSyntax> GetProperties(this ClassDeclarationSyntax syntax)
        {
            var properties = syntax
                .ChildNodes()
                .OfType<PropertyDeclarationSyntax>();

            return properties;
        }

        /// <summary>
        /// Gets the methods.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        public static IEnumerable<MethodDeclarationSyntax> GetMethods(this ClassDeclarationSyntax syntax)
        {
            var methods = syntax
                .ChildNodes()
                .OfType<MethodDeclarationSyntax>();

            return methods;
        }

        /// <summary>
        /// Gets the constructors.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        public static IEnumerable<ConstructorDeclarationSyntax> GetConstructors(this ClassDeclarationSyntax syntax)
        {
            var ctors = syntax
                .ChildNodes()
                .OfType<ConstructorDeclarationSyntax>();

            return ctors;
        }

        /// <summary>
        /// Determines whether this instance is generic.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns>
        ///   <c>true</c> if the specified syntax is generic; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsGeneric(this ClassDeclarationSyntax syntax)
        {
            return syntax.TypeParameterList != null && syntax.TypeParameterList.Parameters.Count > 0;
        }
    }
}
