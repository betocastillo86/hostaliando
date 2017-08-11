//-----------------------------------------------------------------------
// <copyright file="CurrencyExtensions.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using Hostaliando.Data;

    /// <summary>
    /// Currency extensions
    /// </summary>
    public static class CurrencyExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the model</returns>
        public static CurrencyModel ToModel(this Currency entity)
        {
            return new CurrencyModel { Id = entity.Id, Name = entity.Name, Symbol = entity.Symbol };
        }
    }
}