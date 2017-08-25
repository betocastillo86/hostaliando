//-----------------------------------------------------------------------
// <copyright file="MigrationsDbContext.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;

    /// <summary>
    /// Context for migrations
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.Infrastructure.IDbContextFactory{Hostaliando.Data.HostaliandoContext}" />
    public class MigrationsDbContext : IDbContextFactory<HostaliandoContext>
    {
        /// <summary>
        /// Creates a new instance of a derived context.
        /// </summary>
        /// <param name="options">Information about the environment an application is running in.</param>
        /// <returns>
        /// An instance of <typeparamref name="TContext" />.
        /// </returns>
        public HostaliandoContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<HostaliandoContext>();
            builder.UseSqlServer("Server=localhost;Database=Hostaliando;User Id=sa;Password=Temporal1;MultipleActiveResultSets=false");
            return new HostaliandoContext(builder.Options);
        }
    }
}