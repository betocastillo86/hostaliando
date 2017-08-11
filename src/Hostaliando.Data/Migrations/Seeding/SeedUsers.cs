//-----------------------------------------------------------------------
// <copyright file="SeedUsers.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data.Migrations
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Seed users
    /// </summary>
    public static class SeedUsers
    {
        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void Seed(HostaliandoContext context)
        {
            var users = new List<User>();

            users.Add(new User { Name = "Gabriel", Email = "admin@admin.com", Password = "de0395eac174d64f431eb79a30e3dac76cb936f4", Salt = "123456", RoleId = 1, TimeZone = -5 });

            foreach (var item in users)
            {
                if (!context.Users.Any(c => c.Email.Equals(item.Email)))
                {
                    context.Users.Add(item);
                }
            }

            context.SaveChanges();
        }
    }
}