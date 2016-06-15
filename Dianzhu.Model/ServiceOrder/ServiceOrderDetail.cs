﻿using System;
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
        public ServiceOrderDetail()
        {
            
        }
        public ServiceOrderDetail(DZService service,int unitAmount,string targetAddress,DateTime targetTime)
        {
            OriginalService = service;
            this.ServieSnapShot = service.GetServiceSnapShot();

            this.OpenTimeSnapShot = service.GetOpenTimeSnapShot(targetTime);
            this.UnitAmount = unitAmount ;
            this.TargetAddress = targetAddress;
            this.TargetTime = targetTime;
             
        }

        #endregion

        #region 服务项
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 服务项
        /// </summary>
        public virtual DZService OriginalService { get; set; }
        //screenshot of the service
        public virtual ServiceSnapShotForOrder ServieSnapShot { get; protected set; }
        #endregion

        #region 服务项需求
      
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

        //服务项时间设定的快照
        public virtual ServiceOpenTimeForDaySnapShotForOrder OpenTimeSnapShot { get; protected set; }

        #endregion
        /// <summary>
        /// 该服务分配的员工.
        /// </summary>
        public virtual IList<Staff> Staff { get; set; }
        /// <summary>
        /// 是否选择
        /// </summary>
        public virtual bool Selected { get; set; }

        public virtual decimal ServiceAmount
        {
            get {
                return ServieSnapShot.UnitPrice * UnitAmount;
            }
        }
        //分配的职员
      
    }


    
}
