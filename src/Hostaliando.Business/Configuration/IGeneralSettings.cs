//-----------------------------------------------------------------------
// <copyright file="IGeneralSettings.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Configuration
{
    /// <summary>
    /// Interface of general settings
    /// </summary>
    public interface IGeneralSettings
    {
        /// <summary>
        /// Gets the site URL.
        /// </summary>
        /// <value>
        /// The site URL.
        /// </value>
        string SiteUrl { get; }

        /// <summary>
        /// Gets the body base HTML.
        /// </summary>
        /// <value>
        /// The body base HTML.
        /// </value>
        string BodyBaseHtml { get; }

        /// <summary>
        /// Gets the date format.
        /// </summary>
        /// <value>
        /// The date format.
        /// </value>
        string DateFormat { get; }
    }
}