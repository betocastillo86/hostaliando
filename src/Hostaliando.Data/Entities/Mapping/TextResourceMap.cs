//-----------------------------------------------------------------------
// <copyright file="TextResourceMap.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Text Resource Map
    /// </summary>
    public static class TextResourceMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<TextResource> entity)
        {
            entity.ToTable("TextResources");

            entity.Property(e => e.Name).IsRequired();
        }
    }
}