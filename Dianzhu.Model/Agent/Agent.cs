using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 代理商
    /// 代理商发展当地线下店铺
    /// </summary>
    public class Agent:DZMembership
    {
        /// <summary>
        /// 代理商代理的区域
        /// </summary>
        public virtual Area Area { get; set; }

        /// <summary>
        /// 发展的商家(只要是同一地区的商家即属于该代理)
        /// </summary>

        /// <summary>
        /// 分润
        /// </summary>
        public virtual decimal SharePoint { get; set; }
        /// <summary>
        /// 个人信息
        /// </summary>
        public virtual AgentMemberInfo MemberInfo { get; set; }


    }
    /// <summary>
    /// 代理商个人信息
    /// </summary>
    public class AgentMemberInfo : DDDCommon.Domain.Entity<Guid>
    {
        /// <summary>
        /// 真实姓名
        /// </summary>
        public virtual string RealName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual bool Sex { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public virtual string PersonalID { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public virtual string AvatarUrl { get; set; }
    }
}
