//-----------------------------------------------------------------------
// <copyright file="ILogService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Hostaliando.Data;

    /// <summary>
    /// Log Service
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>the logs</returns>
        Task<IPagedList<Log>> GetAll(string keyword = null, int page = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        /// <returns>the task</returns>
        Task Clear();
    }
}