﻿ 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using Ydb.Common.Domain;
using Ydb.Common.Specification;
using Ydb.Membership.DomainModel.Enums;
using Ydb.Membership.DomainModel.Repository;
using Ydb.Common.Infrastructure;
using Ydb.Membership.Infrastructure;
namespace Ydb.Membership.DomainModel
{
    /// <summary>
    /// 基本的用户类.
    /// </summary>
    public class DZMembershipDomainService:IDZMembershipDomainService
    {
        IRepositoryDZMembership repositoryDZMembership;
        IRepositoryUserToken repositoryUserToken;

        IEncryptService encryptService;
        ILoginNameDetermine loginNameDetermine;
        public DZMembershipDomainService(IRepositoryDZMembership repositoryDZMembership,

               IRepositoryUserToken repositoryUserToken, ILoginNameDetermine loginNameDetermine
            ,IEncryptService encryptService)
        {


            this.repositoryDZMembership = repositoryDZMembership;// Bootstrap.Container.Resolve<IRepositoryDZMembership>();
            this.repositoryUserToken = repositoryUserToken;// Bootstrap.Container.Resolve<IRepositoryUserToken>();
            this.loginNameDetermine = loginNameDetermine;// Bootstrap.Container.Resolve<ILoginNameDetermine>();
            this.encryptService = encryptService;
        }
       
        public   bool ChangePassword(string username, string oldPassword, string newPassword,out string errMsg)
        {

            DZMembership validatedUser = ValidateUser(username, oldPassword,false, out errMsg);
            if (validatedUser==null)
            {
                return false;
            }
            if (newPassword.Length <6)
            {

                errMsg = "密码长度至少6位";
            }

            DZMembership member = repositoryDZMembership.GetMemberByName(username);
            string oldPasswordEncrypted =encryptService.GetMD5Hash(oldPassword);
            string newPasswordEncrypted = encryptService.GetMD5Hash(newPassword);
            member.ChangePassword(oldPasswordEncrypted, newPassword, newPasswordEncrypted);
            repositoryDZMembership.Update(member);

            repositoryUserToken.DeleteToken(member.Id.ToString());
            //System.Runtime.Caching.MemoryCache.Default.Remove(member.Id.ToString());
            //UserToken ut = repositoryUserToken.GetToken(member.Id.ToString());
            //if (ut != null)
            //{
            //    ut.Flag = 0;
            //    repositoryUserToken.Update(ut);
            //}

            return true;
        }

 
        public   DZMembership GetUser(string username, bool userIsOnline)
        {

            DZMembership user = repositoryDZMembership.GetMemberByName(username);
            if (user == null) return null;
             
            return user;

        }
 
        /// <summary>
        /// 用户验证
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="isLogin">是否是登录动作</param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public   DZMembership ValidateUser(string username, string password,bool isLogin ,out string errMsg)
        {
            errMsg = string.Empty;
            string encryptedPwd = encryptService.GetMD5Hash(password);

            DZMembership member = repositoryDZMembership.ValidateUser(username, encryptedPwd);

            if (member == null)
            {
                errMsg = "用户名/密码有误";

            }
            else
            {
                if (member.IsLocked)
                {
                    errMsg = "账户已封停";
                }
                if (isLogin)
                {
                    member.LoginTimes += 1;
                    member.LastLoginTime = DateTime.Now;
                }
            }
            return member;

        }



        #region additional method for user
        public DZMembership CreateUser(string loginName, string password, UserType userType, out string errMsg)
        {
            return CreateUser(Guid.NewGuid(), loginName, password, userType, out errMsg);
        }
        public DZMembership CreateUser(Guid id, string loginName, string password, UserType userType, out string errMsg)
        {
            errMsg = string.Empty;
            LoginNameType loginNameType = loginNameDetermine.Determin(loginName);
            string userName = loginName,
                    nickName = loginName,
                    email = string.Empty,
                    phone = string.Empty;
            Guid registerValidateCode = Guid.Empty;
            switch (loginNameType)
            {
                case LoginNameType.Email:
                    email = loginName;
                    registerValidateCode = Guid.NewGuid();
                    break;
                case LoginNameType.PhoneNumber: phone = loginName; break;
            }
            var existedUser = GetUserByName(loginName);
            if (existedUser != null)
            {
                errMsg = "用户已存在";
                return existedUser;
            }
            var password_cred = encryptService.GetMD5Hash(password).ToUpper();
            DZMembership newMember = new DZMembership
            {
                UserName = userName,
                Password = password_cred,
                Email = email,
                Phone = phone,
                NickName = nickName,
                PlainPassword = password,

                UserType = userType,
                LoginType = LoginType.original
            };
            if (registerValidateCode != Guid.Empty)
            {
                newMember.RegisterValidateCode = registerValidateCode.ToString();
            }

            repositoryDZMembership.Add(newMember);

            return newMember;

        }
        public void CreateUserForU3rd(DZMembership member)
        {
            repositoryDZMembership.Add(member);
        }
        public void UpdateUserForU3rd(DZMembership member)
        {
            repositoryDZMembership.Update(member);
        }
        public DZMembership GetUserByWechatOpenId(string openid)
        {
            return repositoryDZMembership.GetMemberByWechatOpenId(openid);
        }
        public DZMembership GetUserByQQOpenId(string openid)
        {
            return repositoryDZMembership.GetMemberByQQOpenId(openid);
        }
        public DZMembership GetUserBySinaWeiboUId(long uid)
        {
            return repositoryDZMembership.GetMemberBySinaWeiboUid(uid);
        }
        public virtual DZMembership GetUserByName(string name)
        {
            return repositoryDZMembership.GetMemberByName(name);
        }
        public virtual DZMembership GetUserById(Guid id)
        {
            return repositoryDZMembership.FindById(id);
        }
        public IList<DZMembership> GetAllDZMembers(int pageIndex, int pageSize, out long totalRecords)
        {
            return repositoryDZMembership.GetAllUsers(pageIndex, pageSize, out totalRecords);
        }

        public void UpdateDZMembership(DZMembership member)
        {

            //var password_cred = FormsAuthentication.HashPasswordForStoringInConfigFile(member.Password, "MD5");
            //member.Password = password_cred;
            repositoryDZMembership.Update(member);
        }
        public DZMembership GetUserByEmail(string email)
        {
            return repositoryDZMembership.GetMemberByEmail(email);
        }
        public DZMembership GetUserByPhone(string phone)
        {

            return repositoryDZMembership.GetMemberByPhone(phone);
        }

        /// <summary>
        /// 根据用户信息获取user
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="platform"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public IList<DZMembership> GetUsers(TraitFilter filter, string name, string email, string phone, string platform, string userType)
        {
            var where = PredicateBuilder.True<DZMembership>();
            where = where.And(x => x.UserType == (UserType)Enum.Parse(typeof(UserType), userType));
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(x => x.DisplayName.Contains(name));
            }
            if (!string.IsNullOrEmpty(email))
            {
                where = where.And(x => x.Email == email);
            }
            if (!string.IsNullOrEmpty(phone))
            {
                where = where.And(x => x.Phone == phone);
            }
            if (!string.IsNullOrEmpty(platform))
            {
                where = where.And(x => x.LoginType == (LoginType)Enum.Parse(typeof(LoginType), platform));
            }
            DZMembership baseone = null;
            if (!string.IsNullOrEmpty(filter.baseID))
            {
                try
                {
                    baseone = repositoryDZMembership.FindByBaseId(new Guid(filter.baseID));
                }
                catch (Exception ex)
                {
                    throw new Exception("filter.baseID错误，" + ex.Message);
                }
            }
            long t = 0;
            var list = filter.pageSize == 0 ? repositoryDZMembership.Find(where, filter.sortby, filter.ascending, filter.offset, baseone).ToList() : repositoryDZMembership.Find(where, filter.pageNum, filter.pageSize, out t, filter.sortby, filter.ascending, filter.offset, baseone).ToList();
            return list;
        }

        /// <summary>
        /// 统计用户数量
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="platform">用户的登录平台(微信,qq,微博,原生)</param>
        /// <param name="userType">用户类型:商户,助理,客户....</param>
        /// <returns></returns>
        public long GetUsersCount(string name, string email, string phone, string platform, string userType)
        {
            var where = PredicateBuilder.True<DZMembership>();
            where = where.And(x => x.UserType == (UserType)Enum.Parse(typeof(UserType), userType));
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(x => x.DisplayName.Contains(name));
            }
            if (!string.IsNullOrEmpty(email))
            {
                where = where.And(x => x.Email == email);
            }
            if (!string.IsNullOrEmpty(phone))
            {
                where = where.And(x => x.Phone == phone);
            }
            if (!string.IsNullOrEmpty(platform))
            {
                where = where.And(x => x.LoginType == (LoginType)Enum.Parse(typeof(LoginType), platform));
            }
            long count = repositoryDZMembership.GetRowCount(where);
            return count;
        }
        #endregion


        log4net.ILog log = log4net.LogManager.GetLogger("Ydb.DZMembership.DomainModel.DZMembershipProvider");
       
       
        public IList<DZMembership> GetAllCustomer(int pageIndex, int pageSize, out long totalRecords)
        {
            return repositoryDZMembership.GetAllCustomer(pageIndex, pageSize, out totalRecords);
        }

        public DZMembership CreateCustomerService(string loginName, string password, out string errMsg)
        {
            errMsg = string.Empty;
            LoginNameType loginNameType = loginNameDetermine.Determin(loginName);
            string userName = loginName,
                    nickName = loginName,
                    email = string.Empty,
                    phone = string.Empty;
            Guid registerValidateCode = Guid.Empty;
            switch (loginNameType)
            {
                case LoginNameType.Email:
                    email = loginName;
                    registerValidateCode = Guid.NewGuid();
                    break;
                case LoginNameType.PhoneNumber: phone = loginName; break;
            }
            var existedUser = GetUserByName(loginName);
            if (existedUser != null)
            {
                errMsg = "用户已存在";
                return existedUser;
            }
            var password_cred = encryptService.GetMD5Hash(password).ToUpper();
            DZMembershipCustomerService newMember = new DZMembershipCustomerService
            {
                UserName = userName,
                Password = password_cred,
                Email = email,
                Phone = phone,
                NickName = nickName,
                PlainPassword = password,

                UserType = UserType.customerservice,
                LoginType = LoginType.original
            };
            if (registerValidateCode != Guid.Empty)
            {
                newMember.RegisterValidateCode = registerValidateCode.ToString();
            }

            repositoryDZMembership.Add(newMember);

            return newMember;

        }


        public DZMembershipCustomerService GetDZMembershipCustomerServiceByName(string userName)
        {
            DZMembership membership = repositoryDZMembership.GetMemberByName(userName);
            if (membership != null && membership.GetType() == typeof(DZMembershipCustomerService))
            {
                return (DZMembershipCustomerService)membership;
            }
            else
            {
                throw new Exception("该助理不存在");
            }

        }


        public DZMembershipCustomerService GetDZMembershipCustomerServiceById(string id)
        {
            DZMembership membership = repositoryDZMembership.GetMemberById(new Guid(id));
            if (membership!=null && membership.GetType() == typeof(DZMembershipCustomerService))
            {
                return (DZMembershipCustomerService)membership;
            }
            else
            {
                throw new Exception("该助理不存在");
            }
        }

        /// <summary>
        /// 根据代理区域获取客服信息
        /// </summary>
        /// <param name="areaList"></param>
        /// <returns></returns>
        public IList<DZMembershipCustomerService> GetDZMembershipCustomerServiceByArea(IList<string> areaIdList)
        {
            IList<DZMembershipCustomerService> dzMembershipCustomerServiceList = new List<DZMembershipCustomerService>();
            IList<DZMembership> memberList = repositoryDZMembership.GetUsersByArea(areaIdList, DateTime.MinValue, DateTime.MinValue, UserType.customerservice);
            foreach (DZMembership member in memberList)
            {
                if (member.GetType() == typeof(DZMembershipCustomerService))
                {
                    DZMembershipCustomerService membershipCustomerService = (DZMembershipCustomerService)member;
                    dzMembershipCustomerServiceList.Add(membershipCustomerService);
                }
            }
            return dzMembershipCustomerServiceList;
        }

    }
}
