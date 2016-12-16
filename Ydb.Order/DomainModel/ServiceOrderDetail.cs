using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Common;
using System.Diagnostics;
 
namespace Ydb.Order.DomainModel
{
    /// <summary>
    /// 订单明细. 包含服务项快照
    /// </summary>
    
    public class ServiceOrderDetail
    {

        #region constructor
        public ServiceOrderDetail()
        {
            
        }
        public ServiceOrderDetail(string serviceId,ServiceSnapShot serviceSnapShot, WorkTimeSnapshot OpenTimeSnapShot,
         //   ServiceOpenTimeForDaySnapShotForOrder OpenTimeForDaySnapShot,
            int unitAmount,string targetCustomerName,string targetCustomerPhone, string targetAddress,DateTime targetTime,string memo)
        {
            OriginalServiceId= serviceId;
            this.ServiceSnapShot = serviceSnapShot;// service.GetServiceSnapShot();

           // this.OpenTimeSnapShot = OpenTimeForDaySnapShot ;//service.GetOpenTimeSnapShot(targetTime);
          this.ServiceOpentimeSnapshot = OpenTimeSnapShot;// service.GetServiceOpenTimeSnapshot(targetTime);

            this.UnitAmount = unitAmount ;
            this.TargetCustomerName = targetCustomerName;
            this.TargetCustomerPhone = targetCustomerPhone;
            this.TargetAddress = targetAddress;
            this.TargetTime = targetTime;
            this.Memo = memo;
        }

        #endregion

        #region 服务项
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 服务项
        /// </summary>
        public virtual string OriginalServiceId { get; set; }
        //screenshot of the service
        public virtual ServiceSnapShot ServiceSnapShot { get; protected set; }

        public virtual WorkTimeSnapshot ServiceOpentimeSnapshot { get; protected set; }
        #endregion

        
        #region 服务项需求
      
        /// <summary>
        /// 购买数量
        /// </summary>
        public virtual int UnitAmount { get; set; }
        /// <summary>
        /// 目标用户名称
        /// </summary>
        public virtual string TargetCustomerName { get; set; }
        /// <summary>
        /// 目标用户电话
        /// </summary>
        public virtual string TargetCustomerPhone { get; set; }
        /// <summary>
        /// 客户要求的服务地址
        /// </summary>
        public virtual string TargetAddress { get; set; }
        /// <summary>
        /// 服务预约时间
        /// </summary>
        public virtual DateTime TargetTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Memo { get; set; }

     
        #endregion
        /// <summary>
        /// 该服务分配的员工.
        /// </summary>
        public virtual IList<string> StaffIds { get; set; }
        /// <summary>
        /// 是否选择
        /// </summary>
        public virtual bool Selected { get; set; }

        public virtual decimal ServiceAmount
        {
            get {
                return ServiceSnapShot.UnitPrice * UnitAmount;
            }
        }
        //分配的职员
      
    }


    
}
