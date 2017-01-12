using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Membership.Application
{
    public interface IUserTokenService
    {
        /// <summary>
        /// 新增或修改token
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="token"></param>
        /// <param name="appName"></param>
        void addToken(string userID, string token, string appName);

        /// <summary>
        /// 判断token是否存在
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool CheckToken(string token);

        /// <summary>
        /// 让token失效
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        void DeleteToken(string userID);
    }
}
