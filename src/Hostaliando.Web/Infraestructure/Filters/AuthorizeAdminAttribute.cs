//-----------------------------------------------------------------------
// <copyright file="AuthorizeAdminAttribute.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Infraestructure.Filters
{
    using Hostaliando.Business.Security;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Authorize Admin Attribute
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute" />
    public class AuthorizeAdminAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeAdminAttribute"/> class.
        /// </summary>
        /// <param name="workContext">The work context.</param>
        public AuthorizeAdminAttribute(IWorkContext workContext)
        {
            this.workContext = workContext;
        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (this.workContext.CurrentUser?.Role != Data.Role.Admin)
            {
                context.Result = new ForbidResult();
            }

            base.OnActionExecuting(context);
        }
    }
}