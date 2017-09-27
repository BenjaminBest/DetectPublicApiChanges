using DetectPublicApiChanges.Analysis.Roslyn;
using DetectPublicApiChanges.Analysis.SyntaxNodeAnalyzers;
using DetectPublicApiChanges.Common;
using DetectPublicApiChanges.Extensions;
using DetectPublicApiChanges.Interfaces;
using Microsoft.CodeAnalysis;
using System;
using System.Linq;

namespace DetectPublicApiChanges.Analysis.Obsoletes
{
	public class ObsoleteSyntaxNodeAnalyzer
	{
		/// <summary>
		/// Determines detailed information if the analyzed instance is obsolete or <c>null</c>.
		/// </summary>
		/// <returns></returns>
		public static IObsoleteEntityInformation GetObsoleteInformation(SyntaxNode syntaxNode)
		{
			ObsoleteEntityInformation info = null;

			foreach (var attributeList in syntaxNode.GetAttributeLists())
			{
				var attribute = attributeList.Attributes.FirstOrDefault(a => a.AttributeNameMatches("Obsolete"));

				if (attribute != null)
				{
					var description =
						attribute.ArgumentList.IsNotNull(a => a.Arguments.IsNotNull(b => b.FirstOrDefault()?.GetArgumentValue().ToString()));

					info = new ObsoleteEntityInformation();

					if (!string.IsNullOrEmpty(description))
					{
						DateTime date;
						description.TryParseDate(DateParser.DateTimeFormat.UK_DATE, out date);

						if (date != DateTime.MinValue)
							info.Date = date;
					}

					info.Description = description;
				}

			}

			return info;
		}
	}
}
