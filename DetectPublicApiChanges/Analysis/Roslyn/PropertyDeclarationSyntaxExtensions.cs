using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.Roslyn
{
    /// <summary>
    /// Extension methods for PropertyDeclarationSyntax
    /// </summary>
    public static class PropertyDeclarationSyntaxExtensions
    {
        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <param name="syntax">The syntax.</param>
        /// <returns></returns>
        public static string GetFullName(this PropertyDeclarationSyntax syntax)
        {
            var parentNameSpace = string.Empty;
            var classStructure = syntax.Parent as ClassDeclarationSyntax;
            if (classStructure != null)
                parentNameSpace = classStructure.GetFullName();
            else if (syntax.Parent is InterfaceDeclarationSyntax)
                parentNameSpace = ((InterfaceDeclarationSyntax)syntax.Parent).GetFullName();
            else if (syntax.Parent is StructDeclarationSyntax)
                parentNameSpace = ((StructDeclarationSyntax)syntax.Parent).GetFullName();

            return parentNameSpace + "." + syntax.Identifier;
        }
    }
}
