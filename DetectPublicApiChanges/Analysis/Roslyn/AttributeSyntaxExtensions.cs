using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.Roslyn
{
	/// <summary>
	///  Extension methods for AttributeSyntax
	/// </summary>
	public static class AttributeSyntaxExtensions
	{
		/// <summary>
		/// Gets the simple name from node.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <returns></returns>
		private static SimpleNameSyntax GetSimpleNameFromNode(AttributeSyntax node)
		{
			var identifierNameSyntax = node.Name as IdentifierNameSyntax;
			var qualifiedNameSyntax = node.Name as QualifiedNameSyntax;

			return
				identifierNameSyntax
				??
				qualifiedNameSyntax?.Right
				??
				(node.Name as AliasQualifiedNameSyntax).Name;
		}

		/// <summary>
		/// Returns <c>true</c> if the attribute name matches <param name="attributeName"></param>
		/// </summary>
		/// <param name="attribute">The attribute.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <returns></returns>
		public static bool AttributeNameMatches(this AttributeSyntax attribute, string attributeName)
		{
			return
				GetSimpleNameFromNode(attribute)
					.Identifier
					.Text
					.StartsWith(attributeName);
		}
	}
}
