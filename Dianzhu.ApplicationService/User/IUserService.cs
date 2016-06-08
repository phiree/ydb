using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService.User
{
    public interface IUserService:IDisposable
    {
        /// <summary>
        /// 验证用户名密码验证
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
         bool ValidateUser(string username, string password);

        /// <summary>
        /// 根据userID获取user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        userObj GetUserById(string userID);
    }
}
