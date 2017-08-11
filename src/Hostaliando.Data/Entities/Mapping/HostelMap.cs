//-----------------------------------------------------------------------
// <copyright file="HostelMap.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Hostel Map
    /// </summary>
    public static class HostelMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<Hostel> entity)
        {
            entity.ToTable("Hostels");

            entity.Property(e => e.Address).HasColumnType("varchar(50)");

            entity.Property(e => e.CreationDateUtc).HasColumnType("datetime");

            entity.Property(e => e.Email)
                .IsRequired()
                .HasColumnType("varchar(150)");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(150)");

            entity.Property(e => e.PhoneNumber).HasColumnType("varchar(15)");

            entity.HasOne(d => d.Currency)
                .WithMany()
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Hostels_Currencies");

            entity.HasOne(d => d.Location)
                .WithMany()
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Hostels_Locations");
        }
    }
}