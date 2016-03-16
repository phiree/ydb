using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model.Enums;
using System.Diagnostics;

namespace Dianzhu.Model
{
    /// <summary>
    /// 订单明细. 包含服务项快照
    /// </summary>

    public class ServiceOrderPushedService
    {

        #region constructor
        protected ServiceOrderPushedService()
        {

        }
        public ServiceOrderPushedService(ServiceOrder order, DZService service, int unitAmount, string targetAddress, DateTime targetTime)
        {
            ServiceOrder = order;
            OriginalService = service;
            ServiceName = service.Name;
            Description = service.Description;
            IsCompensationAdvance = service.IsCompensationAdvance;
            MinPrice = service.MinPrice;
            UnitPrice = service.UnitPrice;
            ChargeUnit = service.ChargeUnit;
            DepositAmount = service.DepositAmount;
            CancelCompensation = service.CancelCompensation;
            OverTimeForCancel = service.OverTimeForCancel;
            ServiceMode = service.ServiceMode;

            this.UnitAmount = unitAmount;
            this.TargetAddress = targetAddress;
            this.TargetTime = targetTime;
            this.Selected = false;
        }

        #endregion
        /// <summary>
        /// 对应的订单
        /// </summary>
        public virtual ServiceOrder ServiceOrder { get; set; }
        #region 服务项
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 服务项
        /// </summary>
        public virtual DZService OriginalService { get; set; }
        //screenshot of the service
        public virtual string ServiceName { get; set; }
        public virtual string Description { get; set; }
        public virtual bool IsCompensationAdvance { get; set; }
        public virtual decimal MinPrice { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual enum_ChargeUnit ChargeUnit { get; set; }

        public virtual decimal DepositAmount { get; set; }
        public virtual decimal CancelCompensation { get; set; }
        public virtual int OverTimeForCancel { get; set; }
        public virtual enum_ServiceMode ServiceMode { get; set; }
        #endregion

       

        /// <summary>
        /// 购买数量
        /// </summary>
        public virtual int UnitAmount { get; set; }
        /// <summary>
        /// 客户要求的服务地址
        /// </summary>
        public virtual string TargetAddress { get; set; }
        /// <summary>
        /// 服务预约时间
        /// </summary>
        public virtual DateTime TargetTime { get; set; }
 
        /// <summary>
        /// 是否选择
        /// </summary>
        public virtual bool Selected { get; set; }

        public virtual decimal ServiceAmount
        {
            get
            {
                return UnitPrice * UnitAmount;
            }
        }
        //分配的职员

    }



}
