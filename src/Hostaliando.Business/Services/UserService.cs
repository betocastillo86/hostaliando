//-----------------------------------------------------------------------
// <copyright file="UserService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Beto.Core.EventPublisher;
    using Beto.Core.Helpers;
    using Hostaliando.Business.Exceptions;
    using Hostaliando.Data;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// User Service
    /// </summary>
    /// <seealso cref="Hostaliando.Business.Services.IUserService" />
    public class UserService : IUserService
    {
        /// <summary>
        /// The publisher
        /// </summary>
        private readonly IPublisher publisher;

        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IRepository<User> userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="publisher">The publisher.</param>
        public UserService(
            IRepository<User> userRepository,
            IPublisher publisher)
        {
            this.userRepository = userRepository;
            this.publisher = publisher;
        }

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task Delete(User user)
        {
            user.Deleted = true;

            await this.userRepository.UpdateAsync(user);

            await this.publisher.EntityDeleted(user);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="role">The role.</param>
        /// <param name="hostelId">The hostel identifier.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// the list of users
        /// </returns>
        public async Task<IPagedList<User>> GetAll(
            string keyword = null,
            Role? role = null,
            int? hostelId = null,
            SortUserBy sortBy = SortUserBy.Recent,
            int page = 0,
            int pageSize = int.MaxValue)
        {
            var query = this.userRepository.Table
                .Include(c => c.Hostel)
                .Where(c => !c.Deleted);

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => c.Name.Contains(keyword) || c.Email.Contains(keyword));
            }

            if (role.HasValue)
            {
                var roleId = Convert.ToInt16(role);
                query = query.Where(c => c.RoleId == roleId);
            }

            if (hostelId.HasValue)
            {
                query = query.Where(c => c.HostelId == hostelId.Value);
            }

            switch (sortBy)
            {
                case SortUserBy.Name:
                    query = query.OrderBy(c => c.Name);
                    break;

                case SortUserBy.Recent:
                    query = query.OrderByDescending(c => c.Id);
                    break;
            }

            return await new PagedList<User>().Async(query, page, pageSize);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeHostel">includes the hostel information</param>
        /// <returns>
        /// the user
        /// </returns>
        public User GetById(int id, bool includeHostel = false)
        {
            var query = this.userRepository.TableNoTracking;

            if (includeHostel)
            {
                query = query.Include(c => c.Hostel).AsQueryable();
            }

            return query.FirstOrDefault(c => c.Id == id && !c.Deleted);
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

        /// <summary>
        /// Inserts the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task Insert(User user)
        {
            try
            {
                if (!this.userRepository.Table.Any(c => c.Email.Equals(user.Email)))
                {
                    await this.userRepository.InsertAsync(user);

                    await this.publisher.EntityInserted(user);
                }
                else
                {
                    throw new HostaliandoException("Email", HostaliandoExceptionCode.UserEmailAlreadyExists);
                }
            }
            catch (DbUpdateException e)
            {
                var inner = (SqlException)e.InnerException;

                if (inner.Number == 547)
                {
                    this.Throw547Exception(inner);
                }
            }
        }

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// the task
        /// </returns>
        /// <exception cref="HostaliandoException">the email already used</exception>
        public async Task Update(User user)
        {
            try
            {
                if (!this.userRepository.Table.Any(c => c.Email.Equals(user.Email) && c.Id != user.Id))
                {
                    await this.userRepository.UpdateAsync(user);

                    await this.publisher.EntityUpdated(user);
                }
                else
                {
                    throw new HostaliandoException("Email", HostaliandoExceptionCode.UserEmailAlreadyExists);
                }
            }
            catch (DbUpdateException e)
            {
                var inner = (SqlException)e.InnerException;

                if (inner.Number == 547)
                {
                    this.Throw547Exception(inner);
                }
            }
        }

        /// <summary>
        /// Throws a 547 the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <exception cref="HostaliandoException">the exception</exception>
        private void Throw547Exception(SqlException ex)
        {
            var target = "Unknown";

            if (ex.Message.IndexOf("FK_Users_Hostels") != -1)
            {
                target = "Hostel";
            }

            throw new HostaliandoException(target, HostaliandoExceptionCode.InvalidForeignKey);
        }
    }
}