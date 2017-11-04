using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DetectPublicApiChanges.Analysis.Roslyn
{
	/// <summary>
	///  Extension methods for AttributeArgumentSyntax
	/// </summary>
	public static class AttributeArgumentSyntaxExtensions
	{
		/// <summary>
		/// Gets the attribute arguments value
		/// </summary>
		/// <param name="argument">The argument.</param>
		/// <returns></returns>
		public static object GetArgumentValue(this AttributeArgumentSyntax argument)
		{
			return argument?.Expression
				.GetFirstToken().ToFullString();
		}
	}
}
