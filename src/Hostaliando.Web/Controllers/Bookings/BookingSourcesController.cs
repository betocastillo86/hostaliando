//-----------------------------------------------------------------------
// <copyright file="BookingSourcesController.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Controllers.Bookings
{
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Hostaliando.Business.Services;
    using Hostaliando.Web.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Booking Sources Controller
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    [Route("api/v1/bookingsources")]
    public class BookingSourcesController : BaseApiController
    {
        /// <summary>
        /// The booking source service
        /// </summary>
        private readonly IBookingSourceService bookingSourceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingSourcesController"/> class.
        /// </summary>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        /// <param name="bookingSourceService">The booking source service.</param>
        public BookingSourcesController(
            IMessageExceptionFinder messageExceptionFinder,
            IBookingSourceService bookingSourceService) : base(messageExceptionFinder)
        {
            this.bookingSourceService = bookingSourceService;
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>the action</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BookingSourceFilterModel filter)
        {
            var sources = await this.bookingSourceService.GetAll(filter.Keyword);

            var models = sources.Select(c => new BaseNamedModel() { Id = c.Id, Name = c.Name });

            return this.Ok(models);
        }
    }
}