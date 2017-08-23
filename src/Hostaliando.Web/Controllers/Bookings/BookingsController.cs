//-----------------------------------------------------------------------
// <copyright file="BookingsController.cs" company="Gabriel Castillo">
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
    using Hostaliando.Data;
    using Hostaliando.Web.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Bookings Controller
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    [Route("api/v1/bookings")]
    public class BookingsController : BaseApiController
    {
        /// <summary>
        /// The booking service
        /// </summary>
        private readonly IBookingService bookingService;

        /// <summary>
        /// The room service
        /// </summary>
        private readonly IRoomService roomService;

        /// <summary>
        /// The work context
        /// </summary>
        private readonly IWorkContext workContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingsController"/> class.
        /// </summary>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="roomService">The room service.</param>
        /// <param name="bookingService">The booking service.</param>
        public BookingsController(
            IMessageExceptionFinder messageExceptionFinder,
            IWorkContext workContext,
            IRoomService roomService,
            IBookingService bookingService) : base(messageExceptionFinder)
        {
            this.workContext = workContext;
            this.roomService = roomService;
            this.bookingService = bookingService;
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>the action</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BookingFilterModel filter)
        {
            if (!this.workContext.CurrentUser.IsAdmin())
            {
                var room = filter.RoomId.HasValue ? await this.roomService.GetById(filter.RoomId.Value) : null;

                // if the user is not admin has to provide hostel or room
                if (!filter.HostelId.HasValue && !filter.RoomId.HasValue)
                {
                    this.ModelState.AddModelError("HostelId", "El campo hostelId o roomId es obligatorio");
                    this.ModelState.AddModelError("RoomId", "El campo hostelId o roomId es obligatorio");
                    return this.BadRequest(this.ModelState);
                }
                else if (this.workContext.CurrentUser.HostelId != filter.HostelId && this.workContext.CurrentUser.HostelId != room?.HostelId)
                {
                    return this.Forbid();
                }
            }

            var bookings = await this.bookingService.GetAll(
                filter.HostelId,
                filter.RoomId,
                filter.FromDate,
                filter.ToDate,
                filter.Status,
                filter.Keyword,
                sortBy: filter.OrderByEnum,
                page: filter.Page,
                pageSize: filter.PageSize);

            var models = bookings.ToModels();

            return this.Ok(models, bookings.HasNextPage, bookings.TotalCount);
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the booking</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var booking = await this.bookingService.GetByIdAsync(id, false);

            if (booking == null)
            {
                return this.NotFound();
            }
            else if (!this.workContext.CurrentUser.IsAdmin() && booking.Room.HostelId != this.workContext.CurrentUser.HostelId)
            {
                return this.Forbid();
            }

            var model = booking.ToModel();
            return this.Ok(model);
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>the action</returns>
        [HttpPost]
        [RequiredModel]
        public async Task<IActionResult> Post([FromBody] BookingModel model)
        {
            var room = await this.roomService.GetById(model.Room.Id);

            if (room == null)
            {
                return this.BadRequest(new HostaliandoException("Room", HostaliandoExceptionCode.InvalidForeignKey));
            }

            if (!this.workContext.CurrentUser.IsAdmin() && room.HostelId != this.workContext.CurrentUser.HostelId)
            {
                return this.Forbid();
            }

            var bookingsOnDate = await this.bookingService.GetAll(
                roomId: model.Room.Id,
                fromDate: model.FromDate,
                toDate: model.ToDate,
                status: BookingStatus.Booked);

            if (bookingsOnDate.TotalCount == room.Beds || (bookingsOnDate.Count > 0 && room.IsPrivated))
            {
                return this.BadRequest(new HostaliandoException(HostaliandoExceptionCode.ExceededRoomCapacity));
            }

            var booking = new Booking
            {
                RoomId = model.Room.Id,
                Comments = model.Comments,
                FromDate = model.FromDate.Value,
                ToDate = model.ToDate.Value,
                UserId = this.workContext.CurrentUserId,
                SourceId = model.Source.Id,
                Status = BookingStatus.Booked,
                TotalPrice = model.TotalPrice.Value,
                GuestName = model.GuestName,
                GuestEmail = model.GuestEmail,
                GuestLocationId = model.GuestLocation?.Id
            };

            try
            {
                await this.bookingService.Insert(booking);

                return this.Ok(new BaseModel { Id = booking.Id });
            }
            catch (HostaliandoException e)
            {
                return this.BadRequest(e);
            }
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
        public async Task<IActionResult> Put(int id, [FromBody] BookingModel model)
        {
            var booking = await this.bookingService.GetByIdAsync(id);

            if (booking == null)
            {
                return this.NotFound();
            }
            else if (!this.workContext.CurrentUser.IsAdmin() && booking.Room.HostelId != this.workContext.CurrentUser.HostelId)
            {
                return this.Forbid();
            }

            var room = await this.roomService.GetById(model.Room.Id, false);

            if (room == null)
            {
                return this.BadRequest(new HostaliandoException("Room", HostaliandoExceptionCode.InvalidForeignKey));
            }

            var bookingsOnDate = await this.bookingService.GetAll(
                roomId: model.Room.Id,
                fromDate: model.FromDate,
                toDate: model.ToDate,
                status: BookingStatus.Booked,
                excludeBookings: new int[] { id });

            if (bookingsOnDate.TotalCount == booking.Room.Beds || (bookingsOnDate.Count > 0 && room.IsPrivated))
            {
                return this.BadRequest(new HostaliandoException(HostaliandoExceptionCode.ExceededRoomCapacity));
            }

            booking.Comments = model.Comments;
            booking.FromDate = model.FromDate.Value;
            booking.ToDate = model.ToDate.Value;
            booking.TotalPrice = model.TotalPrice.Value;
            booking.GuestEmail = model.GuestEmail;
            booking.GuestLocationId = model.GuestLocation?.Id;
            booking.GuestName = model.GuestName;
            booking.RoomId = model.Room.Id;
            booking.SourceId = model.Source.Id;
            booking.Status = model.Status;

            try
            {
                await this.bookingService.Update(booking);

                return this.Ok();
            }
            catch (HostaliandoException e)
            {
                return this.BadRequest(e);
            }
        }
    }
}