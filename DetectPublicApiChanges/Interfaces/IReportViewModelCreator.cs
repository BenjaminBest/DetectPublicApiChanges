using DetectPublicApiChanges.Report.Models;
using DetectPublicApiChanges.SourceControl.Interfaces;

namespace DetectPublicApiChanges.Interfaces
{
    /// <summary>
    /// The interface IReportViewModelCreator defines a mapper which creates the report viewmodel based on input
    /// </summary>
    public interface IReportViewModelCreator
    {
        /// <summary>
        /// Creates the specified index comparison.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="subtitle">The subtitle.</param>
        /// <param name="indexComparison">The index comparison.</param>
        /// <param name="changeLog">The change log.</param>
        /// <returns></returns>
        ReportViewModel Create(string title, string subtitle, IStructureIndexComparisonResult indexComparison, ISourceControlChangeLog changeLog);
    }
}