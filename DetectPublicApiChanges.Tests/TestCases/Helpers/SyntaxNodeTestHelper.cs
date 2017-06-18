using DetectPublicApiChanges.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Tests.TestCases.Helpers
{
    /// <summary>
    /// The class SyntaxNodeTestHelper contains helpers for unit tests
    /// </summary>
    public class SyntaxNodeTestHelper
    {
        /// <summary>
        /// Gets the syntax node.
        /// </summary>
        /// <param name="testcase">The testcase.</param>
        /// <returns></returns>
        public static SyntaxNode GetSyntaxNode(string testcase)
        {
            var tree = SyntaxTreeTestHelper.GetSyntaxTree(testcase);

            return tree.GetRoot();
        }

        /// <summary>
        /// Gets the name of the syntax node by.
        /// </summary>
        /// <param name="testcase">The testcase.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static SyntaxNode GetSyntaxNodeByName<TDeclarationType>(string testcase, string name) where TDeclarationType : CSharpSyntaxNode
        {
            var tree = SyntaxTreeTestHelper.GetSyntaxTree(testcase);

            var rootNode = tree.GetRoot();

            var nodes = rootNode.DescendantNodesAndSelf();

            SyntaxNode result = null;
            foreach (var node in nodes)
            {
                if (!(node is TDeclarationType))
                    continue;

                var nodeName = GetIdentifier(node);

                if (!nodeName.Contains(name))
                    continue;

                result = node;
                break;
            }

            return result;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        private static string GetIdentifier(SyntaxNode node)
        {
            var name = string.Empty;

            node.As<ClassDeclarationSyntax>().IsNotNull(n => n.Identifier.ValueText, ref name);
            node.As<InterfaceDeclarationSyntax>().IsNotNull(n => n.Identifier.ValueText, ref name);
            node.As<StructDeclarationSyntax>().IsNotNull(n => n.Identifier.ValueText, ref name);
            node.As<PropertyDeclarationSyntax>().IsNotNull(n => n.Identifier.ValueText, ref name);
            node.As<ConstructorDeclarationSyntax>().IsNotNull(n => n.Identifier.ValueText, ref name);
            node.As<MethodDeclarationSyntax>().IsNotNull(n => n.Identifier.ValueText, ref name);

            return name;
        }
    }
}