using Dianzhu.Model.Enums;
using System;

namespace Dianzhu.Model
{
    /// <summary>
    /// 订单状态变化记录
    /// </summary>
    public class ServiceOrderStateChangeHis:DDDCommon.Domain.Entity<Guid>
    {
        /// <summary>
        /// 构造
        /// </summary>
        protected ServiceOrderStateChangeHis()
        {

        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="order"></param>
        /// <param name="oldStatus"></param>
        /// <param name="num"></param>
        public ServiceOrderStateChangeHis(ServiceOrder order, enum_OrderStatus oldStatus,int num)
        {
            this.Order = order;
            this.OldStatus = oldStatus;
            this.NewStatus = order.OrderStatus;
            this.OrderAmount = order.OrderAmount;
            this.DepositAmount = order.DepositAmount;
            this.NegotiateAmount = order.NegotiateAmount;
            this.Remark = order.Memo;
            this.CreatTime = DateTime.Now;
            //this.Controller=
            this.Number = num;
        }
        /// <summary>
        /// 主键
        /// </summary>
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 订单
        /// </summary>
        public virtual ServiceOrder Order { get; set; }
        /// <summary>
        /// 订单上一次状态
        /// </summary>
        public virtual enum_OrderStatus OldStatus { get; set; }
        /// <summary>
        /// 订单现在的状态
        /// </summary>
        public virtual enum_OrderStatus NewStatus { get; set; }
        /// <summary>
        ///订单预期
        /// </summary>
        public virtual decimal OrderAmount { get; set; }
        /// <summary>
        /// 订金
        /// </summary>
        public virtual decimal DepositAmount { get; set; }
        /// <summary>
        /// 协商总价
        /// </summary>
        public virtual decimal NegotiateAmount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 保存时间
        /// </summary>
        public virtual DateTime CreatTime { get; set; }
        /// <summary>
        /// 订单操作人员
        /// </summary>
        public virtual string ControllerId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public virtual int Number { get; set; }
        #region 临时数据，不需存储
        /// <summary>
        /// 上个状态中文名
        /// </summary>
        public virtual string OldStatusStr { get; set; }
        /// <summary>
        /// 上个状态中文内容
        /// </summary>
        public virtual string OldStatusCon { get; set; }
        /// <summary>
        /// 当前状态中文名
        /// </summary>
        public virtual string NewStatusStr { get; set; }
        /// <summary>
        /// 当前状态中文内容
        /// </summary>
        public virtual string NewStatusCon { get; set; }
        #endregion
    }



}
