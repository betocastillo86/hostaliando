//-----------------------------------------------------------------------
// <copyright file="BookingSourceService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Hostaliando.Data;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Booking Service
    /// </summary>
    /// <seealso cref="Hostaliando.Business.Services.IBookingSourceService" />
    public class BookingSourceService : IBookingSourceService
    {
        /// <summary>
        /// The booking source repository
        /// </summary>
        private readonly IRepository<BookingSource> bookingSourceRepository;

        /// <summary>
        /// The hostel booking source repository
        /// </summary>
        private readonly IRepository<HostelBookingSource> hostelBookingSourceRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingSourceService"/> class.
        /// </summary>
        /// <param name="bookingSourceRepository">The booking source repository.</param>
        /// <param name="hostelBookingSourceRepository">The hostel booking source repository.</param>
        public BookingSourceService(
            IRepository<BookingSource> bookingSourceRepository,
            IRepository<HostelBookingSource> hostelBookingSourceRepository)
        {
            this.bookingSourceRepository = bookingSourceRepository;
            this.hostelBookingSourceRepository = hostelBookingSourceRepository;
        }

        /// <summary>
        /// Deletes the source to hostel.
        /// </summary>
        /// <param name="hostelBookingSource">The hostel booking source.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task DeleteSourceToHostel(HostelBookingSource hostelBookingSource)
        {
            await this.hostelBookingSourceRepository.DeleteAsync(hostelBookingSource);
        }

        /// <summary>
        /// Gets all booking sources
        /// </summary>
        /// <returns>
        /// the list
        /// </returns>
        public async Task<IList<BookingSource>> GetAll()
        {
            return await this.bookingSourceRepository.Table.ToListAsync();
        }

        /// <summary>
        /// Gets the booking sources by hostel identifier.
        /// </summary>
        /// <param name="hostelId">The hostel identifier.</param>
        /// <returns>
        /// the list
        /// </returns>
        public async Task<IList<BookingSource>> GetByHostelId(int hostelId)
        {
            return await this.hostelBookingSourceRepository.Table
                .Include(c => c.Source)
                .Where(c => c.HostelId == hostelId)
                .Select(c => c.Source)
                .ToListAsync();
        }

        /// <summary>
        /// Inserts the source to hostel.
        /// </summary>
        /// <param name="hostelBookingSource">The hostel booking source.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task InsertSourceToHostel(HostelBookingSource hostelBookingSource)
        {
            await this.hostelBookingSourceRepository.InsertAsync(hostelBookingSource);
        }
    }
}