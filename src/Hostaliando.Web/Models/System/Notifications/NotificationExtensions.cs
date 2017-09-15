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
        /// To the entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the entity</returns>
        public static EmailNotification ToEntity(this EmailNotificationModel model)
        {
            return new EmailNotification
            {
                Id = model.Id,
                CreatedDate = model.CreatedDate,
                SentDate = model.SentDate,
                SentTries = model.SentTries,
                Subject = model.Subject,
                To = model.To,
                Body = model.Body
            };
        }

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
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the model</returns>
        public static EmailNotificationModel ToModel(this EmailNotification entity)
        {
            return new EmailNotificationModel
            {
                Id = entity.Id,
                CreatedDate = entity.CreatedDate,
                SentDate = entity.SentDate,
                SentTries = entity.SentTries,
                Subject = entity.Subject,
                To = entity.To,
                Body = entity.Body
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

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>the models</returns>
        public static IList<EmailNotificationModel> ToModels(this ICollection<EmailNotification> entities)
        {
            return entities.Select(ToModel).ToList();
        }
    }
}