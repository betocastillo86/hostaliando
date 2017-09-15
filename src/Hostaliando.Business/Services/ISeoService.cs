//-----------------------------------------------------------------------
// <copyright file="ISeoService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    /// <summary>
    /// Interface of SEO Service
    /// </summary>
    public interface ISeoService
    {
        /// <summary>
        /// Gets the route.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>the route</returns>
        string GetRoute(string key, params string[] parameters);

        /// <summary>
        /// Gets the full route.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>the route</returns>
        string GetFullRoute(string key, params string[] parameters);
    }
}