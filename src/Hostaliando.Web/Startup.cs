//-----------------------------------------------------------------------
// <copyright file="Startup.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Start class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The configure middleware
        /// </summary>
        /// <param name="app">the application</param>
        /// <param name="env">the environment</param>
        /// <param name="loggerFactory">the logger factory</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        /// <summary>
        /// Configures all the services
        /// </summary>
        /// <param name="services">the service collection</param>
        public void ConfigureServices(IServiceCollection services)
        {
        }
    }
}