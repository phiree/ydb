using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using Ydb.ApiClient.DomainModel;
using Ydb.ApiClient.DomainModel.IRepository;
namespace Ydb.ApiClient.Application
{
    public class RefreshTokenService :   IRefreshTokenService
    {
        IRepositoryRefreshToken repoRefreshtoken;
        public RefreshTokenService(IRepositoryRefreshToken repoRefreshToken)
        {
            this.repoRefreshtoken = repoRefreshtoken;
        }
        /// <summary>
        /// 添加新的RefreshToken
        /// </summary>
        /// <param name="token"></param>
        public bool AddRefreshToken( RefreshToken token)
        {
            return repoRefreshtoken.AddRefreshToken(token);
        }

        /// <summary>
        /// 根据Id删除RefreshToken
        /// </summary>
        /// <param name="refreshTokenId"></param>
        public void RemoveRefreshToken(string refreshTokenId)
        {
            repoRefreshtoken.RemoveRefreshToken(refreshTokenId);
        }

        /// <summary>
        /// 删除RefreshToken
        /// </summary>
        /// <param name="refreshtoken"></param>
        public void RemoveRefreshToken( RefreshToken refreshtoken)
        {
            repoRefreshtoken.RemoveRefreshToken(refreshtoken);
        }

        /// <summary>
        /// 根据Id获取RefreshToken
        /// </summary>
        /// <param name="refreshTokenId"></param>
        /// <returns></returns>
        public  RefreshToken FindRefreshToken(string refreshTokenId)
        {
            return repoRefreshtoken.FindRefreshToken(refreshTokenId);
        }

        /// <summary>
        /// 获取所有的RefreshToken
        /// </summary>
        /// <returns></returns>
        public IList< RefreshToken> GetAllRefreshTokens()
        {
            return repoRefreshtoken.GetAllRefreshTokens();
        }
    }
}
