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
                    EmailHtml = "Bienvenido a Hostaliando %%NotifiedUser.Name%%",
                    EmailSubject = "Bienvenido a Hostaliando",
                    SystemText = null,
                    IsEmail = true,
                    IsSystem = false,
                    Tags = "%%NotifiedUser.Name%%,%%User.PasswordRecoveryToken%%"
                },
                new Notification
                {
                    Id = Convert.ToInt32(NotificationType.PasswordRecovery),
                    Name = "Recuperación de clave",
                    Active = true,
                    EmailHtml = "Recuper la clave %%NotifiedUser.Name%%, %%Url%%",
                    EmailSubject = "Recuperar clave de Hostaliando",
                    SystemText = null,
                    IsEmail = true,
                    IsSystem = false,
                    Tags = "%%NotifiedUser.Name%%"
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