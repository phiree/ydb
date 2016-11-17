using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using Ydb.Common;

namespace Dianzhu.Model
{
    /// <summary>
    /// 订单明细. 包含服务项快照
    /// </summary>
    
    public class ServiceOrderAppraise:DDDCommon.Domain.Entity<Guid>
    {

        #region constructor
        protected ServiceOrderAppraise()
        {
            
        }
        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="order"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <param name="content"></param>
        public ServiceOrderAppraise(ServiceOrder order, enum_ChatTarget target,decimal value,string content)
        {
            this.CreateTime = DateTime.Now;
            this.Order = order;
            this.Tatget = target;
            this.Value = value;
            this.Content = content;
        }
        #endregion
        
        /// <summary>
        /// 评价时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 订单
        /// </summary>
        public virtual ServiceOrder Order { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public virtual enum_ChatTarget Tatget { get; set; }
        /// <summary>
        /// 评价值
        /// </summary>
        public virtual decimal Value { get; set; }
        /// <summary>
        /// 评价内容
        /// </summary>
        public virtual string Content { get; set; }
    }


    
}
