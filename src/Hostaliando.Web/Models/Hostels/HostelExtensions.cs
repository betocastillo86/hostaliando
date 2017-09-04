//-----------------------------------------------------------------------
// <copyright file="HostelExtensions.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using System.Collections.Generic;
    using System.Linq;
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
                PhoneNumber = model.PhoneNumber,
                HostelBookingSources = model.Sources != null ? model.Sources.Select(c => new HostelBookingSource { SourceId = c.Id }).ToList() : null
            };
        }

        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the model</returns>
        public static HostelModel ToModel(this Hostel entity)
        {
            return new HostelModel
            {
                Id = entity.Id,
                Address = entity.Address,
                Name = entity.Name,
                CreationDateUtc = entity.CreationDateUtc,
                Currency = entity.Currency?.ToModel(),
                Location = entity.Location?.ToModel(),
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber
            };
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>the models</returns>
        public static IList<HostelModel> ToModels(this ICollection<Hostel> entities)
        {
            return entities.Select(ToModel).ToList();
        }
    }
}