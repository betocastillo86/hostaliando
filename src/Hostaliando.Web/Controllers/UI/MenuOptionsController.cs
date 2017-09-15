//-----------------------------------------------------------------------
// <copyright file="MenuOptionsController.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Controllers.UI
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Hostaliando.Business.Security;
    using Hostaliando.Web.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Menu Options Controller
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    [Route("api/v1/menuoptions")]
    public class MenuOptionsController : BaseApiController
    {
        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuOptionsController"/> class.
        /// </summary>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        /// <param name="workContext">The work context.</param>
        public MenuOptionsController(
            IMessageExceptionFinder messageExceptionFinder,
            IWorkContext workContext) : base(messageExceptionFinder)
        {
            this.workContext = workContext;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>the task</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var options = new List<MenuOptionModel>();

            if (this.workContext.CurrentUser.IsAdmin())
            {
                options.Add(new MenuOptionModel() { Id = 1, Name = "Hostales", Key = "Hostels", Url = "/hostels", Icon = "fa-building-o", DisplayOrder = 2 });
                options.Add(new MenuOptionModel() { Id = 4, Name = "Usuarios", Key = "Users", Url = "/users", Icon = "fa-users", DisplayOrder = 4 });
                
                var settings = new MenuOptionModel() { Id = 5, Name = "Configuracion", Key = "SettingsParent", Icon = "fa-cogs", Children = new List<MenuOptionModel>(), Url = "#", DisplayOrder = 5 };
                options.Add(settings);
                settings.Children.Add(new MenuOptionModel() { Id = 6, Name = "Notificaciones", Key = "Notifications", Url = "/notifications", Icon = "fa-tasks", DisplayOrder = 6 });
                settings.Children.Add(new MenuOptionModel() { Id = 7, Name = "Ajustes", Key = "Settings", Url = "/systemsettings", Icon = "fa-cogs", DisplayOrder = 7 });
                settings.Children.Add(new MenuOptionModel() { Id = 8, Name = "Recursos", Key = "TextResources", Url = "/textresources", Icon = "fa-font", DisplayOrder = 8 });
                settings.Children.Add(new MenuOptionModel() { Id = 9, Name = "Notificaciones Correo", Key = "EmailNotifications", Url = "/emailnotifications", Icon = "fa-send", DisplayOrder = 9 });
                settings.Children.Add(new MenuOptionModel() { Id = 10, Name = "Log de errores", Key = "Logs", Url = "/logs", Icon = "fa-list", DisplayOrder = 10 });
            }

            options.Add(new MenuOptionModel() { Id = 3, Name = "Inicio", Key = "Dashboard", Url = "/", Icon = "fa-home", DisplayOrder = 0 });
            options.Add(new MenuOptionModel() { Id = 2, Name = "Habitaciones", Key = "Rooms", Url = "/rooms", Icon = "fa-hotel", DisplayOrder = 3 });
            options.Add(new MenuOptionModel() { Id = 11, Name = "Disponiblidad", Key = "Booking", Url = "/calendar", Icon = "fa-calendar", DisplayOrder = 1 });

            await Task.FromResult(0);

            return this.Ok(options.OrderBy(c => c.DisplayOrder));
        }
    }
}