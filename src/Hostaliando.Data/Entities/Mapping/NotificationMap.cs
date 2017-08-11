//-----------------------------------------------------------------------
// <copyright file="NotificationMap.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Notification Map
    /// </summary>
    public static class NotificationMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<Notification> entity)
        {
            entity.ToTable("Notifications");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.EmailSubject).HasMaxLength(500);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(e => e.SystemText).HasMaxLength(2000);

            entity.Property(e => e.Tags).HasMaxLength(3000);
        }
    }
}