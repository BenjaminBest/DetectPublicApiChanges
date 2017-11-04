using DetectPublicApiChanges.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System.Text;

namespace DetectPublicApiChanges.Analysis.Roslyn
{
	/// <summary>
	/// The class SyntaxNodeExtensions contains extension methods for the type SyntaxNode
	/// </summary>
	public static class SyntaxNodeExtensions
	{
		/// <summary>
		/// Determines whether parent nodes are public.
		/// </summary>
		/// <param name="syntaxNode">The syntax node.</param>
		/// <returns>
		///   <c>true</c> if parent nodes are public; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsHierarchyPublic(this SyntaxNode syntaxNode)
		{
			while (true)
			{
				SyntaxNode parent;

				var isPublic = false;
				syntaxNode.As<ClassDeclarationSyntax>()
					.IsNotNull(
						n => n.Modifiers.Any(m => m.ValueText.Equals("public") || m.ValueText.Equals("protected")),
						ref isPublic);
				syntaxNode.As<InterfaceDeclarationSyntax>()
					.IsNotNull(
						n => n.Modifiers.Any(m => m.ValueText.Equals("public") || m.ValueText.Equals("protected")),
						ref isPublic);
				syntaxNode.As<StructDeclarationSyntax>()
					.IsNotNull(
						n => n.Modifiers.Any(m => m.ValueText.Equals("public") || m.ValueText.Equals("protected")),
						ref isPublic);
				syntaxNode.As<PropertyDeclarationSyntax>()
					.IsNotNull(
						n => n.Modifiers.Any(m => m.ValueText.Equals("public") || m.ValueText.Equals("protected")),
						ref isPublic);
				syntaxNode.As<ConstructorDeclarationSyntax>()
					.IsNotNull(
						n => n.Modifiers.Any(m => m.ValueText.Equals("public") || m.ValueText.Equals("protected")),
						ref isPublic);
				syntaxNode.As<MethodDeclarationSyntax>()
					.IsNotNull(
						n => n.Modifiers.Any(m => m.ValueText.Equals("public") || m.ValueText.Equals("protected")),
						ref isPublic);

				if (isPublic)
				{
					parent = syntaxNode.Parent;
				}
				else
				{
					return false;
				}

				if (parent == null)
					return true;

				if (parent is NamespaceDeclarationSyntax)
					return true;

				syntaxNode = parent;
			}
		}

		/// <summary>
		/// Gets the attribute lists for the given <param name="syntaxNode"></param>
		/// </summary>
		/// <param name="syntaxNode">The syntax node.</param>
		/// <returns></returns>
		public static SyntaxList<AttributeListSyntax> GetAttributeLists(this SyntaxNode syntaxNode)
		{
			SyntaxList<AttributeListSyntax> result;

			syntaxNode.As<ClassDeclarationSyntax>()
				.IsNotNull(n => n.AttributeLists, ref result);
			syntaxNode.As<InterfaceDeclarationSyntax>()
				.IsNotNull(n => n.AttributeLists, ref result);
			syntaxNode.As<StructDeclarationSyntax>()
				.IsNotNull(n => n.AttributeLists, ref result);
			syntaxNode.As<PropertyDeclarationSyntax>()
				.IsNotNull(n => n.AttributeLists, ref result);
			syntaxNode.As<ConstructorDeclarationSyntax>()
				.IsNotNull(n => n.AttributeLists, ref result);
			syntaxNode.As<MethodDeclarationSyntax>()
				.IsNotNull(n => n.AttributeLists, ref result);

			return result;
		}

		/// <summary>
		/// Gets the full name.
		/// </summary>
		/// <param name="syntaxNode">The syntax node.</param>
		/// <returns></returns>
		public static string GetFullName(this SyntaxNode syntaxNode)
		{
			var fullName = new StringBuilder();

			while (true)
			{
				SyntaxNode parent;

				var name = string.Empty;
				syntaxNode.As<ClassDeclarationSyntax>().IsNotNull(n => n.Identifier.ValueText, ref name);
				syntaxNode.As<InterfaceDeclarationSyntax>().IsNotNull(n => n.Identifier.ValueText, ref name);
				syntaxNode.As<StructDeclarationSyntax>().IsNotNull(n => n.Identifier.ValueText, ref name);
				syntaxNode.As<PropertyDeclarationSyntax>().IsNotNull(n => n.Identifier.ValueText, ref name);
				syntaxNode.As<ConstructorDeclarationSyntax>().IsNotNull(n => n.Identifier.ValueText, ref name);
				syntaxNode.As<MethodDeclarationSyntax>().IsNotNull(n => n.Identifier.ValueText, ref name);
				syntaxNode.As<NamespaceDeclarationSyntax>().IsNotNull(n => n.Name.ToString(), ref name);

				if (!string.IsNullOrEmpty(name))
				{
					fullName.Insert(0, "." + name);
					parent = syntaxNode.Parent;
				}
				else
				{
					break;
				}

				if (parent == null || syntaxNode is NamespaceDeclarationSyntax)
				{
					if (fullName[0] == '.')
						fullName.Remove(0, 1);

					break;
				}

				syntaxNode = parent;
			}

			return fullName.ToString();
		}
	}
}
