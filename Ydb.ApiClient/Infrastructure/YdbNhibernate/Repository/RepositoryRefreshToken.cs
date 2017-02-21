using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.ApiClient.DomainModel;
using Ydb.ApiClient.DomainModel.IRepository;
using Ydb.ApiClient.Intrastructure.YdbNhibernate.UnitOfWork;

namespace Ydb.ApiClient.Intrastructure.YdbNhibernate.Repository
{
    public class RepositoryRefreshToken : NHRepositoryBase<RefreshToken, string>, IRepositoryRefreshToken
    {
       
        /// <summary>
        /// 添加新的RefreshToken
        /// </summary>
        /// <param name="token"></param>
        public bool AddRefreshToken( RefreshToken token)
        {
            
                //判断某客户端的某个用户是否已经生成RefreshToken,若存在就先删除、后添加
                var existingToken =FindOne(r => r.Subject == token.Subject && r.ClientId == token.ClientId);
                if (existingToken != null)
                {
                     Delete(existingToken);
                }

                //Session.Save(token);
                bool b = true;
                try
                {
                    Add(token);
                }
                catch { b = false; }
               
                return b;
            
        }

        /// <summary>
        /// 根据Id删除RefreshToken
        /// </summary>
        /// <param name="refreshTokenId"></param>
        public void RemoveRefreshToken(string refreshTokenId)
        {
            var existingToken = FindOne(r => r.Id == refreshTokenId);
            Delete(existingToken);
        }

        /// <summary>
        /// 删除RefreshToken
        /// </summary>
        /// <param name="refreshtoken"></param>
        public void RemoveRefreshToken( RefreshToken refreshtoken)
        {
            Delete(refreshtoken);
        }

        /// <summary>
        /// 根据Id获取RefreshToken
        /// </summary>
        /// <param name="refreshTokenId"></param>
        /// <returns></returns>
        public  RefreshToken FindRefreshToken(string refreshTokenId)
        {
         
            return FindOne(x => x.Id == refreshTokenId);
        }

        /// <summary>
        /// 获取所有的RefreshToken
        /// </summary>
        /// <returns></returns>
        public IList< RefreshToken> GetAllRefreshTokens()
        {
             
            return Find(x => true);
        }
    }
}
