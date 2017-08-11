//-----------------------------------------------------------------------
// <copyright file="DatabaseInitialization.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Infraestructure.Start
{
    using Hostaliando.Data;
    using Hostaliando.Data.Migrations;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    /// Initialize the database
    /// </summary>
    public static class DatabaseInitialization
    {
        /// <summary>
        /// Initializes the database.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public static void InitDatabase(this IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var context = (HostaliandoContext)app.ApplicationServices.GetService(typeof(HostaliandoContext)))
            {
                context.Database.EnsureCreated();
                context.EnsureSeeding();
            }
        }
    }
}