//-----------------------------------------------------------------------
// <copyright file="HangFireAuthorizationFilter.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Infraestructure.Filters
{
    using Hangfire.Annotations;
    using Hangfire.Dashboard;

    /// <summary>
    /// Hangfire authorization filter
    /// </summary>
    /// <seealso cref="Hangfire.Dashboard.IDashboardAuthorizationFilter" />
    public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        /// <summary>
        /// Authorizes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>the confirmation</returns>
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}