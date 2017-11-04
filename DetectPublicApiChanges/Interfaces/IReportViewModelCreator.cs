using DetectPublicApiChanges.Report.Models;
using System.Collections.Generic;

namespace DetectPublicApiChanges.Interfaces
{
	/// <summary>
	/// The interface IReportViewModelCreator defines a mapper which creates the report viewmodel based on input
	/// </summary>
	public interface IReportViewModelCreator
	{
		/// <summary>
		/// Creates the specified report based on the data given
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		ReportViewModel Create(IDictionary<string, object> data);
	}
}