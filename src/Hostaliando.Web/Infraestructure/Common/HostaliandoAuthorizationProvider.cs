//-----------------------------------------------------------------------
// <copyright file="HostaliandoAuthorizationProvider.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Infraestructure.Common
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AspNet.Security.OpenIdConnect.Extensions;
    using AspNet.Security.OpenIdConnect.Primitives;
    using AspNet.Security.OpenIdConnect.Server;
    using Hostaliando.Business.Services;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http.Authentication;

    /// <summary>
    /// Authorization Provider for OPENID connect.
    /// More information <c>http://kevinchalet.com/2016/07/13/creating-your-own-openid-connect-server-with-asos-creating-your-own-authorization-provider/</c>
    /// and <c>http://kevinchalet.com/2016/07/13/creating-your-own-openid-connect-server-with-asos-implementing-the-resource-owner-password-credentials-grant/</c>
    /// </summary>
    /// <seealso cref="AspNet.Security.OpenIdConnect.Server.OpenIdConnectServerProvider" />
    public class HostaliandoAuthorizationProvider : OpenIdConnectServerProvider
    {
        /// <summary>
        /// Represents an event called for each validated token request
        /// to allow the user code to decide how the request should be handled.
        /// </summary>
        /// <param name="context">The context instance associated with this event.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> that can be used to monitor the asynchronous operation.
        /// </returns>
        public override async Task HandleTokenRequest(HandleTokenRequestContext context)
        {
            if (context.Request.IsPasswordGrantType())
            {
                var userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));

                var user = await userService.GetUserAuthenticated(context.Request.Username, context.Request.Password);

                if (user != null)
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationScheme);

                    identity.AddClaim(OpenIdConnectConstants.Claims.Subject, user.Id.ToString());
                    identity.AddClaim(OpenIdConnectConstants.Claims.Name, user.Name);

                    var ticket = new AuthenticationTicket(
                        new ClaimsPrincipal(identity),
                        new AuthenticationProperties(),
                        context.Options.AuthenticationScheme);

                    ticket.SetScopes(
                        OpenIdConnectConstants.Scopes.OpenId,
                        OpenIdConnectConstants.Scopes.Email,
                        OpenIdConnectConstants.Scopes.Profile);

                    ticket.SetResources("http://hostaliando.com");

                    context.Validate(ticket);
                }
                else
                {
                    context.Reject(
                        error: OpenIdConnectConstants.Errors.AccessDenied,
                        description: "Los datos ingresados son invalidos");
                }
            }
        }

        /// <summary>
        /// Represents an event called for each request to the token endpoint
        /// to determine if the request is valid and should continue to be processed.
        /// </summary>
        /// <param name="context">The context instance associated with this event.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> that can be used to monitor the asynchronous operation.
        /// </returns>
        public override Task ValidateTokenRequest(ValidateTokenRequestContext context)
        {
            // Reject the token requests that don't use grant_type=password or grant_type=refresh_token.
            if (!context.Request.IsPasswordGrantType() && !context.Request.IsRefreshTokenGrantType())
            {
                context.Reject(
                           error: OpenIdConnectConstants.Errors.InvalidClient,
                           description: "Tipo de autenticación inválida");

                return Task.FromResult(0);
            }

            context.Skip();

            return Task.FromResult(0);
        }
    }
}