using System;

namespace DetectPublicApiChanges.Interfaces
{
	/// <summary>
	/// The interface IObsoleteInformation defines information about an obsolete entity
	/// </summary>
	public interface IObsoleteEntityInformation
	{
		/// <summary>
		/// Gets the date.
		/// </summary>
		/// <value>
		/// The date.
		/// </value>
		DateTime? Date { get; }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		string Description { get; }
	}
}