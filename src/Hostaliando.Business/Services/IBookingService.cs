//-----------------------------------------------------------------------
// <copyright file="IBookingService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    using System;
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Hostaliando.Data;

    /// <summary>
    /// Interface of booking service
    /// </summary>
    public interface IBookingService
    {
        /// <summary>
        /// Deletes the specified booking.
        /// </summary>
        /// <param name="booking">The booking.</param>
        /// <returns>the Task</returns>
        Task Delete(Booking booking);

        /// <summary>
        /// Gets all the reservations
        /// </summary>
        /// <param name="hostelId">The hostel identifier.</param>
        /// <param name="rooms">The rooms identifiers.</param>
        /// <param name="fromDate">The date from.</param>
        /// <param name="toDate">The date to.</param>
        /// <param name="status">the booking status</param>
        /// <param name="notStatus">different to this status</param>
        /// <param name="keyword">The keyword.</param>
        /// <param name="excludeBookings">excludes these bookings identifiers in the search</param>
        /// <param name="sortBy">sort the bookings by</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>the list of bookings</returns>
        Task<IPagedList<Booking>> GetAll(
            int? hostelId = null,
            int[] rooms = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            BookingStatus? status = null,
            BookingStatus? notStatus = null,
            string keyword = null,
            int[] excludeBookings = null,
            SortBookingBy sortBy = SortBookingBy.Recent,
            int page = 0,
            int pageSize = int.MaxValue);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeHostel">includes the hostel in the query</param>
        /// <returns>the booking</returns>
        Task<Booking> GetByIdAsync(int id, bool includeHostel = false);

        /// <summary>
        /// Inserts the specified booking.
        /// </summary>
        /// <param name="booking">The booking.</param>
        /// <returns>inserts a booking</returns>
        Task Insert(Booking booking);

        /// <summary>
        /// Updates the specified booking.
        /// </summary>
        /// <param name="booking">The booking.</param>
        /// <returns>the task</returns>
        Task Update(Booking booking);
    }
}