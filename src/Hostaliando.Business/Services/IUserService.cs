//-----------------------------------------------------------------------
// <copyright file="IUserService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Hostaliando.Data;

    /// <summary>
    /// Interface of user service
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>the task</returns>
        Task Delete(User user);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeHostel">includes the hostel information</param>
        /// <returns>the user</returns>
        User GetById(int id, bool includeHostel = false);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="role">The role.</param>
        /// <param name="hostelId">The hostel identifier.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>the list of users</returns>
        Task<IPagedList<User>> GetAll(
            string keyword = null, 
            Role? role = null, 
            int? hostelId = null,
            SortUserBy sortBy = SortUserBy.Recent,
            int page = 0,
            int pageSize = int.MaxValue);

        /// <summary>
        /// Gets the user authenticated.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>the valid user or null</returns>
        Task<User> GetUserAuthenticated(string email, string password);

        /// <summary>
        /// Inserts the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>the task</returns>
        Task Insert(User user);

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>the task</returns>
        Task Update(User user);
    }
}