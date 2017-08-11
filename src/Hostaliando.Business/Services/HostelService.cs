//-----------------------------------------------------------------------
// <copyright file="HostelService.cs" company="Gabriel Castillo">
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
    /// Hostel Service
    /// </summary>
    /// <seealso cref="Hostaliando.Business.Services.IHostelService" />
    public class HostelService : IHostelService
    {
        /// <summary>
        /// The hostel repository
        /// </summary>
        private readonly IRepository<Hostel> hostelRepository;

        /// <summary>
        /// The publisher
        /// </summary>
        private readonly IPublisher publisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="HostelService"/> class.
        /// </summary>
        /// <param name="hostelRepository">The hostel repository.</param>
        /// <param name="publisher">The publisher.</param>
        public HostelService(
            IRepository<Hostel> hostelRepository,
            IPublisher publisher)
        {
            this.hostelRepository = hostelRepository;
            this.publisher = publisher;
        }

        /// <summary>
        /// Deletes the specified hostel.
        /// </summary>
        /// <param name="hostel">The hostel.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task Delete(Hostel hostel)
        {
            hostel.Deleted = true;

            await this.hostelRepository.UpdateAsync(hostel);

            await this.publisher.EntityDeleted(hostel);
        }

        /// <summary>
        /// Gets all the hosted registered
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>the paged list</returns>
        public async Task<IPagedList<Hostel>> GetAll(string keyword = null, int? locationId = default(int?), int page = 0, int pageSize = int.MaxValue)
        {
            var query = this.hostelRepository.Table
                .Include(c => c.Currency)
                .Include(c => c.Location)
                .Where(c => !c.Deleted);

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => c.Name.Contains(keyword) || c.Email.Contains(keyword));
            }

            if (locationId.HasValue)
            {
                query = query.Where(c => c.LocationId == locationId.Value);
            }

            query = query.OrderBy(c => c.Name);

            return await new PagedList<Hostel>().Async(query, page, pageSize);
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// the hostel
        /// </returns>
        public async Task<Hostel> GetById(int id)
        {
            return await this.hostelRepository.Table
                .Include(c => c.Currency)
                .Include(c => c.Location)
                .FirstOrDefaultAsync(c => c.Id == id && !c.Deleted);
        }

        /// <summary>
        /// Inserts the specified hostel.
        /// </summary>
        /// <param name="hostel">The hostel.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task Insert(Hostel hostel)
        {
            hostel.CreationDateUtc = DateTime.UtcNow;

            try
            {
                await this.hostelRepository.InsertAsync(hostel);

                await this.publisher.EntityInserted(hostel);
            }
            catch (DbUpdateException e)
            {
                var inner = (SqlException)e.InnerException;

                if (inner.Number == 547)
                {
                    var target = "Unknown";

                    if (inner.Message.IndexOf("FK_Hostels_Currencies") != -1)
                    {
                        target = "Currency";
                    }
                    else if (inner.Message.IndexOf("FK_Hostels_Locations") != -1)
                    {
                        target = "Locations";
                    }

                    throw new HostaliandoException(target, HostaliandoExceptionCode.InvalidForeignKey);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Updates the specified hostel.
        /// </summary>
        /// <param name="hostel">The hostel.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task Update(Hostel hostel)
        {
            try
            {
                await this.hostelRepository.UpdateAsync(hostel);

                await this.publisher.EntityUpdated(hostel);
            }
            catch (DbUpdateException e)
            {
                var inner = (SqlException)e.InnerException;

                if (inner.Number == 547)
                {
                    var target = "Unknown";

                    if (inner.Message.IndexOf("FK_Hostels_Currencies") != -1)
                    {
                        target = "Currency";
                    }
                    else if (inner.Message.IndexOf("FK_Hostels_Locations") != -1)
                    {
                        target = "Locations";
                    }

                    throw new HostaliandoException(target, HostaliandoExceptionCode.InvalidForeignKey);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}