//-----------------------------------------------------------------------
// <copyright file="HostelsController.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Controllers.Hostels
{
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Beto.Core.Web.Api.Filters;
    using Hostaliando.Business.Exceptions;
    using Hostaliando.Business.Security;
    using Hostaliando.Business.Services;
    using Hostaliando.Web.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Hostels Controller
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    [Route("api/v1/hostels")]
    public class HostelsController : BaseApiController
    {
        /// <summary>
        /// The hostel service
        /// </summary>
        private readonly IHostelService hostelService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="HostelsController"/> class.
        /// </summary>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        /// <param name="hostelService">The hostel service.</param>
        /// <param name="workContext">The work context.</param>
        public HostelsController(
            IMessageExceptionFinder messageExceptionFinder,
            IHostelService hostelService,
            IWorkContext workContext) : base(messageExceptionFinder)
        {
            this.hostelService = hostelService;
            this.workContext = workContext;
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the action</returns>
        [HttpPost]
        [RequiredModel]
        public async Task<IActionResult> Post([FromBody] HostelModel model)
        {
            var hostel = model.ToEntity();

            try
            {
                await this.hostelService.Insert(hostel);

                return this.Ok(new BaseModel { Id = hostel.Id });
            }
            catch (HostaliandoException e)
            {
                return this.BadRequest(e);
            }            
        }
    }
}