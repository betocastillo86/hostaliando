//-----------------------------------------------------------------------
// <copyright file="HostelExtensions.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using Hostaliando.Data;

    /// <summary>
    /// Hostel Extensions
    /// </summary>
    public static class HostelExtensions
    {
        /// <summary>
        /// Converts to entity
        /// </summary>
        /// <param name="model">the model</param>
        /// <returns>the entity</returns>
        public static Hostel ToEntity(this HostelModel model)
        {
            return new Hostel
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                CurrencyId = model.Currency.Id,
                Email = model.Email,
                LocationId = model.Location.Id,
                PhoneNumber = model.PhoneNumber
            };
        }
    }
}