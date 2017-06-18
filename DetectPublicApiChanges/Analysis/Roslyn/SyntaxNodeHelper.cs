using Microsoft.CodeAnalysis;

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
    }
}