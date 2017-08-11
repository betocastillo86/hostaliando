//-----------------------------------------------------------------------
// <copyright file="UserMap.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// User Map
    /// </summary>
    public static class UserMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<User> entity)
        {
            entity.ToTable("Users");

            entity.Property(e => e.Email)
                .IsRequired()
                .HasColumnType("varchar(150)");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(150)");

            entity.Property(e => e.Password)
                .IsRequired()
                .HasColumnType("varchar(50)");

            entity.Property(e => e.Salt)
                .IsRequired()
                .HasColumnType("varchar(6)");

            entity.HasOne(d => d.Hostel)
                .WithMany()
                .HasForeignKey(d => d.HostelId)
                .HasConstraintName("FK_Users_Hostels");
        }
    }
}