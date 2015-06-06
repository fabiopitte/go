using GO.Domain;
using GO.Infra.SqlServer;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace OAuthServer.Api
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async System.Threading.Tasks.Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async System.Threading.Tasks.Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            try
            {
                var user = context.UserName;
                var password = context.Password;

                var users = new Repository<User>().Login(new User { Login = user, Password = password });

                if (null == users)
                {
                    context.SetError("invalid_grant", "Usuário ou senha inválidos");

                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                identity.AddClaim(new Claim(ClaimTypes.Name, user));

                var roles = new List<string>();

                roles.Add("User");

                roles.ForEach(role => { identity.AddClaim(new Claim(ClaimTypes.Role, role)); });

                GenericPrincipal principal = new GenericPrincipal(identity, roles.ToArray());

                Thread.CurrentPrincipal = principal;

                context.Validated(identity);
            }
            catch (System.Exception ex)
            {
                context.SetError("invalid_grant", "Falha ao autenticar no sistema");
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}