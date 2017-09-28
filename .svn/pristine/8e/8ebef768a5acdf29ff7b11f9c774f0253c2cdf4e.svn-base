using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace YY.Edu.Sys.Api.Providers
{
    public class YYAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// Client_Credentials_Grant方式
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;

            //原始方式
            //context.TryGetFormCredentials(out clientId, out clientSecret);

            //使用Basic Authentication传递clientId与clientSecret
            context.TryGetBasicCredentials(out clientId, out clientSecret);

            string clientIdConfig = System.Web.Configuration.WebConfigurationManager.AppSettings["ClientId"];
            string clientSecretConfig = System.Web.Configuration.WebConfigurationManager.AppSettings["ClientSecret"];

            if (clientId == clientIdConfig && clientSecret == clientSecretConfig)
            {
                context.Validated(clientId);
            }

            return base.ValidateClientAuthentication(context);
        }

        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            //普通
            //var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            //oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, "iOS App"));
            //var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties());
            //context.Validated(ticket);

            //return base.GrantClientCredentials(context);

            //refresh token
            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { "as:client_id", context.ClientId }
                });
            var ticket = new AuthenticationTicket(oAuthIdentity, props);

            context.Validated(ticket);
            return base.GrantClientCredentials(context);
        }

        /// <summary>
        /// Password_Credentials_Grant方式
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            if (string.IsNullOrWhiteSpace(context.Request.Query["domain"]))
                await base.GrantResourceOwnerCredentials(context);

            var loginService = new Services.LoginService(context.Request.Query["domain"], context.UserName, context.Password);
            if (loginService.Login())
            {
                var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties());
                context.Validated(ticket);
            }

            await base.GrantResourceOwnerCredentials(context);

        }

        public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.Rejected();
                return;
            }

            var newId = new ClaimsIdentity(context.Ticket.Identity);
            newId.AddClaim(new Claim("newClaim", "refreshToken"));

            var newTicket = new AuthenticationTicket(newId, context.Ticket.Properties);
            context.Validated(newTicket);

            await base.GrantRefreshToken(context);
        }



    }
}