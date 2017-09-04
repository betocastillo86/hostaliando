//-----------------------------------------------------------------------
// <copyright file="NotificationExtensions.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Hostaliando.Data;

    /// <summary>
    /// Notification Extensions
    /// </summary>
    public static class NotificationExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <returns>the model</returns>
        public static NotificationModel ToModel(this Notification notification)
        {
            return new NotificationModel
            {
                Active = notification.Active,
                EmailHtml = notification.EmailHtml,
                EmailSubject = notification.EmailSubject,
                Id = notification.Id,
                IsEmail = notification.IsEmail,
                IsSystem = notification.IsSystem,
                Name = notification.Name,
                SystemText = notification.SystemText,
                Tags = notification.Tags
            };
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="notifications">The notifications.</param>
        /// <returns>the list</returns>
        public static IList<NotificationModel> ToModels(this ICollection<Notification> notifications)
        {
            return notifications.Select(ToModel).ToList();
        }
    }
}