//-----------------------------------------------------------------------
// <copyright file="LogCleanerController.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Controllers
{
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Hostaliando.Business.Services;
    using Hostaliando.Web.Infraestructure.Filters;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Log Cleaner Controller
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    [Route("api/v1/logs/clean")]
    public class LogCleanerController : BaseApiController
    {
        /// <summary>
        /// The log service
        /// </summary>
        private readonly ILogService logService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogCleanerController"/> class.
        /// </summary>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        /// <param name="logService">the log service</param>
        public LogCleanerController(
            IMessageExceptionFinder messageExceptionFinder,
            ILogService logService) : base(messageExceptionFinder)
        {
            this.logService = logService;
        }

        /// <summary>
        /// Posts this instance.
        /// </summary>
        /// <returns>the action</returns>
        [HttpPost]
        [ServiceFilter(typeof(AuthorizeAdminAttribute))]
        public async Task<IActionResult> Post()
        {
            await this.logService.Clear();

            return this.Ok();
        }
    }
}