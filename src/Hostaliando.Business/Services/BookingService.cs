//-----------------------------------------------------------------------
// <copyright file="BookingService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Beto.Core.EventPublisher;
    using Hostaliando.Business.Exceptions;
    using Hostaliando.Data;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Booking Service
    /// </summary>
    /// <seealso cref="Hostaliando.Business.Services.IBookingService" />
    public class BookingService : IBookingService
    {
        /// <summary>
        /// The booking repository
        /// </summary>
        private readonly IRepository<Booking> bookingRepository;

        /// <summary>
        /// The publisher
        /// </summary>
        private readonly IPublisher publisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingService"/> class.
        /// </summary>
        /// <param name="bookingRepository">The booking repository.</param>
        /// <param name="publisher">The publisher.</param>
        public BookingService(
            IRepository<Booking> bookingRepository,
            IPublisher publisher)
        {
            this.bookingRepository = bookingRepository;
            this.publisher = publisher;
        }

        /// <summary>
        /// Gets all the reservations
        /// </summary>
        /// <param name="hostelId">The hostel identifier.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="fromDate">The date from.</param>
        /// <param name="toDate">The date to.</param>
        /// <param name="status">the booking status</param>
        /// <param name="keyword">The keyword.</param>
        /// <param name="excludeBookings">excludes these bookings identifiers in the search</param>
        /// <param name="sortBy">sort the bookings by</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// the list of bookings
        /// </returns>
        public async Task<IPagedList<Booking>> GetAll(
            int? hostelId = null,
            int? roomId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            BookingStatus? status = null,
            string keyword = null,
            int[] excludeBookings = null,
            SortBookingBy sortBy = SortBookingBy.Recent,
            int page = 0,
            int pageSize = int.MaxValue)
        {
            var query = this.bookingRepository.Table.Where(c => !c.Deleted);

            if (hostelId.HasValue)
            {
                query = query.Where(c => c.Room.HostelId == hostelId.Value);
            }

            if (roomId.HasValue)
            {
                query = query.Where(c => c.RoomId == roomId.Value);
            }

            if (fromDate.HasValue)
            {
                query = query.Where(c => c.FromDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(c => c.ToDate <= toDate.Value);
            }

            if (status.HasValue)
            {
                var statusId = Convert.ToInt16(status);
                query = query.Where(c => c.StatusId == statusId);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => c.GuestName.Contains(keyword) || c.GuestEmail.Contains(keyword) || c.Comments.Contains(keyword));
            }

            if (excludeBookings != null && excludeBookings.Length > 0)
            {
                query = query.Where(c => !excludeBookings.Contains(c.Id));
            }

            switch (sortBy)
            {
                case SortBookingBy.Recent:
                default:
                    query = query.OrderByDescending(c => c.CreationDateUtc);
                    break;

                case SortBookingBy.TotalPrice:
                    query = query.OrderByDescending(c => c.TotalPrice);
                    break;

                case SortBookingBy.Room:
                    query = query.OrderBy(c => c.RoomId);
                    break;
            }

            return await new PagedList<Booking>().Async(query, page, pageSize);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeHostel">includes the hostel in the query</param>
        /// <returns>
        /// the booking
        /// </returns>
        public async Task<Booking> GetByIdAsync(int id, bool includeHostel = false)
        {
            var query = this.bookingRepository.Table
                .Include(c => c.Room)
                .AsQueryable();

            if (includeHostel)
            {
                query = query.Include(c => c.Room.Hostel).AsQueryable();
            }

            return await query.FirstOrDefaultAsync(c => c.Id == id && !c.Deleted);
        }

        /// <summary>
        /// Inserts the specified booking.
        /// </summary>
        /// <param name="booking">The booking.</param>
        /// <returns>
        /// inserts a booking
        /// </returns>
        public async Task Insert(Booking booking)
        {
            try
            {
                booking.CreationDateUtc = DateTime.UtcNow;

                await this.bookingRepository.InsertAsync(booking);
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException is SqlException)
                {
                    var inner = (SqlException)e.InnerException;
                    if (inner?.Number == 547)
                    {
                        this.Throw547Exception(inner);
                    }
                    else
                    {
                        throw;
                    }
                }
                else
                {
                    throw;
                }
            }

            await this.publisher.EntityInserted(booking);
        }

        /// <summary>
        /// Updates the specified booking.
        /// </summary>
        /// <param name="booking">The booking.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task Update(Booking booking)
        {
            try
            {
                await this.bookingRepository.UpdateAsync(booking);
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException is SqlException)
                {
                    var inner = (SqlException)e.InnerException;
                    if (inner?.Number == 547)
                    {
                        this.Throw547Exception(inner);
                    }
                    else
                    {
                        throw;
                    }
                }
                else
                {
                    throw;
                }
            }

            await this.publisher.EntityUpdated(booking);
        }

        /// <summary>
        /// Throw a foreign key exception
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <exception cref="Hostaliando.Business.Exceptions.HostaliandoException">the target</exception>
        private void Throw547Exception(SqlException ex)
        {
            var target = "Unknown";

            if (ex.Message.IndexOf("FK_Bookings_Rooms") != -1)
            {
                target = "Room";
            }
            else if (ex.Message.IndexOf("FK_Bookings_Users") != -1)
            {
                target = "User";
            }
            else if (ex.Message.IndexOf("FK_Bookings_BookingSources") != -1)
            {
                target = "BookingSource";
            }
            else if (ex.Message.IndexOf("FK_Bookings_Locations") != -1)
            {
                target = "Location";
            }

            throw new HostaliandoException(target, HostaliandoExceptionCode.InvalidForeignKey);
        }
    }
}