//-----------------------------------------------------------------------
// <copyright file="HostelService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    using System;
    using System.Data.SqlClient;
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
    }
}