//-----------------------------------------------------------------------
// <copyright file="EmailNotificationMap.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Email Notification Map
    /// </summary>
    public static class EmailNotificationMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<EmailNotification> entity)
        {
            entity.ToTable("EmailNotifications");

            entity.Property(e => e.Body).IsRequired();

            entity.Property(e => e.Cc)
                .HasColumnName("CC")
                .HasMaxLength(500);

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.ScheduledDate).HasColumnType("datetime");

            entity.Property(e => e.SentDate).HasColumnType("datetime");

            entity.Property(e => e.Subject)
                .IsRequired()
                .HasColumnType("varchar(300)");

            entity.Property(e => e.To)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.ToName).HasMaxLength(200);
        }
    }
}