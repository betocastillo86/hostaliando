//-----------------------------------------------------------------------
// <copyright file="UserExtensions.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using Hostaliando.Data;

    /// <summary>
    /// User Extensions
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// Determines whether this instance is admin.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>the value</returns>
        public static bool IsAdmin(this User user)
        {
            return user.Role == Role.Admin;
        }
    }
}