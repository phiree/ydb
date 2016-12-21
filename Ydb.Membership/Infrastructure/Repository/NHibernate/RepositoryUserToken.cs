using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel;
using Ydb.Membership.DomainModel.Enums;
 
using Ydb.Membership.DomainModel.Repository;
using NHibernate;
using System.Linq.Expressions;
using Ydb.Common.Repository;
namespace Ydb.Membership.Infrastructure.Repository.NHibernate
{
   public class RepositoryUserToken:NHRepositoryBase<UserToken,Guid>,IRepositoryUserToken
    {
        /// <summary>
        /// 新增或修改token
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="token"></param>
        /// <param name="appName"></param>
        public void addToken(string userID,string token,string appName)
        {
            UserToken usertokenOld = FindOne(x => x.UserID == userID && x.AppName == appName);
            if (usertokenOld != null)
            {
                usertokenOld.Token = token;
                usertokenOld.CreatedTime = DateTime.Now;
                usertokenOld.Flag = 1;
            }
            else
            {
                UserToken usertoken = new UserToken
                {
                    UserID = userID,
                    AppName = appName,
                    Token = token,
                    Flag = 1,
                    CreatedTime = DateTime.Now
                };
                Add(usertoken);
            }
        }

        /// <summary>
        /// 判断token是否存在
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool CheckToken(string token)
        {
            UserToken usertokenOld = FindOne(x => x.Token == token && x.Flag == 1);
            if (usertokenOld == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 让token失效
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public void DeleteToken(string userID)
        {
            IList<UserToken> usertokenlist = Find(x => x.UserID == userID && x.Flag == 1);
            foreach (UserToken usertoken in usertokenlist)
            {
                usertoken.Flag = 0;
            }
        }

    }
}
