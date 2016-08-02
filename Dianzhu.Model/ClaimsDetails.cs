using Dianzhu.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 理赔详情
    /// </summary>
    public class ClaimsDetails : DDDCommon.Domain.Entity<Guid>
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
        public ClaimsDetails(Claims claims, string context,decimal amount,IList<string> resourcesUrl,enum_ChatTarget target,DZMembership member)
        {
            this.CreatTime = this.LastUpdateTime = DateTime.Now;

            this.Claims = claims;
            this.Context = context;
            this.Amount = amount;
            this.ClaimsDetailsResourcesUrl = resourcesUrl;
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
        /// 内容
        /// </summary>
        public virtual string Context { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public virtual decimal Amount { get; set; }
        /// <summary>
        /// 图片链接
        /// </summary>
        /// 20160622_longphui_modify
        ///public virtual string ResourcesUrl { get; set; }
        public virtual IList<string> ClaimsDetailsResourcesUrl { get; set; }
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
