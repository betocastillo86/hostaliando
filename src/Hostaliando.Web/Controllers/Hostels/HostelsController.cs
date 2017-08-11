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
    using Hostaliando.Web.Infraestructure.Filters;
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
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the action</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var hostel = await this.hostelService.GetById(id);

            if (hostel != null)
            {
                return this.Ok(hostel.ToModel());
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
        /// <returns>the list</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] HostelFilterModel filter)
        {
            var hostels = await this.hostelService.GetAll(filter.Keyword, filter.LocationId, filter.Page, filter.PageSize);
            var models = hostels.ToModels();
            return this.Ok(models, hostels.HasNextPage, hostels.TotalCount);
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the action</returns>
        [HttpPost]
        [RequiredModel]
        [ServiceFilter(typeof(AuthorizeAdminAttribute))]
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

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the task</returns>
        [HttpDelete]
        [Route("{id:int}")]
        [ServiceFilter(typeof(AuthorizeAdminAttribute))]
        public async Task<IActionResult> Delete(int id)
        {
            var hostel = await this.hostelService.GetById(id);

            if (hostel != null)
            {
                await this.hostelService.Delete(hostel);
                return this.Ok();
            }
            else
            {
                return this.NotFound();
            }
        }

        /// <summary>
        /// Updates the specified hostel.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>the action</returns>
        [HttpPut]
        [Route("{id:int}")]
        [RequiredModel]
        [ServiceFilter(typeof(AuthorizeAdminAttribute))]
        public async Task<IActionResult> Put(int id, [FromBody] HostelModel model)
        {
            var hostel = await this.hostelService.GetById(id);

            if (hostel != null)
            {
                hostel.Name = model.Name;
                hostel.PhoneNumber = model.PhoneNumber;
                hostel.CurrencyId = model.Currency.Id;
                hostel.LocationId = model.Location.Id;
                hostel.Email = model.Email;

                try
                {
                    await this.hostelService.Update(hostel);
                    return this.Ok();
                }
                catch (HostaliandoException e)
                {
                    return this.BadRequest(e);
                }
            }
            else
            {
                return this.NotFound();
            }
        }
    }
}