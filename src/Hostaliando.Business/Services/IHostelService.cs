//-----------------------------------------------------------------------
// <copyright file="IHostelService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    using System.Threading.Tasks;
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
    }
}