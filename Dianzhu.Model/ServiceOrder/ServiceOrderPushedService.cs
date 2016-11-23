using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Common;
using System.Diagnostics;

namespace Dianzhu.Model
{
    /// <summary>
    /// 被推送的服务
    /// </summary>

    public class ServiceOrderPushedService:DDDCommon.Domain.Entity<Guid>
    {

        #region constructor
        protected ServiceOrderPushedService()
        {

        }
        public ServiceOrderPushedService(ServiceOrder order,
            //todo: 应该去除不需要的属性.  
            string serviceId, 
            ServiceSnapShot serviceSnapshot,


            int unitAmount,string targetCustomerName,string targetCustomerPhone, string targetAddress, DateTime targetTime,string memo)
        {
            ServiceOrder = order;
            OriginalServiceId = serviceId;
            this.ServiceSnapShot = serviceSnapshot;
            this.UnitAmount = unitAmount;
            this.TargetCustomerName = targetCustomerName;
            this.TargetCustomerPhone = targetCustomerPhone;
            this.TargetAddress = targetAddress;
            this.TargetTime = targetTime;
            this.Memo = memo;
            this.Selected = false;
        }



        #endregion

       public virtual ServiceSnapShot ServiceSnapShot { get; set; }
        public virtual WorkTimeSnapshot ServiceOpenTimeSnapShot { get; set; }

        /// <summary>
        /// 对应的订单
        /// </summary>
        public virtual ServiceOrder ServiceOrder { get; set; }
        #region 服务项
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 服务项
        /// </summary>
        public virtual string OriginalServiceId { get; set; }
        //screenshot of the service
        
       
        #endregion

       

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

        /// <summary>
        /// 是否选择
        /// </summary>
        public virtual bool Selected { get; set; }

        public virtual decimal ServiceAmount
        {
            get
            {
                return ServiceSnapShot. UnitPrice * UnitAmount;
            }
        }
        //todo:refactor. 为了编译通过. 需要继续重构
        public virtual string ServiceTypeName { get; set; }
        public virtual string ServiceTypeId { get; set; }
        public virtual string ServiceTypeParentId { get; set; }

    }



}
