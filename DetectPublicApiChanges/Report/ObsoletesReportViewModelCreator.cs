using DetectPublicApiChanges.Analysis.Roslyn;
using DetectPublicApiChanges.Extensions;
using DetectPublicApiChanges.Interfaces;
using DetectPublicApiChanges.Report.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DetectPublicApiChanges.Report
{
	/// <summary>
	/// The ViewModelCreator creates the viewmodel needed for the report creation
	/// </summary>
	//TODO: Refactor this messy code
	public class ObsoletesReportViewModelCreator : IReportViewModelCreator
	{
		/// <summary>
		/// Creates the specified report.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		public ReportViewModel Create(IDictionary<string, object> data)
		{
			var index = data["results"].As<List<IIndexItem>>();

			var details = CreateDetails(index).ToList();

			var viewModel = new ReportViewModel()
			{
				Title = data["title"].ToString(),
				Subtitle = data["subtitle"].ToString(),
				Navigation = CreateNavigation(details, index),
				Details = details
			};

			return viewModel;
		}

		/// <summary>
		/// Creates the navigation.
		/// </summary>
		/// <param name="details">The details.</param>
		/// <param name="items">The items.</param>
		/// <returns></returns>
		protected IEnumerable<NavigationViewModel> CreateNavigation(IEnumerable<DetailViewModel> details, IList<IIndexItem> items)
		{
			var navigation = new List<NavigationViewModel>();

			//Default
			var defaultMenu = new NavigationViewModel("Default", GenerateAnchor("Default"));
			defaultMenu.Items.Add(new NavigationViewModel("Summary", GenerateAnchor("Summary")));
			navigation.Add(defaultMenu);

			//Projects
			var projectList = new NavigationViewModel("Projects", GenerateAnchor("Projects"))
			{
				Items = items
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

			//Structs
			var structs = new NavigationViewModel("Structs", GenerateAnchor("Structs"))
			{
				Items = details.Where(d => d.Type.Equals("Struct"))
					.Select(d => new NavigationViewModel(d.Title, GenerateAnchor(d.Key))).ToList()
			};
			structs.Count = structs.Items.Count;

			navigation.Add(structs);

			return navigation;
		}

		/// <summary>
		/// Creates the details.
		/// </summary>
		/// <param name="items">The items.</param>
		/// <returns></returns>
		protected virtual IEnumerable<DetailViewModel> CreateDetails(IList<IIndexItem> items)
		{
			var details = new Dictionary<string, DetailViewModel>();

			foreach (var item in items)
			{
				DetailViewModel detail = null;

				//Class
				if (item.SyntaxNode.IsKind(SyntaxKind.ClassDeclaration))
				{
					var @class = item.SyntaxNode as ClassDeclarationSyntax;

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

						detail.Tags.Add(item.Description.Category);

						if (item.Description.Obsolete.Date.HasValue)
							detail.Content.Add($"Class obsolete since {item.Description.Obsolete.Date.Value:yyyy-MM-dd}");
					}
				}

				//Interface
				if (item.SyntaxNode.IsKind(SyntaxKind.InterfaceDeclaration))
				{
					var @interface = item.SyntaxNode as InterfaceDeclarationSyntax;

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

						detail.Tags.Add(item.Description.Category);

						if (item.Description.Obsolete.Date.HasValue)
							detail.Content.Add($"Interface obsolete since {item.Description.Obsolete.Date.Value:yyyy-MM-dd}");
					}
				}

				var parentClass = item.SyntaxNode.Parent as ClassDeclarationSyntax;

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
				var parentInterface = item.SyntaxNode.Parent as InterfaceDeclarationSyntax;

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

				//Struct as parent
				var parentstruct = item.SyntaxNode.Parent as StructDeclarationSyntax;

				if (parentstruct != null)
				{
					if (!details.ContainsKey(parentstruct.GetFullName()))
					{
						detail = CreateDetailItem(parentstruct.Identifier.ValueText, parentstruct.GetFullName(), GenerateAnchor(parentstruct.GetFullName()), "Struct");
						details.Add(parentstruct.GetFullName(), detail);
					}
					else
					{
						detail = details[parentstruct.GetFullName()];
					}
				}

				//Add information
				var method = item.SyntaxNode as MethodDeclarationSyntax;
				if (method != null)
				{
					detail.Tags.Add(item.Description.Category);

					if (item.Description.Obsolete.Date.HasValue)
						detail.Content.Add($"Method obsolete since {item.Description.Obsolete.Date.Value:yyyy-MM-dd}");
				}

				var ctor = item.SyntaxNode as ConstructorDeclarationSyntax;
				if (ctor != null)
				{
					detail.Tags.Add(item.Description.Category);

					if (item.Description.Obsolete.Date.HasValue)
						detail.Content.Add($"Ctor obsolete since {item.Description.Obsolete.Date.Value:yyyy-MM-dd}");
				}

				var property = item.SyntaxNode as PropertyDeclarationSyntax;
				if (property != null)
				{
					detail.Tags.Add(item.Description.Category);

					if (item.Description.Obsolete.Date.HasValue)
						detail.Content.Add($"Property obsolete since {item.Description.Obsolete.Date.Value:yyyy-MM-dd}");
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