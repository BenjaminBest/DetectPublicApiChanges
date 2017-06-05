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
        /// Gets the identifier.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        public static string GetId(this ClassDeclarationSyntax syntax)
        {
            return syntax.Identifier.ValueText;
        }

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
        /// Gets the full name.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        public static string GetFullName(this ClassDeclarationSyntax syntax)
        {
            NamespaceDeclarationSyntax namespaceDeclarationSyntax = null;
            if (!SyntaxNodeHelper.TryGetParentSyntax(syntax, out namespaceDeclarationSyntax))
                return string.Empty;

            var namespaceName = namespaceDeclarationSyntax.Name.ToString();
            var fullClassName = namespaceName + "." + syntax.Identifier;

            return fullClassName;
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
    }
}
