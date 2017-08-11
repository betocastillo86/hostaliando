//-----------------------------------------------------------------------
// <copyright file="WorkContext.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Security
{
    using Hostaliando.Data;

    /// <summary>
    /// Work Context
    /// </summary>
    /// <seealso cref="Hostaliando.Business.Security.IWorkContext" />
    public class WorkContext : IWorkContext
    {
        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public User CurrentUser => new User() { Id = 1, Name = "Gabriel" };

        /// <summary>
        /// Gets the current user identifier.
        /// </summary>
        /// <value>
        /// The current user identifier.
        /// </value>
        public int CurrentUserId => this.CurrentUser.Id;

        /// <summary>
        /// Gets a value indicating whether this instance is authenticated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is authenticated; otherwise, <c>false</c>.
        /// </value>
        public bool IsAuthenticated => true;
    }
}