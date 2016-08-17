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
using System.Text.RegularExpressions;


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
        public customerObj GetUserById(string userID,string userType)
        {
            Dianzhu.Model.DZMembership dzm =dzmsp .GetUserById(utils.CheckGuidID(userID, "userID"));
            customerObj userobj = null;
            if (dzm.UserType.ToString()== userType)//customer=1"customer"
            {
                userobj = Mapper.Map<Dianzhu.Model.DZMembership, customerObj>(dzm);
            }
            //if (userobj == null)
            //{
            //    throw new Exception(Dicts.StateCode[4]);
            //}
            return userobj;
        }

        /// <summary>
        /// 根据用户信息获取user
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="userFilter"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public IList<customerObj> GetUsers(common_Trait_Filtering filter,common_Trait_UserFiltering userFilter, string userType)
        {
            Model.Trait_Filtering filter1 = utils.CheckFilter(filter, "DZMembership");
            IList<Dianzhu.Model.DZMembership> dzm = dzmsp.GetUsers(filter1, userFilter.alias, userFilter.email, userFilter.phone, userFilter.platform, userType);
            if (dzm == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                return new List<customerObj>();
            }
            IList<customerObj> customerobj = Mapper.Map<IList<Dianzhu.Model.DZMembership>, IList<customerObj>>(dzm);
            return customerobj;
        }

        /// <summary>
        /// 统计用户数量
        /// </summary>
        /// <param name="userFilter"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public countObj GetUsersCount(common_Trait_UserFiltering userFilter, string userType)
        {
            countObj c = new countObj();
            c.count = dzmsp.GetUsersCount(userFilter.alias, userFilter.email, userFilter.phone, userFilter.platform, userType).ToString();
            return c;
        }


        /// <summary>
        /// 注册新用户
        /// </summary>
        /// <param name="userBody"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public object PostUser(Common_Body userBody, string userType)
        {
            if ((userBody.phone == null || userBody.phone == "") && (userBody.email == null || userBody.email == ""))
            {
                throw new FormatException("手机号码或手机至少一个不能没空！");
            }
            if (userBody.pWord == null || userBody.pWord == "")
            {
                throw new FormatException("密码不能没空！");
            }
            if (userBody.phone != null && userBody.phone != "")
            {
                Regex reg = new Regex(@"^1[3578]\d{9}$");
                if (userBody.phone.Length != 11 || !reg.IsMatch(userBody.phone))
                {
                    throw new FormatException("手机号码格式错误！");
                }
            }
            System.Web.Security.MembershipCreateStatus createStatus;
            Dianzhu.Model.Enums.enum_UserType usertype = (Model.Enums.enum_UserType)Enum.Parse(typeof(Model.Enums.enum_UserType), userType); 
            DZMembership newMember = dzmsp.CreateUser(string.Empty,
                 userBody.phone,
                 string.Empty,
                 userBody.pWord,
                 out createStatus,
                 usertype);
            if (createStatus == System.Web.Security.MembershipCreateStatus.DuplicateUserName)
            {
                throw new Exception("该手机号码用户已存在!");
            }
            else if (createStatus != System.Web.Security.MembershipCreateStatus.Success)
            {
                throw new Exception("注册失败!");
            }
            //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            //Dianzhu.Model.DZMembership dzm = dzmsp.GetUserById(newMember.Id);
            //if (dzm == null)
            //{
            //    throw new Exception("注册失败!");
            //}

            if (userType == "customer")
            {
                customerObj customerobj = Mapper.Map<Dianzhu.Model.DZMembership, customerObj>(newMember);
                return customerobj;
            }
            else
            {
                merchantObj merchantobj = Mapper.Map<Dianzhu.Model.DZMembership, merchantObj>(newMember);
                return merchantobj;
            }
        }

        /// <summary>
        /// 第三方用户注册
        /// </summary>
        /// <param name="u3rd_Model"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public object PostUser3rds(U3RD_Model u3rd_Model, string userType)
        {
            Dianzhu.Model.DZMembership newMember = new DZMembership();
            switch (u3rd_Model.target)
            {
                case "WeChat":
                    newMember=LoginByWeChat.GetUserInfo(u3rd_Model.code, u3rd_Model.appName, dzmsp, userType);
                    break;
                case "SinaWeiBo":
                    newMember = LoginBySinaWeiBo.GetUserInfo(u3rd_Model.code, u3rd_Model.appName, dzmsp, userType);
                    break;
                case "TencentQQ":
                    newMember = LoginByTencentQQ.GetUserInfo(u3rd_Model.code, u3rd_Model.appName, dzmsp, userType);
                    break;
                default:
                    throw new Exception("传入的第三方平台类型有误，请重新上传！!");
            }
           
            Dianzhu.Model.DZMembership dzm = dzmsp.GetUserById(newMember.Id);
            if (dzm == null)
            {
                throw new Exception("注册失败!");
            }
            if (userType == "customer")
            {
                customerObj customerobj = Mapper.Map<Dianzhu.Model.DZMembership, customerObj>(dzm);
                return customerobj;
            }
            else
            {
                merchantObj merchantobj = Mapper.Map<Dianzhu.Model.DZMembership, merchantObj>(dzm);
                return merchantobj;
            }
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="userChangeBody"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public object PatchUser(string userID, UserChangeBody userChangeBody,string userType)
        {
            if (userChangeBody.oldPassWord != null && userChangeBody.oldPassWord != "")
            {
                throw new FormatException("旧密码不能没空！");
            }
            Guid guidUser = utils.CheckGuidID(userID, "userID");
            Dianzhu.Model.DZMembership dzm = dzmsp.GetUserById(guidUser);
            if (dzm == null)
            {
                throw new Exception("该用户不存在！");
            }
            if (dzm.PlainPassword != userChangeBody.oldPassWord)
            {
                throw new Exception("密码错误！");
            }
            if (userChangeBody.alias != null && userChangeBody.alias != "")
            {
                dzm.NickName = userChangeBody.alias;
            }
            if (userChangeBody.email != null && userChangeBody.email != "")
            {
                dzm.Email = userChangeBody.email;
            }
            if (userChangeBody.phone != null && userChangeBody.phone != "")
            {
                dzm.Phone = userChangeBody.phone;
            }
            if (userChangeBody.address != null && userChangeBody.address != "")
            {
                dzm.Address = userChangeBody.address;
            }
            if (userChangeBody.sex != null && userChangeBody.sex != "")
            {
                //dzm.Address = userChangeBody.sex;
            }
            if (userChangeBody.newPassWord != null && userChangeBody.newPassWord != "")
            {
                if (!dzm.ChangePassword(userChangeBody.oldPassWord, userChangeBody.newPassWord))
                {
                    throw new Exception("密码错误!");
                }
            }
            DateTime dt = DateTime.Now;
            dzm.LastLoginTime = dt;
            dzmsp.UpdateDZMembership(dzm);

            dzm = dzmsp.GetUserById(guidUser);
            if (dzm.LastLoginTime != dt)
            {
                throw new Exception("修改失败!");
            }
            if (userType == "customer")
            {
                customerObj customerobj = Mapper.Map<Dianzhu.Model.DZMembership, customerObj>(dzm);
                return customerobj;
            }
            else
            {
                merchantObj merchantobj = Mapper.Map<Dianzhu.Model.DZMembership, merchantObj>(dzm);
                return merchantobj;
            }

        }


        public void Dispose()
        {
            //dzmsp.Dispose();
        }
    }
}
