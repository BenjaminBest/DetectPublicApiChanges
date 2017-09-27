using System.Collections.Generic;

namespace DetectPublicApiChanges.Report.Models
{
	/// <summary>
	/// The report view model
	/// </summary>
	public class ReportViewModel
	{
		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the subtitle.
		/// </summary>
		/// <value>
		/// The subtitle.
		/// </value>
		public string Subtitle { get; set; }

		/// <summary>
		/// Gets or sets the navigation.
		/// </summary>
		/// <value>
		/// The navigation.
		/// </value>
		public IEnumerable<NavigationViewModel> Navigation { get; set; }

		/// <summary>
		/// Gets or sets the details.
		/// </summary>
		/// <value>
		/// The details.
		/// </value>
		public IEnumerable<DetailViewModel> Details { get; set; }

		/// <summary>
		/// Gets or sets the change log.
		/// </summary>
		/// <value>
		/// The change log.
		/// </value>
		public IEnumerable<ChangeLogViewModel> ChangeLog { get; set; }
	}
}
