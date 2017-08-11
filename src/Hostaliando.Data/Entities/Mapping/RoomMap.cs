//-----------------------------------------------------------------------
// <copyright file="RoomMap.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Room Map
    /// </summary>
    public static class RoomMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<Room> entity)
        {
            entity.ToTable("Rooms");

            entity.Property(e => e.CreationDateUtc).HasColumnType("datetime");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            entity.HasOne(d => d.Hostel)
                .WithMany(p => p.Rooms)
                .HasForeignKey(d => d.HostelId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Rooms_Hostels");

            entity.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Rooms_Users");
        }
    }
}