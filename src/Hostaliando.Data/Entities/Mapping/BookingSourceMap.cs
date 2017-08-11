//-----------------------------------------------------------------------
// <copyright file="BookingSourceMap.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Booking Source Map
    /// </summary>
    public static class BookingSourceMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<BookingSource> entity)
        {
            entity.ToTable("BookingSources");

            entity.Property(e => e.Description).HasMaxLength(500);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(150)");
        }
    }
}