using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using Dianzhu.ApplicationService;
using Dianzhu.Model;
using Dianzhu.BLL;

namespace Dianzhu.Web.RestfulApi
{
    internal class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        //ApplicationService.Client.IClientService iclientservice;
        //ApplicationService.User.IUserService iuserservice;
        //public AuthorizationServerProvider(ApplicationService.Client.IClientService iclientservice,
        //ApplicationService.User.IUserService iuserservice)
        //{
        //    this.iclientservice = iclientservice;
        //    this.iuserservice = iuserservice;
        //}

        //public AuthorizationServerProvider()
        //{
        //}


        

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            ApplicationService.Client.IClientService iclientservice = Bootstrap.Container.Resolve<ApplicationService.Client.IClientService>();
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            ClientDTO clientdto = null;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                //Remove the comments from the below line context.SetError, and invalidate context 
                //if you want to force sending clientId/secrects once obtain access tokens. 
                //context.Validated();
                context.SetError("invalid_clientId", "ClientId should be sent.");
                return Task.FromResult<object>(null);
            }

            //using (ApplicationService.Client.IClientService iclientservice = new ApplicationService.Client.ClientService(new BLL.Client.BLLClient (),new BLL.Client.BLLRefreshToken()))
            //{
            clientdto = iclientservice.FindClient(context.ClientId);
            //}


            if (clientdto == null)
            {
                context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
                return Task.FromResult<object>(null);
            }

            if (clientdto.ApplicationType == ApplicationTypes.NativeConfidential)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client secret should be sent.");
                    return Task.FromResult<object>(null);
                }
                else
                {
                    if (clientdto.Secret != clientSecret)//if (client.Secret != Helper.GetHash(clientSecret))
                    {
                        context.SetError("invalid_clientId", "Client secret is invalid.");
                        return Task.FromResult<object>(null);
                    }
                }
            }

            if (!clientdto.Active)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return Task.FromResult<object>(null);
            }

            //context.OwinContext.Set<string>("as:ApplicationType", clientdto.ApplicationType.ToString ());
            context.OwinContext.Set<string>("as:clientAllowedOrigin", clientdto.AllowedOrigin);
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", clientdto.RefreshTokenLifeTime.ToString());

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            ApplicationService.User.IUserService iuserservice = Bootstrap.Container.Resolve<ApplicationService.User.IUserService>();
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

            if (allowedOrigin == null) allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            //using (ApplicationService.User.IUserService iuserservice = new ApplicationService.User.UserService(new DZMembershipProvider()))
            //{
                //UserModel user = _repo.FindUser(context.UserName, context.Password);

                if (!iuserservice.ValidateUser (context.UserName, context.Password))
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
            //}

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            identity.AddClaim(new Claim("sub", context.UserName));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                    },
                    {
                        "userName", context.UserName
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);

        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            // Change auth ticket for refresh token requests  
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newClaim = newIdentity.Claims.Where(c => c.Type == "newClaim").FirstOrDefault();//newClaim
            if (newClaim != null)
            {
                newIdentity.RemoveClaim(newClaim);
            }
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            //var newClaim = newIdentity.Claims.Where(c => c.Value == "user").FirstOrDefault();//newClaim
            //if (newClaim != null)
            //{
            //    newIdentity.RemoveClaim(newClaim);
            //}
            //newIdentity.AddClaim(new Claim(ClaimTypes.Role, "user1"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
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