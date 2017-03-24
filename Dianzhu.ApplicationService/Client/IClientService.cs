using System;
using System.Collections.Generic;

namespace Dianzhu.ApplicationService.Client
{
    public interface IClientService : IDisposable
    {
        /// <summary>
        /// 创建Token值
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <param name="apiName"></param>
        /// <param name="apiKey"></param>
        /// <param name="strPath"></param>
        /// <returns></returns>
        UserTokentDTO CreateToken(string loginName, string password, string apiName, string apiKey, string strPath, string appName);

        /// <summary>
        /// 注册客户端
        /// </summary>
        /// <param name="client"></param>
        void RegisterClient(ClientDTO clientdto);

        /// <summary>
        /// 根据Id获取客户端数据
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        ClientDTO FindClient(string clientId);

        /// <summary>
        /// 添加新的RefreshToken
        /// </summary>
        /// <param name="token"></param>
        bool AddRefreshToken(RefreshTokenDTO tokendto);

        /// <summary>
        /// 根据Id删除RefreshToken
        /// </summary>
        /// <param name="refreshTokenId"></param>
        void RemoveRefreshToken(string refreshTokenId);

        /// <summary>
        /// 删除RefreshToken
        /// </summary>
        /// <param name="refreshtoken"></param>
        void RemoveRefreshToken(RefreshTokenDTO tokendto);

        /// <summary>
        /// 根据Id获取RefreshToken
        /// </summary>
        /// <param name="refreshTokenId"></param>
        /// <returns></returns>
        RefreshTokenDTO FindRefreshToken(string refreshTokenId);

        /// <summary>
        /// 获取所有的RefreshToken
        /// </summary>
        /// <returns></returns>
        IList<RefreshTokenDTO> GetAllRefreshTokens();
    }
}