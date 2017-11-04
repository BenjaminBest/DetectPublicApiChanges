using DetectPublicApiChanges.Interfaces;
using System;

namespace DetectPublicApiChanges.Analysis.SyntaxNodeAnalyzers
{
	public class ObsoleteEntityInformation : IObsoleteEntityInformation
	{
		/// <summary>
		/// Gets the date.
		/// </summary>
		/// <value>
		/// The date.
		/// </value>
		public DateTime? Date { get; set; }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }
	}
}
