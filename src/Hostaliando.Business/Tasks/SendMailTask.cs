//-----------------------------------------------------------------------
// <copyright file="SendMailTask.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Tasks
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Beto.Core.Exceptions;
    using Hostaliando.Business.Configuration;
    using Hostaliando.Business.Extensions;
    using Hostaliando.Data;
    using MailKit.Net.Smtp;
    using MimeKit;
    using MimeKit.Text;

    /// <summary>
    /// Send the pending emails
    /// </summary>
    public class SendMailTask : ITask
    {
        /// <summary>
        /// The log service
        /// </summary>
        private readonly ILoggerService logService;

        /// <summary>
        /// The notification service
        /// </summary>
        private readonly IRepository<EmailNotification> notificationRepository;

        /// <summary>
        /// The notification settings
        /// </summary>
        private readonly INotificationSettings notificationSettings;

        /// <summary>
        /// The task settings
        /// </summary>
        private readonly ITaskSettings taskSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendMailTask"/> class.
        /// </summary>
        /// <param name="notificationRepository">The notification repository.</param>
        /// <param name="taskSettings">The task settings.</param>
        /// <param name="logService">The log service.</param>
        /// <param name="notificationSettings">The notification settings.</param>
        public SendMailTask(
            IRepository<EmailNotification> notificationRepository,
            ITaskSettings taskSettings,
            ILoggerService logService,
            INotificationSettings notificationSettings)
        {
            this.notificationRepository = notificationRepository;
            this.taskSettings = taskSettings;
            this.logService = logService;
            this.notificationSettings = notificationSettings;
        }

        /// <summary>
        /// Sends the pending mails.
        /// </summary>
        /// <returns>the task</returns>
        public async Task SendPendingMails()
        {
            var mails = this.notificationRepository.Table
                .Where(c => c.SentDate == null && c.SentTries < this.notificationSettings.MaxAttemtpsToSendEmail)
                .Take(this.notificationSettings.TakeEmailsToSend)
                .ToList();

            try
            {
                foreach (EmailNotification mail in mails)
                {
                    try
                    {
                        this.SendMessage(mail);
                        mail.SentDate = DateTime.Now;
                    }
                    catch (Exception e)
                    {
                        mail.SentTries++;
                        await this.logService.Error(e);
                    }
                }
            }
            catch (Exception e)
            {
                await this.logService.Error(e);
            }
            finally
            {
                if (mails.Count > 0)
                {
                    this.notificationRepository.Update(mails);
                }
            }
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="notification">The notification.</param>
        private void SendMessage(EmailNotification notification)
        {
            if (this.notificationSettings.SendEmailEnabled)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(this.notificationSettings.EmailSenderName, this.notificationSettings.EmailSenderEmail));
                message.To.Add(new MailboxAddress(notification.ToName, notification.To));
                message.Subject = notification.Subject;
                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = notification.Body
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(
                        this.notificationSettings.SmtpHost,
                        this.notificationSettings.SmtpPort,
                        this.notificationSettings.SmtpUseSsl);

                    client.Authenticate(this.notificationSettings.SmtpUser, this.notificationSettings.SmtpPassword);

                    client.Send(message);

                    client.Disconnect(true);
                }
            }
        }
    }
}