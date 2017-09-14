//-----------------------------------------------------------------------
// <copyright file="LogModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using System;

    /// <summary>
    /// Log Model
    /// </summary>
    /// <seealso cref="Hostaliando.Web.Models.BaseModel" />
    public class LogModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the short message.
        /// </summary>
        /// <value>
        /// The short message.
        /// </value>
        public string ShortMessage { get; set; }

        /// <summary>
        /// Gets or sets the full message.
        /// </summary>
        /// <value>
        /// The full message.
        /// </value>
        public string FullMessage { get; set; }

        /// <summary>
        /// Gets or sets the IP address.
        /// </summary>
        /// <value>
        /// The IP address.
        /// </value>
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the page URL.
        /// </summary>
        /// <value>
        /// The page URL.
        /// </value>
        public string PageUrl { get; set; }

        /// <summary>
        /// Gets or sets the user model.
        /// </summary>
        /// <value>
        /// The user model.
        /// </value>
        public BaseUserModel UserModel { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get; set; }
    }
}