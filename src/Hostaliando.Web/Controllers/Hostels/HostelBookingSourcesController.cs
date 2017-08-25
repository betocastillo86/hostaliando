//-----------------------------------------------------------------------
// <copyright file="HostelBookingSourcesController.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Controllers.Hostels
{
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Hostaliando.Business.Security;
    using Hostaliando.Business.Services;
    using Hostaliando.Web.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Hostel Booking Sources Controller
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    [Route("api/v1/hostels/{id:int}/sources")]
    public class HostelBookingSourcesController : BaseApiController
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
        /// Initializes a new instance of the <see cref="HostelBookingSourcesController"/> class.
        /// </summary>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        /// <param name="hostelService">The hostel service.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="bookingSourceService">The booking source service.</param>
        public HostelBookingSourcesController(
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
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the task</returns>
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var hostel = await this.hostelService.GetById(id);

            if (hostel == null)
            {
                return this.NotFound();
            }
            else if (!this.workContext.CurrentUser.IsAdmin() && this.workContext.CurrentUserId != id)
            {
                return this.Forbid();
            }

            var sources = await this.bookingSourceService.GetByHostelId(id);

            var models = sources.ToModels();

            return this.Ok(models);
        }
    }
}