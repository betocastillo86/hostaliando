//-----------------------------------------------------------------------
// <copyright file="ServiceRegister.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Infraestructure.Start
{
    using System;
    using System.Reflection;
    using Beto.Core.Data;
    using Beto.Core.EventPublisher;
    using Beto.Core.Exceptions;
    using Beto.Core.Helpers;
    using Beto.Core.Registers;
    using Hostaliando.Business.Exceptions;
    using Hostaliando.Business.Security;
    using Hostaliando.Business.Services;
    using Hostaliando.Data;
    using Hostaliando.Web.Common;
    using Hostaliando.Web.Infraestructure.Filters;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Service Register
    /// </summary>
    public static class ServiceRegister
    {
        /// <summary>
        /// Registers the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void RegisterServices(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddDbContext<HostaliandoContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IHostelService, HostelService>();

            services.AddScoped<IWorkContext, WorkContext>();

            services.AddScoped<ILoggerService, LogService>();

            services.AddScoped<IRoomService, RoomService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IBookingSourceService, BookingSourceService>();

            services.AddScoped<AuthorizeAdminAttribute>();

            //// Core
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            services.AddScoped<IMessageExceptionFinder, MessageExceptionFinder>();

            services.AddScoped<IDbContext, HostaliandoContext>();

            services.AddScoped<IHttpContextHelper, HttpContextHelper>();

            services.AddScoped<IServiceFactory, DefaultServiceFactory>();

            services.AddScoped<IPublisher, Publisher>();

            foreach (var implementationType in ReflectionHelper.GetTypesOnProject(typeof(ISubscriber<>), "Hostaliando"))
            {
                var servicesTypeFound = implementationType.GetTypeInfo().FindInterfaces(
                    (type, criteria) =>
                    {
                        return type.GetTypeInfo().IsGenericType && ((Type)criteria).GetTypeInfo().IsAssignableFrom(type.GetGenericTypeDefinition());
                    },
                    typeof(ISubscriber<>));

                foreach (var serviceFoundType in servicesTypeFound)
                {
                    services.AddScoped(serviceFoundType, implementationType);
                }
            }
        }
    }
}