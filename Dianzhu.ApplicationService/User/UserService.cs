using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.BLL;
using System.Configuration;

namespace Dianzhu.ApplicationService.User
{
    public class UserService
    {
        DZMembershipProvider dzmsp;
        public UserService(DZMembershipProvider dzmsp)
        {
            this.dzmsp = dzmsp;
        }

        /// <summary>
        /// 验证用户名密码验证
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidateUser(string username, string password)
        {
            return dzmsp.ValidateUser(username, password);
        }
    }
}
