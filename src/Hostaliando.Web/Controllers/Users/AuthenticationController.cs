//-----------------------------------------------------------------------
// <copyright file="AuthenticationController.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Controllers.Users
{
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Hostaliando.Business.Security;
    using Hostaliando.Web.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Gets the user authenticated
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    [Route("api/v1/auth/current")]
    public class AuthenticationController : BaseApiController
    {
        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        /// <param name="workContext">The work context.</param>
        public AuthenticationController(
            IMessageExceptionFinder messageExceptionFinder,
            IWorkContext workContext) : base(messageExceptionFinder)
        {
            this.workContext = workContext;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>the current user</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = this.workContext.CurrentUser;
            await Task.FromResult(0);
            return this.Ok(user.ToBaseModel());
        }
    }
}