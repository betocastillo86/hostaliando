//-----------------------------------------------------------------------
// <copyright file="SeoService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    using Hostaliando.Business.Configuration;

    /// <summary>
    /// SEO Service
    /// </summary>
    /// <seealso cref="Hostaliando.Business.Services.ISeoService" />
    public class SeoService : ISeoService
    {
        /// <summary>
        /// The general settings
        /// </summary>
        private readonly IGeneralSettings generalSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeoService"/> class.
        /// </summary>
        /// <param name="generalSettings">The general settings.</param>
        public SeoService(IGeneralSettings generalSettings)
        {
            this.generalSettings = generalSettings;
        }

        /// <summary>
        /// Gets the full route.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>the route</returns>
        public string GetFullRoute(string key, params string[] parameters)
        {
            return string.Concat(this.generalSettings.SiteUrl, this.GetRoute(key, parameters));
        }

        /// <summary>
        /// Gets the route.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>the route</returns>
        public string GetRoute(string key, params string[] parameters)
        {
            switch (key)
            {
                case "passwordrecovery":
                    return string.Format("/passwordrecovery/{0}", parameters);
                case "login":
                    return "login";
                case "bookings":
                    return "bookings";
                default:
                    return string.Empty;
            }
        }
    }
}