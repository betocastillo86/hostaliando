namespace Hostaliando.Business.Subscribers.Notifications
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Beto.Core.Data.Notifications;
    using Beto.Core.EventPublisher;
    using Hostaliando.Business.Services;
    using Hostaliando.Data;

    /// <summary>
    /// User Notifications Subscriber
    /// </summary>
    /// <seealso cref="Beto.Core.EventPublisher.ISubscriber{Beto.Core.EventPublisher.EntityInsertedMessage{Hostaliando.Data.User}}" />
    public class UserNotificationsSubscriber : ISubscriber<EntityInsertedMessage<User>>
    {
        /// <summary>
        /// The notification service
        /// </summary>
        private readonly INotificationService notificationService;

        /// <summary>
        /// The SEO service
        /// </summary>
        private readonly ISeoService seoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotificationsSubscriber"/> class.
        /// </summary>
        /// <param name="notificationService">The notification service.</param>
        /// <param name="seoService">The seo service.</param>
        public UserNotificationsSubscriber(
            INotificationService notificationService,
            ISeoService seoService)
        {
            this.notificationService = notificationService;
            this.seoService = seoService;
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task HandleEvent(EntityInsertedMessage<User> message)
        {
            var user = message.Entity;
            var targetUrl = this.seoService.GetFullRoute("login");

            var parameters = new List<NotificationParameter>();
            parameters.AddOrReplace("User.PasswordRecoveryToken", user.PasswordRecoveryToken);

            await this.notificationService.NewNotification(
                user,
                null,
                NotificationType.Welcome,
                targetUrl,
                parameters);
        }
    }
}