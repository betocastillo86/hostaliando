//-----------------------------------------------------------------------
// <copyright file="LocationExtensions.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Hostaliando.Data;

    /// <summary>
    /// Location Extensions
    /// </summary>
    public static class LocationExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>the model</returns>
        public static LocationModel ToModel(this Location entity)
        {
            return new LocationModel { Id = entity.Id, Name = entity.Name, ParentLocation = entity.ParentLocation?.ToModel() };
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="locations">The locations.</param>
        /// <returns>the models</returns>
        public static IList<LocationModel> ToModels(this ICollection<Location> locations)
        {
            return locations.Select(ToModel).ToList();
        }
    }
}