//-----------------------------------------------------------------------
// <copyright file="IBookingSourceService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Hostaliando.Data;

    /// <summary>
    /// Interface of booking source service
    /// </summary>
    public interface IBookingSourceService
    {
        /// <summary>
        /// Gets all booking sources
        /// </summary>
        /// <returns>the list</returns>
        Task<IList<BookingSource>> GetAll();

        /// <summary>
        /// Gets the booking sources by hostel identifier.
        /// </summary>
        /// <param name="hostelId">The hostel identifier.</param>
        /// <returns>the list</returns>
        Task<IList<BookingSource>> GetByHostelId(int hostelId);

        /// <summary>
        /// Inserts the source to hostel.
        /// </summary>
        /// <param name="hostelBookingSource">The hostel booking source.</param>
        /// <returns>the task</returns>
        Task InsertSourceToHostel(HostelBookingSource hostelBookingSource);

        /// <summary>
        /// Deletes the source to hostel.
        /// </summary>
        /// <param name="hostelBookingSource">The hostel booking source.</param>
        /// <returns>the task</returns>
        Task DeleteSourceToHostel(HostelBookingSource hostelBookingSource);
    }
}