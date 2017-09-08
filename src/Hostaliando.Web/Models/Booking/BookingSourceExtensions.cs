//-----------------------------------------------------------------------
// <copyright file="BookingSourceExtensions.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Hostaliando.Data;

    /// <summary>
    /// Booking Source Extensions
    /// </summary>
    public static class BookingSourceExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>the model</returns>
        public static BookingSourceModel ToModel(this BookingSource source)
        {
            return new BookingSourceModel
            {
                Id = source.Id,
                Name = source.Name,
                Color = source.Color,
                Icon = source.Icon
            };
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="sources">The sources.</param>
        /// <returns>the list</returns>
        public static IList<BookingSourceModel> ToModels(this ICollection<BookingSource> sources)
        {
            return sources.Select(ToModel).ToList();
        }
    }
}