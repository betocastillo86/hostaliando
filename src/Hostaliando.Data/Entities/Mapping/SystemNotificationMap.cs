//-----------------------------------------------------------------------
// <copyright file="SystemNotificationMap.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// System Notification Map
    /// </summary>
    public static class SystemNotificationMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<SystemNotification> entity)
        {
            entity.ToTable("SystemNotifications");

            entity.Property(e => e.TargetUrl)
                .IsRequired()
                .HasColumnName("TargetURL")
                .HasMaxLength(500);

            entity.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(500);

            entity.HasOne(d => d.TriggerUser)
                .WithMany()
                .HasForeignKey(d => d.TriggerUserId)
                .HasConstraintName("FK_SystemNotification_TriggerUser");

            entity.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_SystemNotification_User");
        }
    }
}