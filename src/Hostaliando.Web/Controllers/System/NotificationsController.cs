//-----------------------------------------------------------------------
// <copyright file="NotificationsController.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Controllers
{
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Beto.Core.Web.Api.Filters;
    using Hostaliando.Business.Services;
    using Hostaliando.Web.Infraestructure.Filters;
    using Hostaliando.Web.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Notifications Controller
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    [Route("api/v1/notifications")]
    public class NotificationsController : BaseApiController
    {
        /// <summary>
        /// The notification service
        /// </summary>
        private readonly INotificationService notificationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationsController"/> class.
        /// </summary>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        /// <param name="notificationService">The notification service.</param>
        public NotificationsController(
            IMessageExceptionFinder messageExceptionFinder,
            INotificationService notificationService) : base(messageExceptionFinder)
        {
            this.notificationService = notificationService;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <param name="filter">the filter</param>
        /// <returns>the action</returns>
        [HttpGet]
        [ServiceFilter(typeof(AuthorizeAdminAttribute))]
        public async Task<IActionResult> Get([FromQuery] NotificationFilterModel filter)
        {
            var notifications = await this.notificationService.GetAll(filter.Name, filter.Page, filter.PageSize);

            var models = notifications.ToModels();

            return this.Ok(models, notifications.HasNextPage, notifications.TotalCount);
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the notification</returns>
        [HttpGet]
        [ServiceFilter(typeof(AuthorizeAdminAttribute))]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var notification = await this.notificationService.GetById(id);

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
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>the action</returns>
        [HttpPut]
        [RequiredModel]
        [Route("{id:int}")]
        [ServiceFilter(typeof(AuthorizeAdminAttribute))]
        public async Task<IActionResult> Put(int id, [FromBody] NotificationModel model)
        {
            var notification = await this.notificationService.GetById(id);

            if (notification != null)
            {
                notification.Name = model.Name;
                notification.IsEmail = model.IsEmail;
                notification.IsSystem = model.IsSystem;
                notification.EmailHtml = model.EmailHtml;
                notification.SystemText = model.SystemText;
                notification.EmailSubject = model.EmailSubject;
                notification.Active = model.Active;

                await this.notificationService.Update(notification);

                return this.Ok(new { result = true });
            }
            else
            {
                return this.NotFound();
            }
        }
    }
}