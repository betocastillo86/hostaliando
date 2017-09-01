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
        /// The notification repository
        /// </summary>
        private readonly IRepository<Notification> notificationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationService"/> class.
        /// </summary>
        /// <param name="notificationRepository">The notification repository.</param>
        public NotificationService(IRepository<Notification> notificationRepository)
        {
            this.notificationRepository = notificationRepository;
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
    }
}