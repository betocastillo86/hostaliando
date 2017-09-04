//-----------------------------------------------------------------------
// <copyright file="WorkContext.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Common
{
    using System;
    using AspNet.Security.OpenIdConnect.Primitives;
    using Hostaliando.Business.Security;
    using Hostaliando.Business.Services;
    using Hostaliando.Data;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Work Context
    /// </summary>
    /// <seealso cref="Hostaliando.Business.Security.IWorkContext" />
    public class WorkContext : IWorkContext
    {
        /// <summary>
        /// The HTTP context accessor
        /// </summary>
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// The current user
        /// </summary>
        private User currentUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkContext"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public WorkContext(
            IUserService userService,
            IHttpContextAccessor httpContextAccessor)
        {
            this.userService = userService;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public User CurrentUser
        {
            get
            {
                if (this.httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    if (this.currentUser == null)
                    {
                        this.currentUser = this.userService.GetById(this.CurrentUserId, true);
                    }
                }

                return this.currentUser;
            }
        }

        /// <summary>
        /// Gets the current user identifier.
        /// </summary>
        /// <value>
        /// The current user identifier.
        /// </value>
        public int CurrentUserId => this.httpContextAccessor.HttpContext.User.Identity.IsAuthenticated ? Convert.ToInt32(this.httpContextAccessor.HttpContext.User.FindFirst(OpenIdConnectConstants.Claims.Subject).Value) : 0;

        /// <summary>
        /// Gets a value indicating whether this instance is authenticated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is authenticated; otherwise, <c>false</c>.
        /// </value>
        public bool IsAuthenticated => this.httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
    }
}