//-----------------------------------------------------------------------
// <copyright file="MenuOptionModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Menu Option Model
    /// </summary>
    /// <seealso cref="Hostaliando.Web.Models.BaseNamedModel" />
    public class MenuOptionModel : BaseNamedModel
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the display order.
        /// </summary>
        /// <value>
        /// The display order.
        /// </value>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        public IList<MenuOptionModel> Children { get; set; }
    }
}