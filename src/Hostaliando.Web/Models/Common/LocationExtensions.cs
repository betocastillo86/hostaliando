//-----------------------------------------------------------------------
// <copyright file="LocationExtensions.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
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
            return new LocationModel { Id = entity.Id, Name = entity.Name };
        }
    }
}