//-----------------------------------------------------------------------
// <copyright file="ServiceRegister.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Infraestructure.Start
{
    using System;
    using System.Reflection;
    using Beto.Core.Caching;
    using Beto.Core.Data;
    using Beto.Core.Data.Configuration;
    using Beto.Core.Data.Notifications;
    using Beto.Core.EventPublisher;
    using Beto.Core.Exceptions;
    using Beto.Core.Helpers;
    using Beto.Core.Registers;
    using Hostaliando.Business.Configuration;
    using Hostaliando.Business.Exceptions;
    using Hostaliando.Business.Security;
    using Hostaliando.Business.Services;
    using Hostaliando.Business.Tasks;
    using Hostaliando.Data;
    using Hostaliando.Web.Common;
    using Hostaliando.Web.Infraestructure.Common;
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

            //// Settings

            services.AddScoped<IGeneralSettings, GeneralSettings>();
            services.AddScoped<INotificationSettings, Hostaliando.Business.Configuration.NotificationSettings>();
            services.AddScoped<ITaskSettings, TaskSettings>();

            //// Others

            services.AddScoped<IWorkContext, WorkContext>();

            //// Services

            services.AddScoped<IHostelService, HostelService>();

            services.AddScoped<ILoggerService, LogService>();

            services.AddScoped<IRoomService, RoomService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IBookingSourceService, BookingSourceService>();

            services.AddScoped<IBookingService, BookingService>();

            services.AddScoped<ILocationService, LocationService>();

            services.AddScoped<ILogService, LogService>();

            services.AddScoped<ICurrencyService, CurrencyService>();

            services.AddScoped<INotificationService, NotificationService>();

            services.AddScoped<ISeoService, SeoService>();
            
            services.AddScoped<IJavascriptConfigurationGenerator, JavascriptConfigurationGenerator>();

            services.AddScoped<AuthorizeAdminAttribute>();

            //// Core
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            services.AddScoped<IMessageExceptionFinder, MessageExceptionFinder>();

            services.AddScoped<IDbContext, HostaliandoContext>();

            services.AddScoped<IHttpContextHelper, HttpContextHelper>();

            services.AddScoped<IServiceFactory, DefaultServiceFactory>();

            services.AddScoped<ICacheManager, MemoryCacheManager>();

            services.AddScoped<IPublisher, Publisher>();

            services.AddScoped<ICoreNotificationService, CoreNotificationService>();

            services.AddScoped<ICoreSettingService, CoreSettingService>();

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

            foreach (var implementationType in ReflectionHelper.GetTypesOnProject(typeof(ITask), "Hostaliando"))
            {
                var servicesTypeFound = implementationType.GetTypeInfo().FindInterfaces(
                    (type, criteria) =>
                    {
                        return true;
                    },
                    typeof(ITask));

                foreach (var serviceFoundType in servicesTypeFound)
                {
                    services.AddScoped(implementationType, implementationType);
                }
            }
        }
    }
}