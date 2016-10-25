using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Membership.DomainModel
{
    /// <summary>
    /// 微信用户相关数据
    /// </summary>
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
