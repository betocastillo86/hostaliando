//-----------------------------------------------------------------------
// <copyright file="LogMap.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Log Map
    /// </summary>
    public static class LogMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<Log> entity)
        {
            entity.ToTable("Logs");

            entity.Property(e => e.FullMessage).IsRequired();

            entity.Property(e => e.IpAddress).HasMaxLength(100);

            entity.Property(e => e.PageUrl).HasMaxLength(500);

            entity.Property(e => e.ShortMessage).IsRequired();

            entity.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Log_User");
        }
    }
}