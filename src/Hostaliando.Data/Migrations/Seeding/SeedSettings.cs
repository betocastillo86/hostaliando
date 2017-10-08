//-----------------------------------------------------------------------
// <copyright file="SeedSettings.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data.Migrations
{
    using System.Linq;

    /// <summary>
    /// Seed Settings
    /// </summary>
    public static class SeedSettings
    {
        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void Seed(HostaliandoContext context)
        {
            var list = new SystemSetting[]
            {
                new SystemSetting() { Name = "GeneralSettings.SiteUrl", Value = "http://localhost:64901/" },
                new SystemSetting() { Name = "GeneralSettings.BodyBaseHtml", Value = "<html> <body style=\"background: #F6F6F7;\">     <table width=\"600\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\" style=\"background:#FFF; font-family: sans-serif; color: #666666;\">         <tbody>             <tr style=\"height:30px\">                 <td colspan=\"3\">&nbsp;</td>             </tr>             <tr>                 <td style=\"width: 30px;\">&nbsp;</td>                 <td>                     <img src=\"%%RootUrl%%img/logohostaliando100.png\" width=\"50%\" alt=\"hostaliando\" style=\"display:block; margin: 0 auto;\" />                     %%Body%% 					                 </td>                 <td style=\"width: 30px;\">&nbsp;</td>             </tr>             <tr style=\"height:40px\">                 <td colspan=\"3\">&nbsp;</td>             </tr>         </tbody>     </table> </body> </html>" },
                new SystemSetting() { Name = "GeneralSettings.DateFormat", Value = "YYYY/MM/DD" },
                new SystemSetting() { Name = "TaskSettings.SendEmailsInterval", Value = "60" },
                new SystemSetting() { Name = "NotificationSettings.TakeEmailsToSend", Value = "30" },
                new SystemSetting() { Name = "NotificationSettings.MaxAttemtpsToSendEmail", Value = "5" },
                new SystemSetting() { Name = "NotificationSettings.SendEmailEnabled", Value = "False" },
                new SystemSetting() { Name = "NotificationSettings.EmailSenderEmail", Value = "hostaliando@gmail.com" },
                new SystemSetting() { Name = "NotificationSettings.EmailSenderName", Value = "Hostaliando" },
                new SystemSetting() { Name = "NotificationSettings.SmtpHost", Value = "smtp.gmail.com" },
                new SystemSetting() { Name = "NotificationSettings.SmtpPassword", Value = "False" },
                new SystemSetting() { Name = "NotificationSettings.SmtpPort", Value = "465" },
                new SystemSetting() { Name = "NotificationSettings.SmtpUser", Value = "hostaliando@gmail.com" },
                new SystemSetting() { Name = "NotificationSettings.SmtpUseSsl", Value = "True" }
            };

            foreach (var item in list)
            {
                if (!context.SystemSettings.Any(c => c.Name.Equals(item.Name)))
                {
                    context.SystemSettings.Add(item);
                }
            }

            context.SaveChanges();
        }
    }
}