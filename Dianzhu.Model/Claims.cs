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
    public class Claims
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
        /// <param name="context"></param>
        /// <param name="amount"></param>
        /// <param name="resourcesUrl"></param>
        /// <param name="status"></param>
        /// <param name="target"></param>
        /// <param name="reuslt"></param>
        public Claims(ServiceOrder order,string context,decimal amount,string resourcesUrl,enum_OrderStatus status,enum_ChatTarget target,string reuslt)
        {
            this.CreatTime = this.LastUpdateTime = DateTime.Now;

            this.Order = order;
            this.Context = context;
            this.Amount = amount;
            this.ResourcesUrl = resourcesUrl;
            this.Status = status;
            this.Target = target;
            this.Result = reuslt;
        }

        /// <summary>
        /// 主键
        /// </summary>
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 订单id
        /// </summary>
        public virtual ServiceOrder Order { get; set; }
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
        /// 本次理赔的订单所处的订单状态
        /// </summary>
        public virtual enum_OrderStatus Status { get; set; }
        /// <summary>
        /// 提交本次理赔的目标
        /// </summary>
        public virtual enum_ChatTarget Target { get; set; }
        /// <summary>
        /// 处理结果
        /// </summary>
        public virtual string Result { get; set; }
        /// <summary>
        /// 操作人员
        /// </summary>
        public virtual DZMembership Operator { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public virtual DateTime LastUpdateTime { get; set; }
    }
}
