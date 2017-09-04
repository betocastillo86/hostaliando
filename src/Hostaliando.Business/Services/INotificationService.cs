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
        /// Updates the specified notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <returns>the task</returns>
        Task Update(Notification notification);
    }
}