//-----------------------------------------------------------------------
// <copyright file="NotificationSettings.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Configuration
{
    using Beto.Core.Data.Configuration;
    using Hostaliando.Business.Services.Extensions;

    /// <summary>
    /// Notification Settings
    /// </summary>
    /// <seealso cref="Hostaliando.Business.Configuration.INotificationSettings" />
    public class NotificationSettings : INotificationSettings
    {
        /// <summary>
        /// The setting service
        /// </summary>
        private readonly ICoreSettingService coreSettingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationSettings"/> class.
        /// </summary>
        /// <param name="coreSettingService">The setting service.</param>
        public NotificationSettings(ICoreSettingService coreSettingService)
        {
            this.coreSettingService = coreSettingService;
        }

        /// <summary>
        /// Gets the email sender email.
        /// </summary>
        /// <value>
        /// The email sender email.
        /// </value>
        public string EmailSenderEmail => this.coreSettingService.Get<string>("NotificationSettings.EmailSenderEmail");

        /// <summary>
        /// Gets the name of the email sender.
        /// </summary>
        /// <value>
        /// The name of the email sender.
        /// </value>
        public string EmailSenderName => this.coreSettingService.Get<string>("NotificationSettings.EmailSenderName");

        /// <summary>
        /// Gets the maximum attemtps to send email.
        /// </summary>
        /// <value>
        /// The maximum attemtps to send email.
        /// </value>
        public int MaxAttemtpsToSendEmail => this.coreSettingService.Get<int>("NotificationSettings.MaxAttemtpsToSendEmail");

        /// <summary>
        /// Gets a value indicating whether [send email enabled].
        /// </summary>
        /// <value>
        /// <c>true</c> if [send email enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool SendEmailEnabled => this.coreSettingService.Get<bool>("NotificationSettings.SendEmailEnabled");

        /// <summary>
        /// Gets the SMTP host.
        /// </summary>
        /// <value>
        /// The SMTP host.
        /// </value>
        public string SmtpHost => this.coreSettingService.Get<string>("NotificationSettings.SmtpHost");

        /// <summary>
        /// Gets the SMTP password.
        /// </summary>
        /// <value>
        /// The SMTP password.
        /// </value>
        public string SmtpPassword => this.coreSettingService.Get<string>("NotificationSettings.SmtpPassword");

        /// <summary>
        /// Gets the SMTP port.
        /// </summary>
        /// <value>
        /// The SMTP port.
        /// </value>
        public int SmtpPort => this.coreSettingService.Get<int>("NotificationSettings.SmtpPort");

        /// <summary>
        /// Gets the SMTP user.
        /// </summary>
        /// <value>
        /// The SMTP user.
        /// </value>
        public string SmtpUser => this.coreSettingService.Get<string>("NotificationSettings.SmtpUser");

        /// <summary>
        /// Gets a value indicating whether [SMTP use SSL].
        /// </summary>
        /// <value>
        /// <c>true</c> if [SMTP use SSL]; otherwise, <c>false</c>.
        /// </value>
        public bool SmtpUseSsl => this.coreSettingService.Get<bool>("NotificationSettings.SmtpUseSsl");

        /// <summary>
        /// Gets the take emails to send.
        /// </summary>
        /// <value>
        /// The take emails to send.
        /// </value>
        public int TakeEmailsToSend => this.coreSettingService.Get<int>("NotificationSettings.TakeEmailsToSend");
    }
}