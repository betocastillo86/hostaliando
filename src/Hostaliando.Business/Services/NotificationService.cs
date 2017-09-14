//-----------------------------------------------------------------------
// <copyright file="NotificationService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Hostaliando.Data;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Notification Service
    /// </summary>
    /// <seealso cref="Hostaliando.Business.Services.INotificationService" />
    public class NotificationService : INotificationService
    {
        /// <summary>
        /// The email notification repository
        /// </summary>
        private readonly IRepository<EmailNotification> emailNotificationRepository;

        /// <summary>
        /// The notification repository
        /// </summary>
        private readonly IRepository<Notification> notificationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationService"/> class.
        /// </summary>
        /// <param name="notificationRepository">The notification repository.</param>
        /// <param name="emailNotificationRepository">the email notification repository</param>
        public NotificationService(
            IRepository<Notification> notificationRepository,
            IRepository<EmailNotification> emailNotificationRepository)
        {
            this.notificationRepository = notificationRepository;
            this.emailNotificationRepository = emailNotificationRepository;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// the list
        /// </returns>
        public async Task<IPagedList<Notification>> GetAll(string keyword = null, int page = 0, int pageSize = int.MaxValue)
        {
            var query = this.notificationRepository.TableNoTracking
                .Where(c => !c.Deleted);

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => c.Name.Contains(keyword));
            }

            return await new PagedList<Notification>().Async(query, page, pageSize);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// the notification
        /// </returns>
        public async Task<Notification> GetById(int id)
        {
            return await this.notificationRepository.Table.FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Gets the email notification by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// the notification
        /// </returns>
        public async Task<EmailNotification> GetEmailNotificationById(int id)
        {
            return await this.emailNotificationRepository.TableNoTracking.FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Gets the email notifications.
        /// </summary>
        /// <param name="sent">The sent.</param>
        /// <param name="to">the To.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// the notifications
        /// </returns>
        public async Task<IPagedList<EmailNotification>> GetEmailNotifications(bool? sent = null, string to = null, string subject = null, string body = null, int page = 0, int pageSize = int.MaxValue)
        {
            var query = this.emailNotificationRepository.Table;

            if (sent.HasValue)
            {
                query = sent.Value ? query.Where(c => c.SentDate != null) : query.Where(c => c.SentDate == null);
            }

            if (!string.IsNullOrEmpty(to))
            {
                query = query.Where(c => c.To.Contains(to));
            }

            if (!string.IsNullOrEmpty(subject))
            {
                query = query.Where(c => c.Subject.Contains(subject));
            }

            if (!string.IsNullOrEmpty(body))
            {
                query = query.Where(c => c.Body.Contains(body));
            }

            query = query.OrderByDescending(c => c.CreatedDate);

            return await new PagedList<EmailNotification>().Async(query, page, pageSize);
        }

        /// <summary>
        /// Inserts the email notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task InsertEmailNotification(EmailNotification notification)
        {
            notification.CreatedDate = DateTime.Now;
            await this.emailNotificationRepository.InsertAsync(notification);
        }

        /// <summary>
        /// Updates the specified notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task Update(Notification notification)
        {
            notification.UpdateDate = DateTime.UtcNow;

            await this.notificationRepository.UpdateAsync(notification);
        }

        /// <summary>
        /// Updates the email notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task UpdateEmailNotification(EmailNotification notification)
        {
            await this.emailNotificationRepository.UpdateAsync(notification);
        }
    }
}