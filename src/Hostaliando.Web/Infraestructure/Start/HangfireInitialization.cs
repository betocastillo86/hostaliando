//-----------------------------------------------------------------------
// <copyright file="HangfireInitialization.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Infraestructure.Start
{
    using Hangfire;
    using Hostaliando.Business.Configuration;
    using Hostaliando.Business.Tasks;
    using Hostaliando.Web.Infraestructure.Filters;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Hang fire initialization
    /// </summary>
    public static class HangfireInitialization
    {
        /// <summary>
        /// Adds the hang fire.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="configuration">The configuration.</param>
        public static void AddHangFire(this IApplicationBuilder app, IConfigurationRoot configuration)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"));

            var dashboardOptions = new DashboardOptions()
            {
                Authorization = new[] { new HangFireAuthorizationFilter() }
            };

            app.UseHangfireDashboard(options: dashboardOptions);
            app.UseHangfireServer();
        }

        /// <summary>
        /// Registers the hang fire services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void RegisterHangFireServices(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddHangfire(c => c.UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection")));
        }

        /// <summary>
        /// Starts the recurring jobs.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void StartRecurringJobs(this IApplicationBuilder app)
        {
            var settings = (ITaskSettings)app.ApplicationServices.GetService(typeof(ITaskSettings));
            if (settings.SendEmailsInterval > 0)
            {
                RecurringJob.AddOrUpdate<SendMailTask>(c => c.SendPendingMails(), Cron.Minutely());
            }
        }
    }
}