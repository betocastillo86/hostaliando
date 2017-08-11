//-----------------------------------------------------------------------
// <copyright file="BookingMap.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Booking Map
    /// </summary>
    public static class BookingMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<Booking> entity)
        {
            entity.ToTable("Bookings");

            entity.Property(e => e.CreationDateUtc).HasColumnType("datetime");

            entity.Property(e => e.FromDate).HasColumnType("datetime");

            entity.Property(e => e.GuestEmail).HasColumnType("varchar(150)");

            entity.Property(e => e.GuestName)
                .IsRequired()
                .HasColumnType("varchar(150)");

            entity.Property(e => e.ToDate).HasColumnType("datetime");

            entity.Property(e => e.TotalPrice).HasColumnType("money");

            entity.HasOne(d => d.GuestLocation)
                .WithMany()
                .HasForeignKey(d => d.GuestLocationId)
                .HasConstraintName("FK_Bookings_Locations");

            entity.HasOne(d => d.Room)
                .WithMany(p => p.Bookings)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Bookings_Rooms");

            entity.HasOne(d => d.Source)
                .WithMany()
                .HasForeignKey(d => d.SourceId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Bookings_BookingSources");

            entity.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Bookings_Users");
        }
    }
}