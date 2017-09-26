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
                new SystemSetting() { Name = "GeneralSettings.BodyBaseHtml", Value = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\"><head> <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"/> <title>Narrative Confirm Email</title> <style type=\"text/css\"> /* Take care of image borders and formatting */ img{max-width: 600px; outline: none; text-decoration: none; -ms-interpolation-mode: bicubic;}a{border: 0; outline: none;}a img{border: none;}/* General styling */ td, h1, h2, h3{font-family: Helvetica, Arial, sans-serif; font-weight: 400;}td{font-size: 13px; line-height: 150%; text-align: left;}body{-webkit-font-smoothing:antialiased; -webkit-text-size-adjust:none; width: 100%; height: 100%; color: #37302d; background: #ffffff;}table{border-collapse: collapse !important;}h1, h2, h3{padding: 0; margin: 0; color: #444444; font-weight: 400; line-height: 110%;}h1{font-size: 35px;}h2{font-size: 30px;}h3{font-size: 24px;}h4{font-size: 18px; font-weight: normal;}.important-font{color: #21BEB4; font-weight: bold;}.hide{display: none !important;}.force-full-width{width: 100% !important;}</style> <style type=\"text/css\" media=\"screen\"> @media screen{@import url(http://fonts.googleapis.com/css?family=Open+Sans:400); /* Thanks Outlook 2013! */ td, h1, h2, h3{font-family: 'Open Sans', 'Helvetica Neue', Arial, sans-serif !important;}}</style> <style type=\"text/css\" media=\"only screen and (max-width: 600px)\"> /* Mobile styles */ @media only screen and (max-width: 600px){table[class=\"w320\"]{width: 320px !important;}table[class=\"w300\"]{width: 300px !important;}table[class=\"w290\"]{width: 290px !important;}td[class=\"w320\"]{width: 320px !important;}td[class~=\"mobile-padding\"]{padding-left: 14px !important; padding-right: 14px !important;}td[class*=\"mobile-padding-left\"]{padding-left: 14px !important;}td[class*=\"mobile-padding-right\"]{padding-right: 14px !important;}td[class*=\"mobile-block\"]{display: block !important; width: 100% !important; text-align: left !important; padding-left: 0 !important; padding-right: 0 !important; padding-bottom: 15px !important;}td[class*=\"mobile-no-padding-bottom\"]{padding-bottom: 0 !important;}td[class~=\"mobile-center\"]{text-align: center !important;}table[class*=\"mobile-center-block\"]{float: none !important; margin: 0 auto !important;}*[class*=\"mobile-hide\"]{display: none !important; width: 0 !important; height: 0 !important; line-height: 0 !important; font-size: 0 !important;}td[class*=\"mobile-border\"]{border: 0 !important;}}</style></head><body class=\"body\" style=\"padding:0; margin:0; display:block; background:#ffffff; -webkit-text-size-adjust:none\" bgcolor=\"#ffffff\"><table align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" height=\"100%\"> <tr> <td align=\"center\" valign=\"top\" bgcolor=\"#ffffff\" width=\"100%\"> <table cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr> <td style=\"background:#1f1f1f\" width=\"100%\"> <center> <table cellspacing=\"0\" cellpadding=\"0\" width=\"600\" class=\"w320\"> <tr> <td valign=\"top\" class=\"mobile-block mobile-no-padding-bottom mobile-center\" width=\"270\" style=\"background:#1f1f1f;padding:10px 10px 10px 20px;\"> <a href=\"#\" style=\"text-decoration:none;\"> <img src=\"%%RootUrl%%/img/logohostaliando100.png\" width=\"142\" height=\"30\" alt=\"Hostaliando\"/> </a> </td><td valign=\"top\" class=\"mobile-block mobile-center\" width=\"270\" style=\"background:#1f1f1f;padding:10px 15px 10px 10px\"> </td></tr></table> </center> </td></tr><tr><td style=\"background-color:#ffffff;\"><center>%%Body%%</center></td><tr> <tr> <td style=\"background-color:#1f1f1f;\"> <center> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" class=\"w320\" style=\"height:100%;color:#ffffff\" bgcolor=\"#1f1f1f\" > <tr> <td align=\"right\" valign=\"middle\" class=\"mobile-padding\" style=\"font-size:12px;padding:20px; background-color:#1f1f1f; color:#ffffff; text-align:left; \"> <a style=\"color:#ffffff;\" href=\"mailto:hostaliando@gmail.com\">Contactanos</a>&nbsp;&nbsp;|&nbsp;&nbsp; <a style=\"color:#ffffff;\" href=\"http://www.hostaliando.com\">Conoce más</a> </td></tr></table> </center> </td></tr></table> </td></tr></table></body></html>" },
                new SystemSetting() { Name = "GeneralSettings.DateFormat", Value = "YYYY/MM/DD" },
                new SystemSetting() { Name = "TaskSettings.SendEmailsInterval", Value = "60" },
                new SystemSetting() { Name = "NotificationSettings.TakeEmailsToSend", Value = "30" },
                new SystemSetting() { Name = "NotificationSettings.MaxAttemptsToSendEmail", Value = "5" },
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