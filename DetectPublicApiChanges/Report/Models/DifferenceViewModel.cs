using System.Collections.Generic;

namespace DetectPublicApiChanges.Report.Models
{
    /// <summary>
    /// The NavigationViewModel is used to render the navigation menu
    /// </summary>
    public class NavigationViewModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the anchor.
        /// </summary>
        /// <value>
        /// The anchor.
        /// </value>
        public string Anchor { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public List<NavigationViewModel> Items { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationViewModel"/> class.
        /// </summary>
        public NavigationViewModel()
            : this(string.Empty, string.Empty)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationViewModel" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="anchor">The anchor.</param>
        public NavigationViewModel(string name, string anchor)
        {
            Name = name;
            Anchor = anchor;
            Items = new List<NavigationViewModel>();
        }
    }
}
