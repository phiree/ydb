using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel;
using Ydb.Membership.DomainModel.Enums;
using Ydb.Common.Application;
using Ydb.Common.Specification;
using Ydb.Membership.Application.Dto;
using Ydb.Common.Domain;

namespace Ydb.Membership.Application
{
  public interface  IDZMembershipService
    {
        /// <summary>
        /// 商户注册
        /// </summary>
        /// <param name="registerName">登录名</param>
        /// <param name="password">密码</param>
        /// <param name="confirmPassword"></param>
        /// <param name="hostInMail">验证邮件中链接的主机名称或者ip地址</param>
        /// <returns></returns>
        Dto.RegisterResult RegisterBusinessUser(string registerName, string password,string confirmPassword,string hostInMail);
        Dto.RegisterResult RegisterCustomerService(string registerName, string password, string confirmPassword, string hostInMail);
        Dto.RegisterResult RegisterMember(string registerName, string password, string confirmPassword, string userType, string hostInMail);
        Dto.RegisterResult RegisterStaff(string registerName, string password, string confirmPassword,   string hostInMail);
        /// <summary>
        /// 重新发送注册验证邮件
        /// </summary>
        /// <param name="username"></param>
        /// <param name="hostInEmail"></param>
        /// <returns></returns>
        ActionResult ResendVerifyEmail(string username, string hostInEmail);

        /// <summary>
        /// 验证注册代码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="verifyCode">注册验证码</param>
        /// <returns></returns>
        ActionResult VerifyRegisterCode(string verifyCode, string userid);
        
        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
         Dto.MemberDto GetUserByName(string userName);

        /// <summary>
        /// 根据用户名id获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Dto.MemberDto GetUserById(string id);
        IList<MemberDto> GetAllCustomer(int currentPageIndex, int pageSize, out long totalRecord);

        /// <summary>
        /// 用户验证
        /// </summary>
        /// <param name="userNameOrUserId">用户登录名或者ID</param>
        /// <param name="password"></param>
        /// <returns>验证结果,如果验证通过则包含memberdto</returns>
        Dto.ValidateResult  ValidateUser(string userNameOrUserId, string password,bool isLogin);
        Dto.ValidateResult Login(string userNameOrUserId, string password);
        Dto.MemberDto Login3rd(string platForm, string code, string appName,  string userType);
        /// <summary>
        /// 申请重置密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="hostInMail">验证邮件中链接的主机名称或者ip地址</param>
        /// <returns></returns>
        ActionResult ApplyRecovery(string userName,string hostInMail);

        

        ActionResult ChangePassword(string userName, string oldPassword, string newPassword);


        ActionResult RecoveryPasswordByPhone(string phone, string newPassword);

         ActionResult ChangeUserCity(Guid memberId, string cityCode, string longitude, string latitude, Area area);


        ActionResult RecoveryPassword( string recoveryString, string newPassword);

        ActionResult ChangePhone(string userId,string newPhone);
        ActionResult ChangeEmail(string userId, string newEmail);
        ActionResult ChangeAlias(string userId, string neAlias);
        ActionResult ChangeAddress(string userId, string newAddress);
        ActionResult ChangeAvatar(string userId, string newAvatar);
        IList<Dto.MemberDto> GetUsers(TraitFilter filter, string name, string email, string phone, string loginType, string userType);
        long GetUsersCount(string name, string email, string phone, string loginType, string userType);
         
    }
}
