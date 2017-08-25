//-----------------------------------------------------------------------
// <copyright file="AuthenticationInitialization.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Infraestructure.Start
{
    using Hostaliando.Web.Infraestructure.Common;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Authentication services and middleware initialization class
    /// </summary>
    public static class AuthenticationInitialization
    {
        /// <summary>
        /// Registers the authentication services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void RegisterAuthenticationServices(this IServiceCollection services)
        {
            services.AddAuthentication();
        }

        /// <summary>
        /// Configures the OPENID Server.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void ConfigureOpenId(this IApplicationBuilder app)
        {
            // Configuration taken from http://kevinchalet.com/2016/07/13/creating-your-own-openid-connect-server-with-asos-registering-the-middleware-in-the-asp-net-core-pipeline/
            //// TODO: Configurar
            app.UseOAuthValidation();

            app.UseOpenIdConnectServer(c =>
            {
                c.Provider = new HostaliandoAuthorizationProvider();

                c.AccessTokenLifetime = new System.TimeSpan(150, 0, 0);

                // Enable the authorization and token endpoints.
                c.TokenEndpointPath = "/api/v1/auth";

                c.AllowInsecureHttp = true;
            });
        }
    }
}