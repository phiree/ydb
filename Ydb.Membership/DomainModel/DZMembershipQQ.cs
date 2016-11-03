using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Membership.DomainModel
{
  
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
        public virtual string Nickname { get; set; } //用户在QQ空间的昵称。
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

}
