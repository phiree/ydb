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
    public class DZMembershipWeChat : DZMembership
    {
        public virtual string AccessToken { get; set; }//接口调用凭证
        public virtual int ExpiresIn { get; set; }//access_token接口调用凭证超时时间，单位（秒）
        public virtual string RefreshToken { get; set; }//用户刷新access_token
        public virtual string WeChatOpenId { get; set; }//授权用户唯一标识
        public virtual string Scope { get; set; }//用户授权的作用域，使用逗号（,）分隔
        public virtual string Unionid { get; set; }//当且仅当该移动应用已获得该用户的userinfo授权时，才会出现该字段
        public virtual string Nickname { get; set; }//普通用户昵称
        //public virtual int Sex { get; set; }//普通用户性别，1为男性，2为女性
        public virtual string Province { get; set; }//普通用户个人资料填写的省份
        public virtual string City { get; set; }//普通用户个人资料填写的城市
        public virtual string Country { get; set; }//国家，如中国为CN
        public virtual string Headimgurl { get; set; }//用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空
    }

}
