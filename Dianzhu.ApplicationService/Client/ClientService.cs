using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.BLL.Client;

namespace Dianzhu.ApplicationService.Client
{
    public class ClientService
    {
        IBLLClient ibllclient;
        IBLLRefreshToken ibllrefreshtoken;
        public ClientService(IBLLClient ibllclient, IBLLRefreshToken ibllrefreshtoken)
        {
            this.ibllclient = ibllclient;
            this.ibllrefreshtoken = ibllrefreshtoken;
        }

        /// <summary>
        /// 注册客户端
        /// </summary>
        /// <param name="client"></param>
        public void RegisterClient(Model.Client client)
        {
            ibllclient.RegisterClient(client);
        }

        /// <summary>
        /// 根据Id获取客户端数据
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public Model.Client FindClient(string clientId)
        {
            return ibllclient.FindClient(clientId);
        }

        /// <summary>
        /// 添加新的RefreshToken
        /// </summary>
        /// <param name="token"></param>
        public void AddRefreshToken(Model.RefreshToken token)
        {
            ibllrefreshtoken.AddRefreshToken(token);
        }

        /// <summary>
        /// 根据Id删除RefreshToken
        /// </summary>
        /// <param name="refreshTokenId"></param>
        public void RemoveRefreshToken(string refreshTokenId)
        {
            ibllrefreshtoken.RemoveRefreshToken(refreshTokenId);
        }

        /// <summary>
        /// 删除RefreshToken
        /// </summary>
        /// <param name="refreshtoken"></param>
        public void RemoveRefreshToken(Model.RefreshToken refreshtoken)
        {
            ibllrefreshtoken.RemoveRefreshToken(refreshtoken);
        }

        /// <summary>
        /// 根据Id获取RefreshToken
        /// </summary>
        /// <param name="refreshTokenId"></param>
        /// <returns></returns>
        public Model.RefreshToken FindRefreshToken(string refreshTokenId)
        {
            return ibllrefreshtoken.FindRefreshToken(refreshTokenId);
        }

        /// <summary>
        /// 获取所有的RefreshToken
        /// </summary>
        /// <returns></returns>
        public IList<Model.RefreshToken> GetAllRefreshTokens()
        {
            return ibllrefreshtoken.GetAllRefreshTokens();
        }
    }
}
