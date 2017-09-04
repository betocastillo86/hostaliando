//-----------------------------------------------------------------------
// <copyright file="SeedNotifications.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data.Migrations
{
    using System;
    using System.Linq;

    /// <summary>
    /// Seed Notifications
    /// </summary>
    public static class SeedNotifications
    {
        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void Seed(HostaliandoContext context)
        {
            var list = new Notification[]
            {
                new Notification
                {
                    Id = Convert.ToInt32(NotificationType.Welcome),
                    Name = "Registro de usuarios",
                    Active = true,
                    EmailHtml = "Bienvenido a Hostaliando %%User.Name%%",
                    EmailSubject = "Bienvenido a Hostaliando",
                    SystemText = null,
                    IsEmail = true,
                    IsSystem = false,
                    Tags = "%%User.Name%%"
                }
            };

            foreach (var item in list)
            {
                if (!context.Notifications.Any(c => c.Id.Equals(item.Id)))
                {
                    context.Notifications.Add(item);
                }
            }

            context.SaveChanges();
        }
    }
}