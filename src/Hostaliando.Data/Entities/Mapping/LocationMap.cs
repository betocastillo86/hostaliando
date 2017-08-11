//-----------------------------------------------------------------------
// <copyright file="LocationMap.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Location Map
    /// </summary>
    public static class LocationMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<Location> entity)
        {
            entity.ToTable("Locations");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            entity.HasOne(d => d.ParentLocation)
                .WithMany(p => p.Children)
                .HasForeignKey(d => d.ParentLocationId)
                .HasConstraintName("FK_Locations_Locations");
        }
    }
}