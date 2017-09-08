//-----------------------------------------------------------------------
// <copyright file="SeedBookingSources.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data.Migrations
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Seed Booking Sources
    /// </summary>
    public static class SeedBookingSources
    {
        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void Seed(HostaliandoContext context)
        {
            var list = new List<BookingSource>();

            list.Add(new BookingSource { Name = "Fisico", Description = "Cuando la persona llega al hostal", Color = "#3361FF" });
            list.Add(new BookingSource { Name = "TripAdvisor", Description = "Por trip advisor", Color = "#05771F" });
            list.Add(new BookingSource { Name = "Despegar", Description = "Por Despegar", Color = "#180577" });
            list.Add(new BookingSource { Name = "Airbnb", Description = "Por Airbnb", Color = "#F178BC" });

            foreach (var item in list)
            {
                if (!context.BookingSources.Any(c => c.Name.Equals(item.Name)))
                {
                    context.BookingSources.Add(item);
                }
            }

            context.SaveChanges();
        }
    }
}