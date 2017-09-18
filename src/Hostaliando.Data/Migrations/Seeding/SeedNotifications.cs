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
                    EmailHtml = "<table cellpadding=\"0\" cellspacing=\"0\" width=\"600\" class=\"w320\">              <tbody><tr>                <td class=\"mobile-padding\">                  <br class=\"mobile-hide\">                  <h2>Recuerda tu contraseña!</h2><br>                  Hola %%NotifiedUser.Name%%,<br>                  Para restaurar tu clave dale clic en el botón..<br>                  <br>                  <table cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" bgcolor=\"#ffffff\">                    <tbody><tr>                      <td style=\"width: 100px;\">                        <div>                                                                                                           <center>                                                        <b style=\"font-size: 13px;text-align: center;width: 100px;\"><a href=\"%%Url%%\" style=\"background-color: #d84a38;color: #ffffff;font-size: 13px;text-align: center;width: 100px;\">Recordar Clave</a></b>                            </center>                                                                            </div>                      </td>                      <td width=\"281\" style=\"background-color: #ffffff;\">&nbsp;</td>                    </tr>                  </tbody></table>                </td>                <td class=\"mobile-hide\" style=\"vertical-align: bottom;\">                  <table cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">                    <tbody><tr>                      <td style=\"vertical-align: bottom;text-align: right;\">                        <img style=\"vertical-align: bottom;\" src=\"https://www.filepicker.io/api/file/9f3sP1z8SeW1sMiDA48o\" width=\"174\" height=\"294\">                      </td>                    </tr>                  </tbody></table>                </td>              </tr>            </tbody></table>",
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