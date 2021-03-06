﻿//-----------------------------------------------------------------------
// <copyright file="INotificationSettings.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Configuration
{
    /// <summary>
    /// Interface of notification settings
    /// </summary>
    public interface INotificationSettings
    {
        /// <summary>
        /// Gets the email sender email.
        /// </summary>
        /// <value>
        /// The email sender email.
        /// </value>
        string EmailSenderEmail { get; }

        /// <summary>
        /// Gets the name of the email sender.
        /// </summary>
        /// <value>
        /// The name of the email sender.
        /// </value>
        string EmailSenderName { get; }

        /// <summary>
        /// Gets the maximum attempts to send email.
        /// </summary>
        /// <value>
        /// The maximum attempts to send email.
        /// </value>
        int MaxAttemptsToSendEmail { get; }

        /// <summary>
        /// Gets a value indicating whether [send email enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [send email enabled]; otherwise, <c>false</c>.
        /// </value>
        bool SendEmailEnabled { get; }

        /// <summary>
        /// Gets the SMTP host.
        /// </summary>
        /// <value>
        /// The SMTP host.
        /// </value>
        string SmtpHost { get; }

        /// <summary>
        /// Gets the SMTP password.
        /// </summary>
        /// <value>
        /// The SMTP password.
        /// </value>
        string SmtpPassword { get; }

        /// <summary>
        /// Gets the SMTP port.
        /// </summary>
        /// <value>
        /// The SMTP port.
        /// </value>
        int SmtpPort { get; }

        /// <summary>
        /// Gets the SMTP user.
        /// </summary>
        /// <value>
        /// The SMTP user.
        /// </value>
        string SmtpUser { get; }

        /// <summary>
        /// Gets a value indicating whether [SMTP use SSL].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [SMTP use SSL]; otherwise, <c>false</c>.
        /// </value>
        bool SmtpUseSsl { get; }

        /// <summary>
        /// Gets the take emails to send.
        /// </summary>
        /// <value>
        /// The take emails to send.
        /// </value>
        int TakeEmailsToSend { get; }
    }
}