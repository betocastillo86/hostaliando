//-----------------------------------------------------------------------
// <copyright file="HostelsController.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Controllers.Hostels
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Beto.Core.Web.Api.Filters;
    using Hostaliando.Business.Exceptions;
    using Hostaliando.Business.Security;
    using Hostaliando.Business.Services;
    using Hostaliando.Data;
    using Hostaliando.Web.Infraestructure.Filters;
    using Hostaliando.Web.Models;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.JsonPatch.Operations;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Hostels Controller
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    [Route("api/v1/hostels")]
    public class HostelsController : BaseApiController
    {
        /// <summary>
        /// The booking source service
        /// </summary>
        private readonly IBookingSourceService bookingSourceService;

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
        /// <param name="bookingSourceService">The booking source service.</param>
        public HostelsController(
            IMessageExceptionFinder messageExceptionFinder,
            IHostelService hostelService,
            IWorkContext workContext,
            IBookingSourceService bookingSourceService) : base(messageExceptionFinder)
        {
            this.hostelService = hostelService;
            this.workContext = workContext;
            this.bookingSourceService = bookingSourceService;
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
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the action</returns>
        [HttpGet]
        [Route("{id:int}", Name = "ApiGetHostel")]
        public async Task<IActionResult> Get(int id)
        {
            var hostel = await this.hostelService.GetById(id);

            if (hostel != null)
            {
                var model = hostel.ToModel();
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
        /// <returns>the list</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] HostelFilterModel filter)
        {
            var hostels = await this.hostelService.GetAll(filter.Keyword, filter.LocationId, filter.Page, filter.PageSize);
            var models = hostels.ToModels();
            return this.Ok(models, hostels.HasNextPage, hostels.TotalCount);
        }

        /// <summary>
        /// Patches the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="jsonDocument">The JSON document.</param>
        /// <returns>the action</returns>
        [HttpPatch]
        [Route("{id:int}")]
        [ServiceFilter(typeof(AuthorizeAdminAttribute))]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<HostelModel> jsonDocument)
        {
            var hostel = await this.hostelService.GetById(id);

            if (hostel == null)
            {
                return this.NotFound();
            }

            bool updateHostel = false;

            if (jsonDocument.Operations.Any(c => c.path.Equals("/sources")))
            {
                var sources = await this.bookingSourceService.GetHostelBookingSourceByHostelId(id);

                foreach (var source in jsonDocument.Operations.Where(c => c.path.Equals("/sources")))
                {
                    var sourceId = Convert.ToInt32(source.value);

                    if (source.OperationType == OperationType.Add && !sources.Any(c => c.SourceId == sourceId))
                    {
                        await this.bookingSourceService.InsertSourceToHostel(new HostelBookingSource { SourceId = sourceId, HostelId = id });
                    }
                    else if (source.OperationType == OperationType.Remove)
                    {
                        var sourceToDelete = sources.FirstOrDefault(c => c.SourceId == sourceId);
                        await this.bookingSourceService.DeleteSourceToHostel(sourceToDelete);
                    }
                }
            }

            try
            {
                if (updateHostel)
                {
                    await this.hostelService.Update(hostel);
                }

                return this.Ok();
            }
            catch (HostaliandoException e)
            {
                return this.BadRequest(e);
            }
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
            if (model.Sources == null || model.Sources.Count == 0)
            {
                this.ModelState.AddModelError("Sources", "Al menos debe ingresar un origen de datos");
                return this.BadRequest(this.ModelState);
            }

            var hostel = model.ToEntity();

            try
            {
                await this.hostelService.Insert(hostel);

                return this.Created("ApiGetHostel", hostel.Id);
            }
            catch (HostaliandoException e)
            {
                return this.BadRequest(e);
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