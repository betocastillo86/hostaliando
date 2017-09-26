//-----------------------------------------------------------------------
// <copyright file="AvailabilityController.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Controllers.Bookings
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Filters;
    using Hostaliando.Business.Security;
    using Hostaliando.Business.Services;
    using Hostaliando.Data;
    using Hostaliando.Web.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Availability Controller
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    [Route("api/v1/availability")]
    public class AvailabilityController : BaseBookingsController
    {
        /// <summary>
        /// The room service
        /// </summary>
        private readonly IRoomService roomService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// The hostel service
        /// </summary>
        private readonly IHostelService hostelService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AvailabilityController"/> class.
        /// </summary>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        /// <param name="bookingService">The booking service.</param>
        /// <param name="roomService">The room service.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="hostelService">The hostel service.</param>
        public AvailabilityController(
            IMessageExceptionFinder messageExceptionFinder,
            IBookingService bookingService,
            IRoomService roomService,
            IWorkContext workContext,
            IHostelService hostelService) : base(messageExceptionFinder, bookingService)
        {
            this.roomService = roomService;
            this.workContext = workContext;
            this.hostelService = hostelService;
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>the action</returns>
        [HttpGet]
        [RequiredModel(ArgumentName = "filter")]
        public async Task<IActionResult> Get([FromQuery] AvailabilityFilterModel filter)
        {
            var hostel = await this.hostelService.GetById(filter.HostelId.Value);

            if (!this.workContext.CurrentUser.IsAdmin() && hostel.Id != this.workContext.CurrentUser.HostelId)
            {
                return this.Forbid();
            }

            IList<Room> rooms = null;

            if (filter.RoomId.HasValue)
            {
                var room = await this.roomService.GetById(filter.RoomId.Value);

                if (room.HostelId == hostel.Id)
                {
                    rooms = new List<Room> { room };
                }
                else
                {
                    this.ModelState.AddModelError("RoomId", "La habitación no pertenece al hostal");
                    return this.BadRequest(this.ModelState);
                }
            }
            else
            {
                rooms = await this.roomService.GetAll(hostelId: hostel.Id, onlyPrivated: filter.OnlyPrivated);
            }

            var bookings = await this.BookingService.GetAll(
                rooms: rooms.Select(c => c.Id).ToArray(),
                notStatus: BookingStatus.Canceled,
                fromDate: filter.FromDate,
                toDate: filter.ToDate);

            var models = new List<AvailabilityModel>();

            foreach (var room in rooms)
            {
                var roomBookings = bookings.Where(c => c.RoomId == room.Id).ToList();

                if (!this.IsBusy(roomBookings, room, filter.FromDate.Value, filter.ToDate.Value, filter.People.Value - 1))
                {
                    models.Add(new AvailabilityModel
                    {
                        Room = room.ToModel(),
                        DateFrom = filter.FromDate.Value
                    });
                }
            }

            return this.Ok(models, false, models.Count);
        }
    }
}