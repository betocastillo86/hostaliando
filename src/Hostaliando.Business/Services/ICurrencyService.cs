//-----------------------------------------------------------------------
// <copyright file="ICurrencyService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Hostaliando.Data;

    /// <summary>
    /// Interface of currency service
    /// </summary>
    public interface ICurrencyService
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>the list</returns>
        Task<IList<Currency>> GetAll();
    }
}