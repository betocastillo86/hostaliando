//-----------------------------------------------------------------------
// <copyright file="BaseBookingsController.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Beto.Core.Web.Api.Controllers;
    using Hostaliando.Business.Services;
    using Hostaliando.Data;

    /// <summary>
    /// Base Booking Controller
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.Controllers.BaseApiController" />
    public class BaseBookingsController : BaseApiController
    {
        /// <summary>
        /// The booking service
        /// </summary>
        protected readonly IBookingService BookingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBookingsController"/> class.
        /// </summary>
        /// <param name="messageExceptionFinder">The message exception finder.</param>
        /// <param name="bookingService">The booking service.</param>
        public BaseBookingsController(
            IMessageExceptionFinder messageExceptionFinder,
            IBookingService bookingService) : base(messageExceptionFinder)
        {
            this.BookingService = bookingService;
        }

        /// <summary>
        /// Determines whether the specified room is busy.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="from">From date.</param>
        /// <param name="to">To date.</param>
        /// <param name="currentBooking">The current booking.</param>
        /// <returns>if the room is busy</returns>
        protected async Task<bool> IsBusy(Room room, DateTime from, DateTime to, Booking currentBooking = null)
        {
            var bookingsOnDate = await this.BookingService.GetAll(
                rooms: new int[] { room.Id },
                fromDate: from,
                toDate: to,
                status: BookingStatus.Booked,
                excludeBookings: currentBooking?.Id > 0 ? new int[] { currentBooking.Id } : null);

            return this.IsBusy(bookingsOnDate, room, from, to);
        }

        /// <summary>
        /// Determines whether the specified bookings on date is busy.
        /// </summary>
        /// <param name="bookingsOnDate">The bookings on date.</param>
        /// <param name="room">The room.</param>
        /// <param name="from">From date.</param>
        /// <param name="to">To date.</param>
        /// <param name="additionalPeople">The additional people.</param>
        /// <returns>
        ///   <c>true</c> if the specified bookings on date is busy; otherwise, <c>false</c>.
        /// </returns>
        protected bool IsBusy(IList<Booking> bookingsOnDate, Room room, DateTime from, DateTime to, int additionalPeople = 0)
        {
            if (bookingsOnDate.Count > 0)
            {
                if (room.IsPrivated)
                {
                    return true;
                }
                else
                {
                    if (bookingsOnDate.Count + additionalPeople >= room.Beds)
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

                        return countBeds + additionalPeople == room.Beds;
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
    }
}