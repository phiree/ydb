using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.BLL.Client;
using AutoMapper;

namespace Dianzhu.ApplicationService.Client
{
    public class ClientService:IClientService
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
        public void RegisterClient(ClientDTO clientdto)
        {
            Model.Client client= Mapper.Map<ClientDTO, Model.Client>(clientdto);
            ibllclient.RegisterClient(client);
        }

        /// <summary>
        /// 根据Id获取客户端数据
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public ClientDTO FindClient(string clientId)
        {
            Model.Client client= ibllclient.FindClient(clientId);
            ClientDTO clientdto = Mapper.Map<Model.Client, ClientDTO>(client);
            return clientdto;
        }

        /// <summary>
        /// 添加新的RefreshToken
        /// </summary>
        /// <param name="token"></param>
        public bool AddRefreshToken(RefreshTokenDTO tokendto)
        {
            Model.RefreshToken token = Mapper.Map<RefreshTokenDTO, Model.RefreshToken>(tokendto);
            return ibllrefreshtoken.AddRefreshToken(token);
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
        public void RemoveRefreshToken(RefreshTokenDTO tokendto)
        {
            Model.RefreshToken refreshtoken = Mapper.Map<RefreshTokenDTO, Model.RefreshToken>(tokendto);
            ibllrefreshtoken.RemoveRefreshToken(refreshtoken);
        }

        /// <summary>
        /// 根据Id获取RefreshToken
        /// </summary>
        /// <param name="refreshTokenId"></param>
        /// <returns></returns>
        public RefreshTokenDTO FindRefreshToken(string refreshTokenId)
        {
            Model.RefreshToken refreshtoken= ibllrefreshtoken.FindRefreshToken(refreshTokenId);
            RefreshTokenDTO tokendto = Mapper.Map< Model.RefreshToken, RefreshTokenDTO>(refreshtoken);
            return tokendto;
        }

        /// <summary>
        /// 获取所有的RefreshToken
        /// </summary>
        /// <returns></returns>
        public IList<RefreshTokenDTO> GetAllRefreshTokens()
        {
            IList < Model.RefreshToken > refreshtoken = ibllrefreshtoken.GetAllRefreshTokens();
            IList < RefreshTokenDTO> tokendto = Mapper.Map< IList<Model.RefreshToken>, IList< RefreshTokenDTO> >(refreshtoken);
            return tokendto;
        }

        public void Dispose()
        {
            //ibllclient.Dispose();
            //ibllrefreshtoken.Dispose();
        }
    }
}
