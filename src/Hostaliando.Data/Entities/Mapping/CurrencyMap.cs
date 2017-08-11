//-----------------------------------------------------------------------
// <copyright file="CurrencyMap.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Currency Map
    /// </summary>
    public static class CurrencyMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<Currency> entity)
        {
            entity.ToTable("Currencies");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");

            entity.Property(e => e.Symbol)
                .IsRequired()
                .HasColumnType("varchar(2)");
        }
    }
}