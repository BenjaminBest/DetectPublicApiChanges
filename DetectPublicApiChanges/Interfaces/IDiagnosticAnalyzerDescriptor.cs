namespace DetectPublicApiChanges.Interfaces
{
	/// <summary>
	/// The interface IDiagnosticAnalyzerDescriptor defines a description of for an analyzer
	/// </summary>
	public interface IDiagnosticAnalyzerDescriptor
	{
		/// <summary>
		/// Gets the diagnostic identifier.
		/// </summary>
		/// <value>
		/// The diagnostic identifier.
		/// </value>
		string DiagnosticId { get; }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		string Description { get; }

		/// <summary>
		/// Gets the category.
		/// </summary>
		/// <value>
		/// The category.
		/// </value>
		string Category { get; }

		/// <summary>
		/// Gets the obsolete information.
		/// </summary>
		/// <value>
		/// The obsolete information.
		/// </value>
		IObsoleteEntityInformation Obsolete { get; }

		/// <summary>
		/// Adds the description.
		/// </summary>
		/// <param name="desciption">The desciption.</param>
		/// <returns></returns>
		IDiagnosticAnalyzerDescriptor AddDescription(string desciption);

		/// <summary>
		/// Adds the obsolete information.
		/// </summary>
		/// <param name="obsolete">The obsolete.</param>
		/// <returns></returns>
		IDiagnosticAnalyzerDescriptor AddObsoleteInformation(IObsoleteEntityInformation obsolete);
	}
}