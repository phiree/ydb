using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel;
using Ydb.Membership.DomainModel.Repository;
using Ydb.Membership.Infrastructure;

namespace Ydb.Membership.Application
{
    public class UserTokenService : IUserTokenService
    {
        IRepositoryUserToken repositoryUserToken;
        public UserTokenService(IRepositoryUserToken repositoryUserToken)
        {
            this.repositoryUserToken = repositoryUserToken;
        }

        /// <summary>
        /// 新增或修改token
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="token"></param>
        /// <param name="appName"></param>
        [UnitOfWork]
        public void addToken(string userID, string token, string appName)
        {
            repositoryUserToken.addToken(userID, token, appName);
        }

        /// <summary>
        /// 判断token是否存在
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [UnitOfWork]
        public bool CheckToken(string token)
        {
            return repositoryUserToken.CheckToken(token);
        }

        /// <summary>
        /// 让token失效
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [UnitOfWork]
        public void DeleteToken(string userID)
        {
            repositoryUserToken.DeleteToken(userID);
        }
    }
}
