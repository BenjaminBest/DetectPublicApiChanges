using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using RazorEngine.Templating;
using RazorEngine.Text;

namespace DetectPublicApiChanges.Report.Mvc
{
    /// <summary>
    /// HtmlTemplateBase is used be able to use razor syntax
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="RazorEngine.Templating.TemplateBase{T}" />
    /// <seealso cref="IViewDataContainer" />
    [RequireNamespaces("System.Web.Mvc.Html")]
    public class HtmlTemplateBase<T> : TemplateBase<T>, IViewDataContainer
    {
        /// <summary>
        /// The helper
        /// </summary>
        private HtmlHelper<T> helper;

        /// <summary>
        /// The viewdata
        /// </summary>
        private ViewDataDictionary viewdata;

        /// <summary>
        /// Gets the HTML.
        /// </summary>
        /// <value>
        /// The HTML.
        /// </value>
        public HtmlHelper<T> Html
        {
            get
            {
                if (helper == null)
                {
                    var writer = this.CurrentWriter; //TemplateBase.CurrentWriter
                    var vcontext = new ViewContext() { RequestContext = HttpContext.Current.Request.RequestContext, Writer = writer, ViewData = this.ViewData };

                    helper = new HtmlHelper<T>(vcontext, this);
                }
                return helper;
            }
        }

        /// <summary>
        /// Gets or sets the view data dictionary.
        /// </summary>
        public ViewDataDictionary ViewData
        {
            get
            {
                if (viewdata == null)
                {
                    viewdata = new ViewDataDictionary
                    {
                        TemplateInfo = new TemplateInfo() { HtmlFieldPrefix = string.Empty }
                    };

                    if (this.Model != null)
                    {
                        viewdata.Model = Model;
                    }
                }
                return viewdata;
            }
            set => viewdata = value;
        }

        /// <summary>
        /// Writes the specified object to the specified <see cref="T:System.IO.TextWriter" />.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value to be written.</param>
        /// <exception cref="ArgumentNullException">writer</exception>
        public override void WriteTo(TextWriter writer, object value)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            if (value == null) return;

            //try to cast to RazorEngine IEncodedString
            var encodedString = value as IEncodedString;
            if (encodedString != null)
            {
                writer.Write(encodedString);
            }
            else
            {
                //try to cast to IHtmlString (Could be returned by Mvc Html helper methods)
                var htmlString = value as IHtmlString;
                if (htmlString != null) writer.Write(htmlString.ToHtmlString());
                else
                {
                    //default implementation is to convert to RazorEngine encoded string
                    encodedString = TemplateService.EncodedStringFactory.CreateEncodedString(value);
                    writer.Write(encodedString);
                }

            }
        }
    }
}