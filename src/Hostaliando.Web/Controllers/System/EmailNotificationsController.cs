//-----------------------------------------------------------------------
// <copyright file="EmailNotificationsController.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Controllers
{
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Hostaliando.Business.Exceptions;
    using Hostaliando.Business.Services;
    using Hostaliando.Data;
    using Hostaliando.Web.Infraestructure.Filters;
    using Hostaliando.Web.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Email Notifications Controller
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    [Route("api/v1/emailnotifications")]
    public class EmailNotificationsController : BaseApiController
    {
        /// <summary>
        /// The notification service
        /// </summary>
        private readonly INotificationService notificationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailNotificationsController"/> class.
        /// </summary>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        /// <param name="notificationService">The notification service.</param>
        public EmailNotificationsController(
            IMessageExceptionFinder messageExceptionFinder,
            INotificationService notificationService) : base(messageExceptionFinder)
        {
            this.notificationService = notificationService;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the action</returns>
        [HttpGet]
        [ServiceFilter(typeof(AuthorizeAdminAttribute))]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var notification = await this.notificationService.GetEmailNotificationById(id);

            if (notification != null)
            {
                var model = notification.ToModel();
                return this.Ok(model);
            }
            else
            {
                return this.NotFound();
            }
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>the action</returns>
        [HttpGet]
        [ServiceFilter(typeof(AuthorizeAdminAttribute))]
        public async Task<IActionResult> Get([FromQuery]EmailNotificationFilterModel filter)
        {
            var notifications = await this.notificationService.GetEmailNotifications(
                   filter.Sent,
                   filter.To,
                   filter.Subject,
                   null,
                   filter.Page,
                   filter.PageSize);

            var models = notifications.ToModels();
            return this.Ok(models, notifications.HasNextPage, notifications.TotalCount);
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the task</returns>
        [HttpPost]
        [ServiceFilter(typeof(AuthorizeAdminAttribute))]
        public async Task<IActionResult> Post([FromBody] EmailNotificationModel model)
        {
            var entity = new EmailNotification();
            entity.To = model.To;
            entity.Subject = model.Subject;
            entity.Body = model.Body;

            await this.notificationService.InsertEmailNotification(entity);

            return this.Ok(new BaseModel() { Id = entity.Id });
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>the task</returns>
        [HttpPut]
        [ServiceFilter(typeof(AuthorizeAdminAttribute))]
        [Route("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmailNotificationModel model)
        {
            var notification = await this.notificationService.GetEmailNotificationById(id);

            if (notification == null)
            {
                return this.NotFound();
            }

            if (!notification.SentDate.HasValue)
            {
                notification.To = model.To;
                notification.Body = model.Body;
                notification.Subject = model.Subject;

                await this.notificationService.UpdateEmailNotification(notification);

                return this.Ok();
            }
            else
            {
                return this.BadRequest(HostaliandoExceptionCode.CantUpdateEmailNotification, "No se puede modificar una notificación ya enviada");
            }
        }
    }
}