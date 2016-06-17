using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Dianzhu.ApplicationService;

namespace Dianzhu.Web.RestfulApi
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            ApplicationService.Client.IClientService iclientservice = Bootstrap.Container.Resolve<ApplicationService.Client.IClientService>();
            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");

            //using (AuthRepository _repo = new AuthRepository())
            //{
            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

            var token = new RefreshTokenDTO()
            {
                Id = Helper.GetHash(refreshTokenId),
                ClientId = clientid,
                Subject = context.Ticket.Identity.Name,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            token.ProtectedTicket = context.SerializeTicket();
            bool b = true;

            var result = iclientservice.AddRefreshToken(token);

            if (result)
            {
                context.SetToken(refreshTokenId);
            }

            //}
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            ApplicationService.Client.IClientService iclientservice = Bootstrap.Container.Resolve<ApplicationService.Client.IClientService>();
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            string hashedTokenId = Helper.GetHash(context.Token);

            //using (AuthRepository _repo = new AuthRepository())
            //{
            var refreshToken = iclientservice.FindRefreshToken(hashedTokenId);

            if (refreshToken != null)
            {
                //Get protectedTicket from refreshToken class
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                iclientservice.RemoveRefreshToken(hashedTokenId);
            }
            //}
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}