//-----------------------------------------------------------------------
// <copyright file="IUserService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    using System.Threading.Tasks;
    using Hostaliando.Data;

    /// <summary>
    /// Interface of user service
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the user</returns>
        User GetById(int id);

        /// <summary>
        /// Gets the user authenticated.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>the valid user or null</returns>
        Task<User> GetUserAuthenticated(string email, string password);
    }
}