using Dianzhu.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 投诉类
    /// </summary>
    public class ClaimsDetails
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        protected ClaimsDetails()
        {
            
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="context"></param>
        /// <param name="amount"></param>
        /// <param name="resourcesUrl"></param>
        /// <param name="target"></param>
        /// <param name="member">提交理赔的用户</param>
        public ClaimsDetails(Claims claims, string context,decimal amount,string resourcesUrl,enum_ChatTarget target,DZMembership member)
        {
            this.CreatTime = this.LastUpdateTime = DateTime.Now;

            this.Claims = claims;
            this.Context = context;
            this.Amount = amount;
            this.ResourcesUrl = resourcesUrl;
            this.Target = target;
            this.Member = member;
        }

        /// <summary>
        /// 主键
        /// </summary>
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 订单id
        /// </summary>
        public virtual Claims Claims { get; set; }
        /// <summary>
        /// 投诉内容
        /// </summary>
        public virtual string Context { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public virtual decimal Amount { get; set; }
        /// <summary>
        /// 投诉的图片链接
        /// </summary>
        public virtual string ResourcesUrl { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreatTime { get; set; }
        /// <summary>
        /// 提交本次理赔的目标
        /// </summary>
        public virtual enum_ChatTarget Target { get; set; }        
        /// <summary>
        /// 操作人员
        /// </summary>
        public virtual DZMembership Member { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public virtual DateTime LastUpdateTime { get; set; }
    }
}
