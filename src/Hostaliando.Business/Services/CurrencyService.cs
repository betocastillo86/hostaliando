//-----------------------------------------------------------------------
// <copyright file="CurrencyService.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Beto.Core.Data;
    using Hostaliando.Data;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Currency Service
    /// </summary>
    /// <seealso cref="Hostaliando.Business.Services.ICurrencyService" />
    public class CurrencyService : ICurrencyService
    {
        /// <summary>
        /// The currency repository
        /// </summary>
        private readonly IRepository<Currency> currencyRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyService"/> class.
        /// </summary>
        /// <param name="currencyRepository">The currency repository.</param>
        public CurrencyService(
            IRepository<Currency> currencyRepository)
        {
            this.currencyRepository = currencyRepository;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>the list</returns>
        public async Task<IList<Currency>> GetAll() => await this.currencyRepository.TableNoTracking.ToListAsync();
    }
}