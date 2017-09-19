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
                    EmailHtml = "<h2 style=\"color: #304a6f;text-align: center;font-size: 22px;\"><b>!Nos alegra mucho que te hayas unido a Hostaliando!</b></h2>                      <p><br></p> <p>Hola %%NotifiedUser.Name%%,</p>                      <p>queremos comunicarte que el perfil ya está creado en nuestra página y podrás empezar a:</p>  <ul>  	<li>Crear habitaciones</li> 	<li>Crear reservas</li> 	<li>Organizar tu calendario</li>  </ul>  <p>Para empezar debes cambiar tu clave.</p>                      <p><a href=\"%%RootUrl%%/passwordrecovery/%%User.PasswordRecoveryToken%%\" style=\"color: #FFF; background: #3C75C2; font-size: 20px;  text-decoration: none; margin: 10px auto; display: block; min-width: 140px; text-align: center; border-radius: 6px; padding: 10px 0;\">Crear mi clave</a></p>",
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
                    EmailHtml = "<h2 style=\"color: #304a6f;text-align: center;font-size: 22px;\"><b>!Cambia tu clave!</b></h2>                       <p><br></p>                       <p>Hola %%NotifiedUser.Name%%,</p>                       <p>ya puedes realizar el cambio de la clave:</p>                       <p><a href=\"%%Url%%\" style=\"color: #FFF; background: #3C75C2; font-size: 20px;  text-decoration: none; margin: 10px auto; display: block; min-width: 140px; text-align: center; border-radius: 6px; padding: 10px 0;\">Cambiar clave</a></p>",
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