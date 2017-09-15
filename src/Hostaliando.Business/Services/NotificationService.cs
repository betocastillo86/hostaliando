//-----------------------------------------------------------------------
// <copyright file="NotificationService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Caching;
    using Beto.Core.Data;
    using Beto.Core.Data.Notifications;
    using Beto.Core.Data.Users;
    using Hostaliando.Business.Caching;
    using Hostaliando.Business.Configuration;
    using Hostaliando.Data;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Notification Service
    /// </summary>
    /// <seealso cref="Hostaliando.Business.Services.INotificationService" />
    public class NotificationService : INotificationService
    {
        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager cacheManager;

        /// <summary>
        /// The core notification service
        /// </summary>
        private readonly ICoreNotificationService coreNotificationService;

        /// <summary>
        /// The email notification repository
        /// </summary>
        private readonly IRepository<EmailNotification> emailNotificationRepository;

        /// <summary>
        /// The general settings
        /// </summary>
        private readonly IGeneralSettings generalSettings;

        /// <summary>
        /// The notification repository
        /// </summary>
        private readonly IRepository<Notification> notificationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationService"/> class.
        /// </summary>
        /// <param name="notificationRepository">The notification repository.</param>
        /// <param name="coreNotificationService">The core notification service.</param>
        /// <param name="generalSettings">The general settings.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="emailNotificationRepository">The email notification repository.</param>
        public NotificationService(
            IRepository<Notification> notificationRepository,
            ICoreNotificationService coreNotificationService,
            IGeneralSettings generalSettings,
            ICacheManager cacheManager,
            IRepository<EmailNotification> emailNotificationRepository)
        {
            this.notificationRepository = notificationRepository;
            this.coreNotificationService = coreNotificationService;
            this.generalSettings = generalSettings;
            this.cacheManager = cacheManager;
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
        /// the task
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
        /// Inserts the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="userTriggerEvent">The user trigger event.</param>
        /// <param name="type">The type.</param>
        /// <param name="targetUrl">The target URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task NewNotification(
            User user,
            User userTriggerEvent,
            NotificationType type,
            string targetUrl,
            IList<NotificationParameter> parameters)
        {
            await this.NewNotification(user, userTriggerEvent, type, targetUrl, parameters, null, null, null);
        }

        /// <summary>
        /// Creates a notification.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="userTriggerEvent">The user trigger event.</param>
        /// <param name="type">The type.</param>
        /// <param name="targetUrl">The target URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="defaultFromName">The default from name.</param>
        /// <param name="defaultSubject">The default subject.</param>
        /// <param name="defaultMessage">The default message.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task NewNotification(
                    User user,
                    User userTriggerEvent,
                    NotificationType type,
                    string targetUrl,
                    IList<NotificationParameter> parameters,
                    string defaultFromName,
                    string defaultSubject,
                    string defaultMessage)
        {
            var list = new List<User>() { user };
            await this.NewNotification(list, userTriggerEvent, type, targetUrl, parameters, defaultFromName, defaultSubject, defaultMessage);
        }

        /// <summary>
        /// Creates a notification.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <param name="userTriggerEvent">The user trigger event.</param>
        /// <param name="type">The type.</param>
        /// <param name="targetUrl">The target URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task NewNotification(
                    IList<User> users,
                    User userTriggerEvent,
                    NotificationType type,
                    string targetUrl,
                    IList<NotificationParameter> parameters)
        {
            await this.NewNotification(users, userTriggerEvent, type, targetUrl, parameters, null, null, null);
        }

        /// <summary>
        /// Creates a notification.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <param name="userTriggerEvent">The user trigger event.</param>
        /// <param name="type">The type.</param>
        /// <param name="targetUrl">The target URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="defaultFromName">The default from name.</param>
        /// <param name="defaultSubject">The default subject.</param>
        /// <param name="defaultMessage">The default message.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task NewNotification(
                    IList<User> users,
                    User userTriggerEvent,
                    NotificationType type,
                    string targetUrl,
                    IList<NotificationParameter> parameters,
                    string defaultFromName,
                    string defaultSubject,
                    string defaultMessage)
        {
            var notificationId = Convert.ToInt32(type);
            var notification = this.GetCachedNotifications()
                .FirstOrDefault(n => n.Id == notificationId);

            var settings = new NotificationSettings()
            {
                BaseHtml = this.generalSettings.BodyBaseHtml,
                DefaultFromName = defaultFromName,
                DefaultMessage = defaultMessage,
                DefaultSubject = defaultSubject,
                IsManual = false,
                SiteUrl = this.generalSettings.SiteUrl
            };

            await this.coreNotificationService.NewNotification<SystemNotification, EmailNotification>(
                users.Select(c => (IUserEntity)c).ToList(),
                userTriggerEvent,
                notification,
                targetUrl,
                parameters,
                settings);
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

        /// <summary>
        /// Gets the cached notifications.
        /// </summary>
        /// <returns>the notifications</returns>
        private IList<Notification> GetCachedNotifications()
        {
            return this.cacheManager.Get(
                CacheKeys.NOTIFICATIONS_ALL,
                () =>
                {
                    return this.notificationRepository.Table.ToList();
                });
        }
    }
}