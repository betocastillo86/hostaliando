//-----------------------------------------------------------------------
// <copyright file="BookingSourceService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    using System.Collections.Generic;
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
        /// The publisher
        /// </summary>
        private readonly IPublisher publisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingSourceService"/> class.
        /// </summary>
        /// <param name="bookingSourceRepository">The booking source repository.</param>
        /// <param name="hostelBookingSourceRepository">The hostel booking source repository.</param>
        public BookingSourceService(
            IRepository<BookingSource> bookingSourceRepository,
            IRepository<HostelBookingSource> hostelBookingSourceRepository,
            IPublisher publisher)
        {
            this.bookingSourceRepository = bookingSourceRepository;
            this.hostelBookingSourceRepository = hostelBookingSourceRepository;
            this.publisher = publisher;
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
        /// Gets the hostel booking source by hostel identifier.
        /// </summary>
        /// <param name="hostelId">The hostel identifier.</param>
        /// <returns>the list</returns>
        public async Task<IList<HostelBookingSource>> GetHostelBookingSourceByHostelId(int hostelId)
        {
            return await this.hostelBookingSourceRepository.Table
                .Include(c => c.Source)
                .Where(c => c.HostelId == hostelId)
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
            try
            {
                await this.hostelBookingSourceRepository.InsertAsync(hostelBookingSource);

                await this.publisher.EntityInserted(hostelBookingSource);
            }
            catch (DbUpdateException e)
            {
                var inner = (SqlException)e.InnerException;

                if (inner.Number == 547)
                {
                    var target = "Unknown";

                    if (inner.Message.IndexOf("FK_HostelBookingSources_BookingSources") != -1)
                    {
                        target = "Source";
                    }

                    throw new HostaliandoException(target, HostaliandoExceptionCode.InvalidForeignKey);
                }
            }
        }
    }
}