using System.Collections.Generic;
using System.Linq;
using DetectPublicApiChanges.Analysis.Roslyn;
using DetectPublicApiChanges.Interfaces;
using DetectPublicApiChanges.Report.Models;
using DetectPublicApiChanges.SourceControl.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
        /// <param name="indexComparison">The index comparison.</param>
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
            var projectList = new NavigationViewModel("Projects", GenerateAnchor("Projects"))
            {
                Items = indexComparison.Differences
                    .Select(d => d.Project)
                    .GroupBy(d => d.Name)
                    .Select(d => new NavigationViewModel(d.Key, GenerateAnchor(d.Key))).ToList()
            };
            projectList.Count = projectList.Items.Count;

            navigation.Add(projectList);

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

                //Class
                if (diff.SyntaxNode.IsKind(SyntaxKind.ClassDeclaration))
                {
                    var @class = diff.SyntaxNode as ClassDeclarationSyntax;

                    if (@class != null)
                    {

                        if (!details.ContainsKey(@class.GetFullName()))
                        {
                            detail = CreateDetailItem(@class.Identifier.ValueText, @class.GetFullName(),
                                GenerateAnchor(@class.GetFullName()), "Class");
                            details.Add(@class.GetFullName(), detail);
                        }
                        else
                        {
                            detail = details[@class.GetFullName()];
                        }

                        detail.Tags.Add(diff.Description.Category);
                        detail.Content.Add(diff.Description.Description);
                    }
                }

                //Interface
                if (diff.SyntaxNode.IsKind(SyntaxKind.InterfaceDeclaration))
                {
                    var @interface = diff.SyntaxNode as InterfaceDeclarationSyntax;

                    if (@interface != null)
                    {

                        if (!details.ContainsKey(@interface.GetFullName()))
                        {
                            detail = CreateDetailItem(@interface.Identifier.ValueText, @interface.GetFullName(), GenerateAnchor(@interface.GetFullName()), "Interface");
                            details.Add(@interface.GetFullName(), detail);
                        }
                        else
                        {
                            detail = details[@interface.GetFullName()];
                        }

                        detail.Tags.Add(diff.Description.Category);
                        detail.Content.Add(diff.Description.Description);
                    }
                }

                var parentClass = diff.SyntaxNode.Parent as ClassDeclarationSyntax;

                //Class as parent
                if (parentClass != null)
                {
                    if (!details.ContainsKey(parentClass.GetFullName()))
                    {
                        detail = CreateDetailItem(parentClass.Identifier.ValueText, parentClass.GetFullName(), GenerateAnchor(parentClass.GetFullName()), "Class");
                        details.Add(parentClass.GetFullName(), detail);
                    }
                    else
                    {
                        detail = details[parentClass.GetFullName()];
                    }
                }

                //Interface as parent
                var parentInterface = diff.SyntaxNode.Parent as InterfaceDeclarationSyntax;

                if (parentInterface != null)
                {
                    if (!details.ContainsKey(parentInterface.GetFullName()))
                    {
                        detail = CreateDetailItem(parentInterface.Identifier.ValueText, parentInterface.GetFullName(), GenerateAnchor(parentInterface.GetFullName()), "Interface");
                        details.Add(parentInterface.GetFullName(), detail);
                    }
                    else
                    {
                        detail = details[parentInterface.GetFullName()];
                    }
                }

                //Add information
                var method = diff.SyntaxNode as MethodDeclarationSyntax;
                if (method != null)
                {
                    detail.Tags.Add(diff.Description.Category);
                    detail.Content.Add(diff.Description.Description);
                }

                var ctor = diff.SyntaxNode as ConstructorDeclarationSyntax;
                if (ctor != null)
                {
                    detail.Tags.Add(diff.Description.Category);
                    detail.Content.Add(diff.Description.Description);
                }

                var property = diff.SyntaxNode as PropertyDeclarationSyntax;
                if (property != null)
                {
                    detail.Tags.Add(diff.Description.Category);
                    detail.Content.Add(diff.Description.Description);
                }
            }

            return details.Select(d => d.Value).ToList();
        }

        /// <summary>
        /// Creates the detail item.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="subtitle">The subtitle.</param>
        /// <param name="key">The key.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
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