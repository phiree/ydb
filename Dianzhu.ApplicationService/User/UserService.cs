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
        public userObj GetUserById(string userID,string userType)
        {
            Dianzhu.Model.DZMembership dzm =dzmsp .GetUserById(utils.CheckGuidID(userID, "userID"));
            userObj userobj = null;
            if (dzm.UserType.ToString()== userType)//customer=1"customer"
            {
                userobj = Mapper.Map<Dianzhu.Model.DZMembership, userObj>(dzm);
            }
            if (userobj == null)
            {
                throw new Exception(Dicts.StateCode[4]);
            }
            return userobj;
        }

        /// <summary>
        /// 根据用户信息获取user
        /// </summary>
        /// <param name="userFilter"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public userObj GetUserByInfo(common_Trait_UserFiltering userFilter, string userType)
        {
            if ((userFilter.alias == null || userFilter.alias == "") && (userFilter.email == null || userFilter.email == "") && (userFilter.phone == null || userFilter.phone == ""))
            {
                throw new Exception("至少要传入用户名、手机号码和邮箱三个中的一个！");
            }
            Dianzhu.Model.DZMembership dzm = dzmsp.GetUserByInfo(userFilter.alias, userFilter.email, userFilter.phone);
            userObj userobj = null;
            if (dzm.UserType.ToString() == userType)//customer=1"customer"
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
