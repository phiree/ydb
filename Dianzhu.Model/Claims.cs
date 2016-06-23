using Dianzhu.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{
    /// <summary>
    /// 理赔
    /// </summary>
    public class Claims:DDDCommon.Domain.Entity<Guid>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        protected Claims()
        {
            
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="order"></param>
        /// <param name="status"></param>
        /// <param name="applicant"></param>
        public Claims(ServiceOrder order,enum_OrderStatus status,DZMembership applicant)
        {
            this.CreatTime = this.LastUpdateTime = DateTime.Now;

            this.Order = order;
            this.Status = status;
            this.Applicant = applicant;

            ClaimsDatailsList = new List<ClaimsDetails>();
        }

        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 订单id
        /// </summary>
        public virtual ServiceOrder Order { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreatTime { get; set; }
        /// <summary>
        /// 本次理赔的订单所处的订单状态
        /// </summary>
        public virtual enum_OrderStatus Status { get; set; }
        /// <summary>
        /// 操作人员
        /// </summary>
        public virtual DZMembership Applicant { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public virtual DateTime LastUpdateTime { get; set; }
        /// <summary>
        /// 理赔详情
        /// </summary>
        public virtual IList<ClaimsDetails> ClaimsDatailsList { get; protected set; }
        #endregion

        /// <summary>
        /// 增加理赔详情
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="context"></param>
        /// <param name="amount"></param>
        /// <param name="resourcesUrl"></param>
        /// <param name="target"></param>
        /// <param name="member"></param>
        public virtual void AddDetailsFromClaims(Claims claims, string context, decimal amount, IList<string> resourcesUrl, enum_ChatTarget target, DZMembership member)
        {
            ClaimsDetails claimsDetails = new ClaimsDetails(claims, context, amount, resourcesUrl, target, member);
            ClaimsDatailsList.Add(claimsDetails);
        }
    }
}
