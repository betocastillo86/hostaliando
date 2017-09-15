//-----------------------------------------------------------------------
// <copyright file="BookingsController.cs" company="Gabriel Castillo">
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
    using Hostaliando.Business.Extensions;
    using Hostaliando.Business.Security;
    using Hostaliando.Business.Services;
    using Hostaliando.Data;
    using Hostaliando.Web.Models;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.JsonPatch.Exceptions;
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
        /// The logger service
        /// </summary>
        private readonly ILoggerService loggerService;

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
        /// <param name="loggerService">The logger service.</param>
        public BookingsController(
            IMessageExceptionFinder messageExceptionFinder,
            IWorkContext workContext,
            IRoomService roomService,
            IBookingService bookingService,
            ILoggerService loggerService) : base(messageExceptionFinder)
        {
            this.workContext = workContext;
            this.roomService = roomService;
            this.bookingService = bookingService;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the action</returns>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
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

            await this.bookingService.Delete(booking);

            return this.Ok();
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
                filter.NotStatus,
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
        [Route("{id:int}", Name = "ApiGetBooking")]
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
        /// Patches the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="patchDocument">The patch document.</param>
        /// <returns>the action</returns>
        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<BookingModel> patchDocument)
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

            var model = new BookingModel() { Room = new RoomModel() { } };

            try
            {
                patchDocument.ApplyTo(model);
            }
            catch (JsonPatchException e)
            {
                await this.loggerService.Error(e, this.workContext.CurrentUser);
                return this.BadRequest(HostaliandoExceptionCode.BadArgument, "Argumento invalido");
            }

            bool validateAvailability = false;

            var room = await this.roomService.GetById(model.Room.Id > 0 ? model.Room.Id : booking.RoomId, false);

            if (room == null)
            {
                return this.BadRequest(new HostaliandoException("Room", HostaliandoExceptionCode.InvalidForeignKey));
            }
            else if (room.HostelId != this.workContext.CurrentUser.HostelId)
            {
                return this.BadRequest(new HostaliandoException("Room", HostaliandoExceptionCode.RoomIsOfAnotherHostel));
            }

            this.SetPatchFields(patchDocument, model, booking, out validateAvailability);

            if (booking.FromDate > booking.ToDate)
            {
                return this.BadRequest(new HostaliandoException("FromDate", HostaliandoExceptionCode.BadArgument));
            }

            if (validateAvailability && await this.IsBusy(room, booking.FromDate, booking.ToDate, booking))
            {
                return this.BadRequest(new HostaliandoException(HostaliandoExceptionCode.ExceededRoomCapacity));
            }

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

            if (await this.IsBusy(room, model.FromDate.Value, model.ToDate.Value))
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

                return this.Created("ApiGetBooking", booking.Id);
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
            else if (room.HostelId != this.workContext.CurrentUser.HostelId)
            {
                return this.BadRequest(new HostaliandoException("Room", HostaliandoExceptionCode.RoomIsOfAnotherHostel));
            }

            if (await this.IsBusy(room, booking.FromDate, booking.ToDate, booking))
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

        /// <summary>
        /// Determines whether the specified room is busy.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="from">From date.</param>
        /// <param name="to">To date.</param>
        /// <param name="currentBooking">The current booking.</param>
        /// <returns>if the room is busy</returns>
        private async Task<bool> IsBusy(Room room, DateTime from, DateTime to, Booking currentBooking = null)
        {
            var bookingsOnDate = await this.bookingService.GetAll(
                roomId: room.Id,
                fromDate: from,
                toDate: to,
                status: BookingStatus.Booked,
                excludeBookings: currentBooking?.Id > 0 ? new int[] { currentBooking.Id } : null);

            if (bookingsOnDate.Count > 0)
            {
                if (room.IsPrivated)
                {
                    return true;
                }
                else
                {
                    if (bookingsOnDate.Count >= room.Beds)
                    {
                        //// when the room is not private, has to validate if the bed would be empty in
                        //// the days of the reservation
                        var nights = (to - from).TotalDays + 1;

                        var countBeds = 0;

                        for (int i = 0; i < nights; i++)
                        {
                            var day = from.AddDays(i);

                            countBeds = bookingsOnDate.Count(c => c.FromDate <= day && c.ToDate >= day);

                            if (countBeds == room.Beds)
                            {
                                break;
                            }
                        }

                        return countBeds == room.Beds;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sets the patch fields.
        /// </summary>
        /// <param name="patchDocument">The patch document.</param>
        /// <param name="model">The model.</param>
        /// <param name="booking">The booking.</param>
        /// <param name="validateAvailability">if set to <c>true</c> [validate availability].</param>
        private void SetPatchFields(JsonPatchDocument<BookingModel> patchDocument, BookingModel model, Booking booking, out bool validateAvailability)
        {
            validateAvailability = false;

            if (patchDocument.Operations.Any(c => c.path.Equals("/room/id") && c.OperationType == Microsoft.AspNetCore.JsonPatch.Operations.OperationType.Replace))
            {
                booking.RoomId = model.Room.Id;
                validateAvailability = true;
            }

            if (patchDocument.Operations.Any(c => c.path.Equals("/fromDate") && c.OperationType == Microsoft.AspNetCore.JsonPatch.Operations.OperationType.Replace))
            {
                booking.FromDate = model.FromDate.Value;
                validateAvailability = true;
            }

            if (patchDocument.Operations.Any(c => c.path.Equals("/toDate") && c.OperationType == Microsoft.AspNetCore.JsonPatch.Operations.OperationType.Replace))
            {
                booking.ToDate = model.ToDate.Value;
                validateAvailability = true;
            }

            if (patchDocument.Operations.Any(c => c.path.Equals("/comments") && c.OperationType == Microsoft.AspNetCore.JsonPatch.Operations.OperationType.Replace))
            {
                booking.Comments = model.Comments;
            }

            if (patchDocument.Operations.Any(c => c.path.Equals("/totalPrice") && c.OperationType == Microsoft.AspNetCore.JsonPatch.Operations.OperationType.Replace))
            {
                booking.TotalPrice = model.TotalPrice.Value;
            }

            if (patchDocument.Operations.Any(c => c.path.Equals("/status") && c.OperationType == Microsoft.AspNetCore.JsonPatch.Operations.OperationType.Replace))
            {
                booking.Status = model.Status;
            }
        }
    }
}