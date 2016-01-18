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
                    DZMembership newUserWechat = new DZMembershipWeChat();
                    return newUserWechat;
                case enum_LoginType.SinaWeiBo:
                    DZMembership newUserSinaWeibo = new DZMembershipSinaWeibo();
                    return newUserSinaWeibo;
                case enum_LoginType.TencentQQ:
                    DZMembership newUserQQ = new DZMembershipQQ();
                    return newUserQQ;

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

    /// <summary>
    /// QQ用户相关数据
    /// </summary>
    public class DZMembershipQQ : DZMembership
    {
        public virtual string ClientId { get; set; } //appid
        public virtual string OpenId { get; set; }//openid是此网站上唯一对应用户身份的标识
        public virtual int Ret { get; set; }//返回码
        public virtual string Msg { get; set; }//如果ret<0，会有相应的错误信息提示，返回数据全部用UTF-8编码。
        public virtual int IsLost { get; set; }//判断是否有数据丢失。如果应用不使用cache，不需要关心此参数。0或者不返回：没有数据丢失，可以缓存。1：有部分数据丢失或错误，不要缓存。
        public virtual string NickName { get; set; } //用户在QQ空间的昵称。
        public virtual string Gender { get; set; } //性别。 如果获取不到则默认返回"男"
        public virtual string Province { get; set; } //省（当pf=qzone、pengyou或qplus时返回）。
        public virtual string City { get; set; } //性市（当pf=qzone、pengyou或qplus时返回）。
        public virtual string Year { get; set; } //出生年份
        public virtual string Figureurl { get; set; } //大小为30×30像素的QQ空间头像URL。
        public virtual string Figureurl1 { get; set; } //大小为50×50像素的QQ空间头像URL。
        public virtual string Figureurl2 { get; set; } //大小为100×100像素的QQ空间头像URL。
        public virtual string FigureurlQq1 { get; set; } //大小为40×40像素的QQ头像URL。
        public virtual string FigureurlQq2 { get; set; } //大小为100×100像素的QQ头像URL。需要注意，不是所有的用户都拥有QQ的100x100的头像，但40x40像素则是一定会有。    
        public virtual string IsYellowVip { get; set; } //标识用户是否为黄钻用户（0：不是；1：是）。
        public virtual string Vip { get; set; } //标识用户是否为黄钻用户（0：不是；1：是）
        public virtual string YellowVipLevel { get; set; } //黄钻等级
        public virtual string Level { get; set; } //黄钻等级。
        public virtual string IsYellowYearVip { get; set; } //标识是否为年费黄钻用户（0：不是； 1：是）
    }

    /// <summary>
    /// 新浪微博用户相关数据
    /// </summary>
    public class DZMembershipSinaWeibo : DZMembership
    {
        public virtual string AccessToken { get; set; }//用于调用access_token，接口获取授权后的access token。
        public virtual string ExpiresId { get; set; }//access_token的生命周期，单位是秒数。
        public virtual string RefreshToken { get; set; }//刷新access_token
        public virtual string Scope { get; set; }//用户授权的scope权限。
        public virtual string CreateAt { get; set; }//access_token的创建时间，从1970年到创建时间的秒数。
        public virtual long UId { get; set; }//用户UID
        public virtual string UIdstr { get; set; }//字符串型的用户UID
        public virtual string ScreenName { get; set; }//用户昵称
        public virtual string Name { get; set; }//友好显示名称
        public virtual int Province { get; set; }//用户所在省级ID
        public virtual int City { get; set; }//用户所在城市ID
        public virtual string Location { get; set; }//用户所在地
        public virtual string Description { get; set; }//用户个人描述
        public virtual string Url { get; set; }//用户博客地址
        public virtual string ProfileImageUrl { get; set; }//用户头像地址（中图），50×50像素
        public virtual string ProfileUrl { get; set; }//用户的微博统一URL地址
        public virtual string Domain { get; set; }//用户的个性化域名
        public virtual string WeiHao { get; set; }//用户的微号
        public virtual string Gender { get; set; }//性别，m：男、f：女、n：未知
        public virtual int FollowersCount { get; set; }//粉丝数
        public virtual int FriendsCount { get; set; }//关注数
        public virtual int StatusesCount { get; set; }//微博数
        public virtual int FavouritesCount { get; set; }//收藏数
        public virtual string CreatedAt { get; set; }//用户创建（注册）时间
        public virtual bool Following { get; set; }//暂未支持
        public virtual bool AllowAllActMsg { get; set; }//是否允许所有人给我发私信，true：是，false：否
        public virtual bool GeoEnabled { get; set; }//是否允许标识用户的地理位置，true：是，false：否
        public virtual bool Verified { get; set; }//是否是微博认证用户，即加V用户，true：是，false：否
        public virtual int VerifiedType { get; set; }//暂未支持
        public virtual string Remark { get; set; }//用户备注信息，只有在查询用户关系时才返回此字段
        public virtual bool AllowAllComment { get; set; }//是否允许所有人对我的微博进行评论，true：是，false：否
        public virtual string AvatarLarge { get; set; }//用户头像地址（大图），180×180像素
        public virtual string AvatarHd { get; set; }//用户头像地址（高清），高清头像原图
        public virtual string VerifiedReason { get; set; }//认证原因
        public virtual bool FollowMe { get; set; }//该用户是否关注当前登录用户，true：是，false：否
        public virtual int OnlineStatus { get; set; }//用户的在线状态，0：不在线、1：在线
        public virtual int BiFollowersCount { get; set; }//用户的互粉数
        public virtual string Lang { get; set; }//用户当前的语言版本，zh-cn：简体中文，zh-tw：繁体中文，en：英语
    }
}
