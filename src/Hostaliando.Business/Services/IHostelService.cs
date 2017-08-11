//-----------------------------------------------------------------------
// <copyright file="IHostelService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Hostaliando.Data;

    /// <summary>
    /// The hostel service
    /// </summary>
    public interface IHostelService
    {
        /// <summary>
        /// Inserts the specified hostel.
        /// </summary>
        /// <param name="hostel">The hostel.</param>
        /// <returns>the task</returns>
        Task Insert(Hostel hostel);

        /// <summary>
        /// Gets all the hosted registered
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>the paged list</returns>
        Task<IPagedList<Hostel>> GetAll(string keyword = null, int? locationId = null, int page = 0, int pageSize = int.MaxValue);
    }
}