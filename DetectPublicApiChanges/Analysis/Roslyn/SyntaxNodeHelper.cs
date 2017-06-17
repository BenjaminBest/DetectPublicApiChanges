using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.Roslyn
{
    /// <summary>
    /// The SyntaxNodeHelper recursivly tries to reach the most parent type 
    /// </summary>
    public static class SyntaxNodeHelper
    {
        /// <summary>
        /// Tries to get the parent syntax.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="syntaxNode">The syntax node.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static bool TryGetParentSyntax<T>(SyntaxNode syntaxNode, out T result)
                    where T : SyntaxNode
        {
            // set defaults
            result = null;

            if (syntaxNode == null)
            {
                return false;
            }

            try
            {
                syntaxNode = syntaxNode.Parent;

                if (syntaxNode == null)
                {
                    return false;
                }

                if (syntaxNode.GetType() == typeof(T))
                {
                    result = syntaxNode as T;
                    return true;
                }

                return TryGetParentSyntax<T>(syntaxNode, out result);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether this and parent nodes are public.
        /// </summary>
        /// <param name="syntaxNode">The syntax node.</param>
        /// <returns>
        ///   <c>true</c> if this and parent nodes are public; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsHierarchyPublic(SyntaxNode syntaxNode)
        {
            while (true)
            {
                SyntaxNode parent = null;
                if (syntaxNode is ClassDeclarationSyntax)
                {
                    var classNode = syntaxNode as ClassDeclarationSyntax;

                    if (!classNode.Modifiers.Any(m => m.ValueText.Equals("public") || m.ValueText.Equals("protected")))
                        return false;

                    parent = classNode.Parent;
                }

                if (syntaxNode is InterfaceDeclarationSyntax)
                {
                    var interfaceNode = syntaxNode as InterfaceDeclarationSyntax;

                    if (!interfaceNode.Modifiers.Any(m => m.ValueText.Equals("public") || m.ValueText.Equals("protected")))
                        return false;

                    parent = interfaceNode.Parent;
                }

                if (parent == null)
                    return true;

                if (parent is NamespaceDeclarationSyntax)
                    return true;

                syntaxNode = parent;
            }
        }
    }
}