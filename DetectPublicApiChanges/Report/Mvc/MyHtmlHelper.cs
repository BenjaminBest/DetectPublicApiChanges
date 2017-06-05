using System;
using System.IO;
using RazorEngine.Text;

namespace DetectPublicApiChanges.Report.Mvc
{
    /// <summary>
    /// Html helper
    /// </summary>
    public class MyHtmlHelper
    {
        /// <summary>
        /// Raws the specified raw string.
        /// </summary>
        /// <param name="rawString">The raw string.</param>
        /// <returns></returns>
        public IEncodedString Raw(string rawString)
        {
            return new RawString(rawString);
        }

        /// <summary>
        /// Renders the CSS.
        /// </summary>
        /// <returns></returns>
        public static IEncodedString RenderCss()
        {
            var css = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Views\\Style.css"));

            return new RawString(css);
        }
    }

}