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
    }
}