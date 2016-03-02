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
    
    public class ServiceOrderDetail
    {

        #region constructor
        public ServiceOrderDetail(DZService service)
        {
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
        }
        /// <summary>
        /// 系统外服务项
        /// </summary>
        /// <param name="ServiceName"></param>
        /// <param name="Description"></param>
        /// <param name="IsCompensationAdvance"></param>
        /// <param name="MinPrice"></param>
        /// <param name="UnitPrice"></param>
        /// <param name="ChargeUnit"></param>
        /// <param name="DepositAmount"></param>
        /// <param name="CancelCompensation"></param>
        /// <param name="OverTimeForCancel"></param>
        /// <param name="ServiceMode"></param>
        public ServiceOrderDetail(string ServiceName, string Description, bool IsCompensationAdvance,
          decimal MinPrice, decimal UnitPrice, enum_ChargeUnit ChargeUnit,decimal DepositAmount,
          decimal CancelCompensation,int OverTimeForCancel, enum_ServiceMode ServiceMode)
        {
          
            this.ServiceName = ServiceName;
            this.Description = Description;
            this.IsCompensationAdvance = IsCompensationAdvance;
            this.MinPrice = MinPrice;
            this.UnitPrice = UnitPrice;
            this.ChargeUnit = ChargeUnit;
            this.DepositAmount = DepositAmount;
            this.CancelCompensation = CancelCompensation;
            this.OverTimeForCancel = OverTimeForCancel;
            this.ServiceMode = ServiceMode;
        }
        #endregion

        #region properties
        public Guid Id { get; set; }
        /// <summary>
        /// 服务项
        /// </summary>
        public DZService OriginalService { get; set; }
        //screenshot of the service
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public virtual bool IsCompensationAdvance { get; set; }
        public virtual decimal MinPrice { get; set; }
        public virtual decimal UnitPrice { get; set; }
        public virtual enum_ChargeUnit ChargeUnit { get; set; }

        public virtual decimal DepositAmount { get; set; }
        public virtual decimal CancelCompensation { get; set; }
        public virtual int OverTimeForCancel { get; set; }
        public virtual enum_ServiceMode ServiceMode { get; set; }

        /// <summary>
        /// 购买数量
        /// </summary>
        public virtual decimal UnitAmount { get; set; }
        #endregion

        public decimal ServiceAmount
        {
            get {
                return UnitPrice * UnitAmount;
            }
        }
    }


    
}
