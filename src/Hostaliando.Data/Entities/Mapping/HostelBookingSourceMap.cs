//-----------------------------------------------------------------------
// <copyright file="HostelBookingSourceMap.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Hostel Booking Source Map
    /// </summary>
    public static class HostelBookingSourceMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<HostelBookingSource> entity)
        {
            entity.ToTable("HostelBookingSources");

            entity.HasOne(d => d.Hostel)
                .WithMany(p => p.HostelBookingSources)
                .HasForeignKey(d => d.HostelId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_HostelBookingSources_Hostels");

            entity.HasOne(d => d.Source)
                .WithMany()
                .HasForeignKey(d => d.SourceId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_HostelBookingSources_BookingSources");
        }
    }
}