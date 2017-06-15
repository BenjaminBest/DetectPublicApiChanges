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
        /// Gets the full name.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        public static string GetFullName(this StructDeclarationSyntax syntax)
        {
            NamespaceDeclarationSyntax namespaceDeclarationSyntax;
            if (!SyntaxNodeHelper.TryGetParentSyntax(syntax, out namespaceDeclarationSyntax))
                return string.Empty;


            var namespaceName = namespaceDeclarationSyntax.Name.ToString();
            var fullClassName = namespaceName + "." + syntax.Identifier;

            return fullClassName;
        }
    }
}
