using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Configuration;
using AutoMapper;
using DDDCommon;


namespace Dianzhu.ApplicationService.User
{
    public class UserService:IUserService
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

        /// <summary>
        /// 根据userID获取user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public userObj GetUserById(string userID)
        {
            Dianzhu.Model.DZMembership dzm =dzmsp .GetUserById(new Guid(userID));
            userObj userobj = null;
            if (dzm.UserType.ToString()== "customer")//customer=1
            {
                userobj = Mapper.Map<Dianzhu.Model.DZMembership, userObj>(dzm);
            }
            if (userobj == null)
            {
                throw new Exception(Dicts.StateCode[4]);
            }
            return userobj;
        }


        public void Dispose()
        {
            //dzmsp.Dispose();
        }
    }
}
