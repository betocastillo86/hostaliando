//-----------------------------------------------------------------------
// <copyright file="UserExtensions.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using System.Collections.Generic;
    using System.Linq;
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

        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>the model</returns>
        public static UserModel ToModel(this User user)
        {
            return new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Hostel = user.Hostel?.ToModel()
            };
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <returns>the models</returns>
        public static IList<UserModel> ToModels(this ICollection<User> users)
        {
            return users.Select(ToModel).ToList();
        }
    }
}