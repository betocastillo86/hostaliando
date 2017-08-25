//-----------------------------------------------------------------------
// <copyright file="Startup.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web
{
    using Beto.Core.Web.Api.Filters;
    using Beto.Core.Web.Middleware;
    using FluentValidation.AspNetCore;
    using Hostaliando.Web.Infraestructure.Start;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Start class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">The env.</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            this.Configuration = builder.Build();
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// The configure middleware
        /// </summary>
        /// <param name="app">the application</param>
        /// <param name="env">the environment</param>
        /// <param name="loggerFactory">the logger factory</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            ////TODO:Entender bien como funciona
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionsMiddleware>();

            app.ConfigureOpenId();

            app.InitDatabase(env);

            app.UseStaticFiles();

            app.UseMvc();
        }

        /// <summary>
        /// Configures all the services
        /// </summary>
        /// <param name="services">the service collection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterAuthenticationServices();

            services.AddMvc(options =>
            {
                options.Filters.Add(new FluentValidatorAttribute());

                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.RegisterServices(this.Configuration);
        }
    }
}