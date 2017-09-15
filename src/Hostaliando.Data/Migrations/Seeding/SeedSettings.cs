//-----------------------------------------------------------------------
// <copyright file="SeedSettings.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data.Migrations
{
    using System.Linq;

    /// <summary>
    /// Seed Settings
    /// </summary>
    public static class SeedSettings
    {
        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void Seed(HostaliandoContext context)
        {
            var list = new SystemSetting[]
            {
                new SystemSetting() { Name = "GeneralSettings.SiteUrl", Value = "http://localhost:64901/" },
                new SystemSetting() { Name = "GeneralSettings.BodyBaseHtml", Value = "<h1>Hostaliando</h1> %%Body%%" },
                new SystemSetting() { Name = "GeneralSettings.DateFormat", Value = "YYYY/MM/DD" }
            };

            foreach (var item in list)
            {
                if (!context.SystemSettings.Any(c => c.Name.Equals(item.Name)))
                {
                    context.SystemSettings.Add(item);
                }
            }

            context.SaveChanges();
        }
    }
}