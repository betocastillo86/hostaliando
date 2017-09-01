//-----------------------------------------------------------------------
// <copyright file="EnsureSeedingExtension.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data.Migrations
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <summary>
    /// Ensure seeding extension
    /// </summary>
    public static class EnsureSeedingExtension
    {
        /// <summary>
        /// Ensures the seeding.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void EnsureSeeding(this HostaliandoContext context)
        {
            if (EnsureSeedingExtension.AreAllMigrationsApplied(context))
            {
                EnsureSeedingExtension.Seed(context);
            }
        }

        /// <summary>
        /// Validates if are all migrations applied.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>true if all migrations are applied</returns>
        private static bool AreAllMigrationsApplied(HostaliandoContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void Seed(HostaliandoContext context)
        {
            SeedLocations.Seed(context);
            SeedCurrencies.Seed(context);
            SeedUsers.Seed(context);
            SeedBookingSources.Seed(context);
            SeedSettings.Seed(context);
            SeedNotifications.Seed(context);
        }
    }
}