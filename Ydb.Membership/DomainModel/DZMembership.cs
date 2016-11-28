
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ydb.Common.Domain;
using Ydb.Common.Application;
using Ydb.Membership.DomainModel.Enums;
namespace Ydb.Membership.DomainModel
{
    /// <summary>
    /// 基本的用户类.
    /// </summary>
    public class DZMembership : Entity<Guid>
    {
        public DZMembership()
        {
            LoginType = LoginType.original;
            IsRegisterValidated = false;
            LastLoginTime = TimeCreated = DateTime.Now;

        }
        public static DZMembership Create(LoginType type)
        {
            switch (type)
            {
                case LoginType.original:
                    DZMembership newOriginal = new DZMembership();
                    return newOriginal;
                case LoginType.WeChat:
                    DZMembership newUserWechat = new DZMembershipWeChat();
                    return newUserWechat;
                case LoginType.SinaWeiBo:
                    DZMembership newUserSinaWeibo = new DZMembershipSinaWeibo();
                    return newUserSinaWeibo;
                case LoginType.TencentQQ:
                    DZMembership newUserQQ = new DZMembershipQQ();
                    return newUserQQ;

            }
            return null;
        }
        public virtual int LoginTimes { get; protected internal set; }
        /// <summary>
        /// 呼叫 或者 接收呼叫的次数
        /// </summary>
        public virtual int ReceptionTimes { get; set; }
        //public virtual Guid Id { get; set; }
        public virtual string UserName { get;protected internal set; }
        //用||(双竖线)替换邮箱用户中的@符号
        public virtual string UserNameForOpenFire { get; set; }
        public virtual string Password { get; set; }
        //todo: openfire用户验证只能用plain 无法使用md5.

        public virtual string PlainPassword { get; set; }
        public virtual DateTime TimeCreated { get; set; }
        public virtual DateTime LastLoginTime { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
        public virtual string NickName { get; set; }
        public virtual string Address { get; set; }

        /// <summary>
        /// 注册平台
        /// </summary>
        public virtual LoginType LoginType { get; set; }

        /// <summary>
        /// 注册验证码(邮箱验证链接,手机验证码)
        /// </summary>
        public virtual string RegisterValidateCode { get; set; }
        /// <summary>
        /// 是否通过了验证.
        /// </summary>
        public virtual bool IsRegisterValidated { get; set; }
        /// <summary>
        /// 找回密码时的验证码
        /// </summary>
        public virtual Guid RecoveryCode { get; set; }
        public virtual void CopyTo(DZMembership newMember)
        {
            newMember.Id = Id;
            newMember.UserName = UserName;
            newMember.Password = Password;
            newMember.TimeCreated = TimeCreated;
            newMember.LastLoginTime = LastLoginTime;
            newMember.Email = Email;
            newMember.Phone = Phone;
            newMember.NickName = NickName;
            newMember.Address = Address;
        }
        /// <summary>
        /// 头像图片相对路径.
        /// </summary>
        public virtual string AvatarUrl { get; set; }
        public virtual string DisplayName
        {
            get { return string.IsNullOrEmpty(NickName) ? UserName : NickName; }
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        public virtual UserType UserType { get; set; }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public virtual ActionResult ChangePassword(string oldEncryptedPassword, string newPlainPassword, string newEncryptedPassword)
        {
            return ChangePassword(oldEncryptedPassword, newPlainPassword, newEncryptedPassword, true);

        }
        protected virtual ActionResult ChangePassword(string oldEncryptedPassword, string newPlainPassword, string newEncryptedPassword, bool needOldPassword)
        {
            ActionResult result = new ActionResult();
            if (newPlainPassword.Length < 6)
            {
                result.IsSuccess = false;
                result.ErrMsg = "密码不能少于6个字符";
            }
            else if (needOldPassword && this.Password.ToLower() != oldEncryptedPassword.ToLower())
            {
                result.ErrMsg = "原密码有误";
                result.IsSuccess = false;
            }
            else if (this.Password == newEncryptedPassword)
            {
                result.IsSuccess = false;
                result.ErrMsg = "新密码与原密码相同,不需修改";
            }
            else
            {
                this.Password = newEncryptedPassword;
                this.PlainPassword = newPlainPassword;
                result.IsSuccess = true;
            }
            return result;

        }


       protected internal virtual ActionResult ChangePasswordByPhone(string newPlainPassword, string newEncryptedPassword)
        {
            ActionResult result = new ActionResult();
            if (newPlainPassword.Length < 6)
            {
                result.IsSuccess = false;
                result.ErrMsg = "密码不能少于6个字符";
            }
            else
            {
                this.Password = newEncryptedPassword;
                this.PlainPassword = newPlainPassword;
                result.IsSuccess = true;
            }
            return result;
        }
        public virtual string FriendlyUserType
        {
            get
            {

                return UserType == UserType.admin ? "后台管理员" :
                       UserType == UserType.business ? "商家" :
                       UserType == UserType.customer ? "客户" :
                       UserType == UserType.customerservice ? "助理" :
                       UserType == UserType.staff ? "员工" : "未知类型";
            }
        }
        /// <summary>
        /// 用户所属城市
        /// </summary>
        public virtual string UserCity { get; set; }
        /// <summary>
        /// 用户所在位置经度
        /// </summary>
        public virtual string Longitude { get; set; }
        /// <summary>
        /// 用户所在位置纬度
        /// </summary>
        public virtual string Latitude { get; set; }


        public virtual string BuildRegisterValidationContent(string host)
        {

            
            string body = string.Empty;
            if (IsRegisterValidated)
            {
                body = "您已经通过了邮件验证,无需重复验证";
            }
            else
            {

                string verifyUrl = host + "/verify.aspx"
                                 + "?userId=" + Id + "&verifyCode=" + RegisterValidateCode;

                body = "感谢您加入一点办.请点击下面的连接验证您的注册邮箱.</br>"
                     + "<a style='border:solid 1px #999;margin:20px;padding:10px 40px; background-color:#eee' href='"
                         + verifyUrl + "'>点击验证</a><br/><br/><br/>"
                     + "如果你无法点击此链接,请将下面的网址粘贴到浏览器地址栏.<br/><br/><br/>"
                     + verifyUrl;


            }
            IsRegisterValidated = false;
            return body;
        }

        public virtual string BuildRecoveryContent(string host)
        {
           
            string body = string.Empty;


            RecoveryCode = Guid.NewGuid();

            string recoveryUrl = host+ "/account/recovery.aspx"
                             +"?p=" + System.Uri.EscapeUriString(PHSuit.Security.Encrypt(UserName, false).Replace("+", "kewudejiahao")) 
                             + Config.pwssword_recovery_spliter + RecoveryCode;

              body = "您已申请密码重置.请点击下面的连接重置您的密码.</br>"
                        + "<a style='border:solid 1px #999;margin:20px;padding:10px 40px; background-color:#eee' href='"
                        + recoveryUrl + "'>点击验证</a><br/><br/><br/>"
                        + "如果你无法点击此链接,请将下面的网址粘贴到浏览器地址栏.<br/><br/><br/>"
                        + recoveryUrl;
            return body;
        }
        public virtual ActionResult RecoveryPassword(string recoveryCode, string newPassword,string newEncryptedPassword)
        {
            ActionResult actionresult = new ActionResult();
            //todo: 关于验证, 应该使用统一的验证机制, 区分不同的适用环境.
            if (recoveryCode != RecoveryCode.ToString())
            {
                actionresult.IsSuccess = false;
                actionresult.ErrMsg = "重置代码有误";
            }
            else
            {
             actionresult=   ChangePassword(string.Empty, newPassword, newEncryptedPassword, false);
            }
            return actionresult;
        }
    }
}
