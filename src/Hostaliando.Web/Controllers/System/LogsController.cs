//-----------------------------------------------------------------------
// <copyright file="LogsController.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Controllers
{
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Beto.Core.Web.Api.Filters;
    using Hostaliando.Business.Extensions;
    using Hostaliando.Business.Services;
    using Hostaliando.Web.Infraestructure.Filters;
    using Hostaliando.Web.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Logs Controller
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    [Route("api/v1/logs")]
    public class LogsController : BaseApiController
    {
        /// <summary>
        /// The log service
        /// </summary>
        private readonly ILogService logService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogsController"/> class.
        /// </summary>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        /// <param name="logService">The log service.</param>
        public LogsController(
            IMessageExceptionFinder messageExceptionFinder,
            ILogService logService) : base(messageExceptionFinder)
        {
            this.logService = logService;
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>the action</returns>
        [HttpGet]
        [ServiceFilter(typeof(AuthorizeAdminAttribute))]
        public async Task<IActionResult> Get([FromQuery] LogFilterModel filter)
        {
            var logs = await this.logService.GetAll(filter.Keyword, filter.Page, filter.PageSize);

            var models = logs.ToModels();

            return this.Ok(models, logs.HasNextPage, logs.TotalCount);
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the action</returns>
        [HttpPost]
        [RequiredModel]
        public async Task<IActionResult> Post([FromBody] LogModel model)
        {
            var service = (ILoggerService)this.logService;
            await service.Error(model.ShortMessage, model.FullMessage);
            return this.Ok(new { result = true });
        }
    }
}