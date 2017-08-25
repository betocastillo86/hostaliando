﻿//<auto-generated />
namespace Hostaliando.Web.TagHelpers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    /// <summary>
    /// HtmlHelper for Javascript Resources
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Razor.TagHelpers.TagHelper" />
    [HtmlTargetElement("jsresources")]
    public class JavascriptResourcesTagHelper : TagHelper
    {
        /// <summary>
        /// The application environment
        /// </summary>
        private readonly IHostingEnvironment _appEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="JavascriptResourcesTagHelper"/> class.
        /// </summary>
        /// <param name="appEnvironment">The application environment.</param>
        public JavascriptResourcesTagHelper(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        /// <summary>
        /// Synchronously executes the <see cref="T:Microsoft.AspNetCore.Razor.TagHelpers.TagHelper" /> with the given <paramref name="context" /> and
        /// <paramref name="output" />.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var rootPath = _appEnvironment.ContentRootPath + "/wwwroot";
            var strHtml = new StringBuilder();
            var mainFolder = context.AllAttributes["type"].Value.ToString();

            SearchJsInPath(rootPath + "/app", true, mainFolder)
                .ToList()
                .ForEach(c => strHtml.AppendLine($"<script src=\"{c.Replace(rootPath, string.Empty).Replace("\\", "/")}\"></script>"));

            output.Content.SetHtmlContent(strHtml.ToString());
            base.Process(context, output);
        }

        /// <summary>
        /// Searches the js in path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="root">if set to <c>true</c> [root].</param>
        /// <returns>the value</returns>
        private IList<string> SearchJsInPath(string path, bool root, string mainFolder)
        {
            var foundFiles = new List<string>();

            if (root)
            {
                foreach (var file in System.IO.Directory.GetFiles(path))
                {
                    foundFiles.Add(file);
                }
            }

            foreach (var directory in System.IO.Directory.GetDirectories(path).OrderBy(c => c))
            {
                var directoryName = new System.IO.DirectoryInfo(directory).Name;

                if (root && !directoryName.Equals("services") && !directoryName.Equals("components") && !directoryName.Equals(mainFolder))
                {
                    continue;
                }

                foreach (var file in System.IO.Directory.GetFiles(directory).Where(f => f.EndsWith(".js")).OrderBy(c => c))
                {
                    foundFiles.Add(file);
                }

                foundFiles.AddRange(SearchJsInPath(directory, false, null));
            }

            return foundFiles;
        }
    }
}