using System.Collections.Generic;
using System.Linq;
using DetectPublicApiChanges.Analysis.Structures;
using DetectPublicApiChanges.Interfaces;
using DetectPublicApiChanges.Report.Models;

namespace DetectPublicApiChanges.Report
{
    /// <summary>
    /// The ViewModelCreator creates the viewmodel needed for the report creation
    /// </summary>
    //TODO: Refactor this messy code
    public class ReportViewModelCreator : IReportViewModelCreator
    {
        /// <summary>
        /// Creates the specified index comparison.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="subtitle">The subtitle.</param>
        /// <param name="indexComparison">The index comparison.</param>
        /// <param name="changeLog">The change log.</param>
        /// <returns></returns>
        public ReportViewModel Create(string title, string subtitle, IStructureIndexComparisonResult indexComparison, ISourceControlChangeLog changeLog)
        {
            var details = CreateDetails(indexComparison).ToList();

            var viewModel = new ReportViewModel()
            {
                HasDifferences = indexComparison.HasDifferences,
                Title = title,
                Subtitle = subtitle,
                Navigation = CreateNavigation(details, indexComparison),
                Details = details,
                ChangeLog = CreateChangeLog(changeLog),
                HasChangeLog = changeLog != null && changeLog.Items.Any()
            };

            return viewModel;
        }

        /// <summary>
        /// Creates the change log.
        /// </summary>
        /// <param name="changeLog">The change log.</param>
        /// <returns></returns>
        protected IEnumerable<ChangeLogViewModel> CreateChangeLog(ISourceControlChangeLog changeLog)
        {
            if (changeLog == null || changeLog.Items.Count == 0)
                return Enumerable.Empty<ChangeLogViewModel>();

            return changeLog.Items.Select(
                c => new ChangeLogViewModel() { Author = c.Author, Message = c.Message, TimeStamp = c.TimeStamp });
        }

        /// <summary>
        /// Creates the navigation.
        /// </summary>
        /// <param name="details">The details.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        protected IEnumerable<NavigationViewModel> CreateNavigation(IEnumerable<DetailViewModel> details, IStructureIndexComparisonResult indexComparison)
        {
            var navigation = new List<NavigationViewModel>();

            //Default
            var defaultMenu = new NavigationViewModel("Default", GenerateAnchor("Default"));
            defaultMenu.Items.Add(new NavigationViewModel("Summary", GenerateAnchor("Summary")));
            defaultMenu.Items.Add(new NavigationViewModel("Changelog", GenerateAnchor("Changelog")));
            navigation.Add(defaultMenu);

            //Projects
            var projects = new NavigationViewModel("Projects", GenerateAnchor("Projects"))
            {
                Items = indexComparison.Differences.Where(d => d.Parent is ProjectStructure)
                    .Select(d => d.Parent as ProjectStructure)
                    .GroupBy(d => d.Name)
                    .Select(d => new NavigationViewModel(d.Key, GenerateAnchor(d.Key))).ToList()
            };
            projects.Count = projects.Items.Count;

            navigation.Add(projects);

            //Classes
            var classes = new NavigationViewModel("Classes", GenerateAnchor("Classes"))
            {
                Items = details.Where(d => d.Type.Equals("Class"))
                    .Select(d => new NavigationViewModel(d.Title, GenerateAnchor(d.Key))).ToList()
            };
            classes.Count = classes.Items.Count;

            navigation.Add(classes);

            //Interfaces
            var interfaces = new NavigationViewModel("Interfaces", GenerateAnchor("Interfaces"))
            {
                Items = details.Where(d => d.Type.Equals("Interface"))
                    .Select(d => new NavigationViewModel(d.Title, GenerateAnchor(d.Key))).ToList()
            };
            interfaces.Count = interfaces.Items.Count;

            navigation.Add(interfaces);

            //Misc
            var miscMenu = new NavigationViewModel("Miscellaneous", GenerateAnchor("Miscellaneous"));
            miscMenu.Items.Add(new NavigationViewModel("Obsolete items", GenerateAnchor("Obsolete items")));
            navigation.Add(miscMenu);

            return navigation;
        }

        /// <summary>
        /// Creates the details.
        /// </summary>
        /// <param name="indexComparison">The index comparison.</param>
        /// <returns></returns>
        protected virtual IEnumerable<DetailViewModel> CreateDetails(IStructureIndexComparisonResult indexComparison)
        {
            var details = new Dictionary<string, DetailViewModel>();

            foreach (var diff in indexComparison.Differences)
            {
                DetailViewModel detail = null;

                if (diff.Parent.GetType() == typeof(ProjectStructure))
                {
                    var @class = diff.Structure as ClassStructure;

                    //Class
                    if (@class != null)
                    {

                        if (!details.ContainsKey(@class.FullName))
                        {
                            detail = CreateDetailItem(@class.Name, @class.FullName, GenerateAnchor(@class.FullName), "Class");
                            details.Add(@class.FullName, detail);
                        }
                        else
                        {
                            detail = details[@class.FullName];
                        }

                        detail.Tags.Add("Class");
                        detail.Content.Add($"The class {@class.Name} itself seems to be changed or removed");
                    }

                    var @interface = diff.Structure as InterfaceStructure;

                    //Interface
                    if (@interface != null)
                    {

                        if (!details.ContainsKey(@interface.FullName))
                        {
                            detail = CreateDetailItem(@interface.Name, @interface.FullName, GenerateAnchor(@interface.FullName), "Interface");
                            details.Add(@interface.FullName, detail);
                        }
                        else
                        {
                            detail = details[@interface.FullName];
                        }

                        detail.Tags.Add("Interface");
                        detail.Content.Add($"The interface {@interface.Name} itself seems to be changed or removed");
                    }
                }

                var parentClass = diff.Parent as ClassStructure;

                //Class as parent
                if (parentClass != null)
                {
                    if (!details.ContainsKey(parentClass.FullName))
                    {
                        detail = CreateDetailItem(parentClass.Name, parentClass.FullName, GenerateAnchor(parentClass.FullName), "Class");
                        details.Add(parentClass.FullName, detail);
                    }
                    else
                    {
                        detail = details[parentClass.FullName];
                    }
                }

                //Interface as parent
                var parentInterface = diff.Parent as InterfaceStructure;

                if (parentInterface != null)
                {
                    if (!details.ContainsKey(parentInterface.FullName))
                    {
                        detail = CreateDetailItem(parentInterface.Name, parentInterface.FullName, GenerateAnchor(parentInterface.FullName), "Interface");
                        details.Add(parentInterface.FullName, detail);
                    }
                    else
                    {
                        detail = details[parentInterface.FullName];
                    }
                }

                //Add information
                var method = diff.Structure as MethodStructure;
                if (method != null)
                {
                    detail.Tags.Add("Method");
                    detail.Content.Add($"The Method {method.Name} seems to be changed or removed");
                }

                var ctor = diff.Structure as ConstructorStructure;
                if (ctor != null)
                {
                    detail.Tags.Add("Constructor");
                    detail.Content.Add("The Constructor seems to be changed or removed");
                }

                var property = diff.Structure as PropertyStructure;
                if (property != null)
                {
                    detail.Tags.Add("Property");
                    detail.Content.Add($"The Property {property.Name} seems to be changed or removed");
                }
            }

            return details.Select(d => d.Value).ToList();
        }

        protected virtual DetailViewModel CreateDetailItem(string title, string subtitle, string key, string type)
        {
            var detail = new DetailViewModel()
            {
                Title = title,
                Subtitle = subtitle,
                Key = key,
                Type = type
            };

            return detail;
        }

        /// <summary>
        /// Generates the anchor based on the given <paramref name="text"/>
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>Anchor</returns>
        private static string GenerateAnchor(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var anchor = text
                .Trim()
                .Replace(' ', '-')
                .Replace('<', '-')
                .Replace('>', '-');

            return anchor;
        }
    }
}