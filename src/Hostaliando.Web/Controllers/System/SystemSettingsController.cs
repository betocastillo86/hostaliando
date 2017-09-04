//-----------------------------------------------------------------------
// <copyright file="SystemSettingsController.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Controllers
{
    using System.Threading.Tasks;
    using Beto.Core.Data.Configuration;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Beto.Core.Web.Api.Filters;
    using Hostaliando.Business.Security;
    using Hostaliando.Data;
    using Hostaliando.Web.Infraestructure.Filters;
    using Hostaliando.Web.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// System Settings Controller
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    [Route("api/v1/systemsettings")]
    public class SystemSettingsController : BaseApiController
    {
        /// <summary>
        /// The core setting service
        /// </summary>
        private readonly ICoreSettingService systemSettingService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemSettingsController"/> class.
        /// </summary>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="systemSettingService">The system setting service.</param>
        public SystemSettingsController(
            IMessageExceptionFinder messageExceptionFinder,
            IWorkContext workContext,
            ICoreSettingService systemSettingService) : base(messageExceptionFinder)
        {
            this.workContext = workContext;
            this.systemSettingService = systemSettingService;
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>the task</returns>
        [HttpGet]
        [ServiceFilter(typeof(AuthorizeAdminAttribute))]
        public async Task<IActionResult> Get([FromQuery] SystemSettingFilterModel filter)
        {
            var settings = await this.systemSettingService
                                        .GetAsync<SystemSetting>(filter.Keyword, null, filter.Page, filter.PageSize);

            var models = settings.ToModels();
            return this.Ok(models, settings.HasNextPage, settings.TotalCount);
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>the task</returns>
        [HttpPut]
        [RequiredModel]
        [Route("{id:int}")]
        [ServiceFilter(typeof(AuthorizeAdminAttribute))]
        public async Task<IActionResult> Put(int id, [FromBody] SystemSettingModel model)
        {
            var setting = this.systemSettingService.GetByKey<SystemSetting>(model.Name);

            if (setting != null && setting.Id == id)
            {
                setting.Value = model.Value;
                await this.systemSettingService.Update(setting);

                return this.Ok(new { result = true });
            }
            else
            {
                return this.NotFound();
            }
        }
    }
}