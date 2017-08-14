//-----------------------------------------------------------------------
// <copyright file="CurrencyModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    /// <summary>
    /// Currency Model
    /// </summary>
    /// <seealso cref="Hostaliando.Web.Models.BaseModel" />
    public class CurrencyModel : BaseNamedModel
    {
        /// <summary>
        /// Gets or sets the symbol.
        /// </summary>
        /// <value>
        /// The symbol.
        /// </value>
        public string Symbol { get; set; }
    }
}