//-----------------------------------------------------------------------
// <copyright file="BookingExtensions.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Hostaliando.Data;

    /// <summary>
    /// Booking Extensions
    /// </summary>
    public static class BookingExtensions
    {
        /// <summary>
        /// To the model.
        /// </summary>
        /// <param name="booking">The booking.</param>
        /// <returns>the model</returns>
        public static BookingModel ToModel(this Booking booking)
        {
            return new BookingModel
            {
                Id = booking.Id,
                GuestName = booking.GuestName,
                GuestEmail = booking.GuestEmail,
                GuestLocation = booking.GuestLocation?.ToModel(),
                TotalPrice = booking.TotalPrice,
                FromDate = booking.FromDate,
                ToDate = booking.ToDate,
                Comments = booking.Comments,
                CreationDateUtc = booking.CreationDateUtc,
                Room = booking.Room?.ToModel(),
                Source = new BaseNamedModel { Id = booking.SourceId, Name = booking.Source?.Name }
            };
        }

        /// <summary>
        /// To the models.
        /// </summary>
        /// <param name="bookings">The bookings.</param>
        /// <returns>the list</returns>
        public static IList<BookingModel> ToModels(this ICollection<Booking> bookings)
        {
            return bookings.Select(ToModel).ToList();
        }
    }
}