//-----------------------------------------------------------------------
// <copyright file="INotificationService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Hostaliando.Data;

    /// <summary>
    /// Interface of notification service
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>the list</returns>
        Task<IPagedList<Notification>> GetAll(string keyword = null, int page = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the notification</returns>
        Task<Notification> GetById(int id);

        /// <summary>
        /// Gets the email notification by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the notification</returns>
        Task<EmailNotification> GetEmailNotificationById(int id);

        /// <summary>
        /// Gets the email notifications.
        /// </summary>
        /// <param name="sent">The sent.</param>
        /// <param name="to">the To.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>the notifications</returns>
        Task<IPagedList<EmailNotification>> GetEmailNotifications(
            bool? sent = null,
            string to = null,
            string subject = null,
            string body = null,
            int page = 0,
            int pageSize = int.MaxValue);

        /// <summary>
        /// Inserts the email notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <returns>the task</returns>
        Task InsertEmailNotification(EmailNotification notification);

        /// <summary>
        /// Updates the specified notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <returns>the task</returns>
        Task Update(Notification notification);

        /// <summary>
        /// Updates the email notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <returns>the task</returns>
        Task UpdateEmailNotification(EmailNotification notification);
    }
}