 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using Ydb.Common.Domain;
using Ydb.Membership.DomainModel.Enums;
namespace Ydb.Membership.DomainModel
{
    /// <summary>
    /// 基本的用户类.
    /// </summary>
    public class DZMembership:Entity<Guid>
    {
        public DZMembership()
        {
            LoginType = LoginType.original;
            IsRegisterValidated = false;
             LastLoginTime=  TimeCreated = DateTime.Now;
            
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
        public virtual int LoginTimes { get;   set; }
        /// <summary>
        /// 呼叫 或者 接收呼叫的次数
        /// </summary>
        public virtual int ReceptionTimes { get; set; }
        //public virtual Guid Id { get; set; }
        public virtual string UserName { get; set; }
        //用||(双竖线)替换邮箱用户中的@符号
        public virtual string UserNameForOpenFire { get; set; }
        public virtual string Password { get; set; }
        //todo: openfire用户验证只能用plain 无法使用md5.

        public virtual string PlainPassword { get; set; }
        public virtual DateTime TimeCreated { get; set; }
        public virtual DateTime LastLoginTime { get; set; }
        public virtual string Email{ get; set; }
        public virtual string  Phone{ get; set; }
        public virtual string NickName { get; set; }
        public virtual string Address { get; set; }

        /// <summary>
        /// 注册平台
        /// </summary>
        public virtual LoginType LoginType{ get; set; }

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
        public virtual string DisplayName {
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
        public virtual bool ChangePassword(string oldEncryptedPassword,string newPlainPassword, string newEncryptedPassword)
        {
           
            if (this.Password != oldEncryptedPassword) return false;
            else {
                this.Password = newEncryptedPassword;
                this.PlainPassword = newPlainPassword;
                return true;
            }

        }
        public virtual string  FriendlyUserType
        {
            get{

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
 
 
    }
}
