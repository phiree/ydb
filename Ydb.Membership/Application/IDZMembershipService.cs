﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel;
using Ydb.Membership.DomainModel.Enums;
using Ydb.Common.Application;
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
        /// 用户验证
        /// </summary>
        /// <param name="userNameOrUserId">用户登录名或者ID</param>
        /// <param name="password"></param>
        /// <returns>验证结果,如果验证通过则包含memberdto</returns>
        Dto.ValidateResult  ValidateUser(string userNameOrUserId, string password,bool isLogin);
        Dto.ValidateResult Login(string userNameOrUserId, string password);
        /// <summary>
        /// 申请重置密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="hostInMail">验证邮件中链接的主机名称或者ip地址</param>
        /// <returns></returns>
        ActionResult ApplyRecovery(string userName,string hostInMail);

        

        ActionResult ChangePassword(string userName, string oldPassword, string newPassword);
        ActionResult RecoveryPassword( string recoveryString, string newPassword);

        ActionResult ChangePhone(string userId,string newPhone);
        ActionResult ChangeEmail(string userId, string newEmail);

    }
}
