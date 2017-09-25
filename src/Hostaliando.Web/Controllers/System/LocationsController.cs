//-----------------------------------------------------------------------
// <copyright file="LocationsController.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Controllers
{
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Hostaliando.Business.Services;
    using Hostaliando.Web.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Locations Controller
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    [Route("api/v1/locations")]
    public class LocationsController : BaseApiController
    {
        /// <summary>
        /// The location service
        /// </summary>
        private readonly ILocationService locationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationsController"/> class.
        /// </summary>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        /// <param name="locationService">The location service.</param>
        public LocationsController(
            IMessageExceptionFinder messageExceptionFinder,
            ILocationService locationService) : base(messageExceptionFinder)
        {
            this.locationService = locationService;
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>the task</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]LocationFilterModel filter)
        {
            var locations = await this.locationService.GetAll(
                   filter.Name,
                   filter.ParentId,
                   filter.OnlyParents,
                   filter.Page,
                   filter.PageSize);

            var models = locations.ToModels();

            return this.Ok(models, locations.HasNextPage, locations.TotalCount);
        }
    }
}