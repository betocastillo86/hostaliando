//-----------------------------------------------------------------------
// <copyright file="UserService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Beto.Core.Helpers;
    using Hostaliando.Data;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// User Service
    /// </summary>
    /// <seealso cref="Hostaliando.Business.Services.IUserService" />
    public class UserService : IUserService
    {
        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IRepository<User> userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        public UserService(
            IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// the user
        /// </returns>
        public User GetById(int id)
        {
            return this.userRepository.Table.FirstOrDefault(c => c.Id == id && !c.Deleted);
        }

        /// <summary>
        /// Gets the user authenticated.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// the valid user or null
        /// </returns>
        public async Task<User> GetUserAuthenticated(string email, string password)
        {
            var user = await this.userRepository.Table.FirstOrDefaultAsync(c => c.Email.Equals(email));

            if (user != null && user.Password.Equals(StringHelpers.ToSha1(password, user.Salt)))
            {
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}