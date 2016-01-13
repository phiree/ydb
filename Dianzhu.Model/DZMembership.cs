using Dianzhu.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 基本的用户类.
    /// </summary>
  
    public class DZMembership
    {
        public DZMembership()
        {
            IsRegisterValidated = false;
             LastLoginTime=  TimeCreated = DateTime.Now;
            
        }
        public static DZMembership Create(enum_LoginType type)
        {
            switch (type)
            {
                case enum_LoginType.original:break;

                case enum_LoginType.WeChat:
                    DZMembership newUser = new DZMembershipWeChat();
                    return newUser;
                    
            }
            return null;
        }
        public virtual Guid Id { get; set; }
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
        public virtual string UserType { get; set; }

        
    }
    /// <summary>
    /// 商家相关用户.
    /// 
    /// </summary>
    [Obsolete]
    public class BusinessUser : DZMembership
    {
        /// <summary>
        /// 用户所属商家.
        /// </summary>
        public virtual Business BelongTo { get; set; }
    }
    public class Customer : DZMembership
    {
        /// <summary>
        /// 客户姓名
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual string Gender { get; set; }
         
        
    }
    public class CustomerService:DZMembership
    {
        /// <summary>
        /// 客服编号.
        /// </summary>
        public virtual string CSId { get; set; }
        
    }

    /// <summary>
    /// 微信用户相关数据
    /// </summary>
    public class DZMembershipWeChat : DZMembership
    {
        public virtual string AccessToken { get; set; }//接口调用凭证
        public virtual int ExpiresIn { get; set; }//access_token接口调用凭证超时时间，单位（秒）
        public virtual string RefreshToken { get; set; }//用户刷新access_token
        public virtual string OpenId { get; set; }//授权用户唯一标识
        public virtual string Scope { get; set; }//用户授权的作用域，使用逗号（,）分隔
        public virtual string Unionid { get; set; }//当且仅当该移动应用已获得该用户的userinfo授权时，才会出现该字段
        public virtual string Nickname { get; set; }//普通用户昵称
        public virtual int Sex { get; set; }//普通用户性别，1为男性，2为女性
        public virtual string Province { get; set; }//普通用户个人资料填写的省份
        public virtual string City { get; set; }//普通用户个人资料填写的城市
        public virtual string Country { get; set; }//国家，如中国为CN
        public virtual string Headimgurl { get; set; }//用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空
    }
}
