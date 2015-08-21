using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model.Enums;
namespace Dianzhu.Model
{
    /// <summary>
    /// 具体某项服务的定义
    /// </summary>
   public class DZService
    {
       public DZService()
       {
           PropertyValues = new List<ServicePropertyValue>();
           OpenTimes = new List<ServiceOpenTime>();
           
       }
       public virtual Guid Id { get; set; }
       /// <summary>
       /// 服务项目所属类别
       /// </summary>
       public virtual ServiceType ServiceType { get; set; }
       /// <summary>
       /// 服务名称
       /// </summary>
       public virtual string Name { get; set; }
       /// <summary>
       /// 所属商家
       /// </summary>
       public virtual Business Business { get; set; }
       /// <summary>
       /// 商圈代码.
       /// </summary>
       public virtual string BusinessAreaCode { get; set; }
       /// <summary>
       /// 详细描述
       /// </summary>
       public virtual string Description { get; set; }
       /// <summary>
       /// 类别属性的值.
       /// </summary>
       public virtual IList<ServicePropertyValue> PropertyValues { get; set; }
       /// <summary>
       /// 服务开始时间?如果是多段
       /// </summary>
       public virtual string ServiceTimeBegin { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public virtual string ServiceTimeEnd { get; set; }
       /// <summary>
       /// 每日接单总量
       /// </summary>
       public virtual int MaxOrdersPerDay { get; set; }
       /// <summary>
       /// 单位时间内(小时)最大接单量
       /// </summary>
       public virtual int MaxOrdersPerHour { get; set; }

       

       public virtual PayType PayType { get; set; }

       /// <summary>
       /// 服务保障:是否先行赔付
       /// </summary>
       public virtual bool IsCompensationAdvance
       { get; set; }
       /// <summary>
       /// 服务准备时长(提前多长时间预约).单位分钟.
       /// </summary>
       public virtual int OrderDelay { get; set; }
       /// <summary>
       /// 是否可以对公. 否:只能为私人提供
       /// </summary>
       public virtual bool IsForBusiness { get; set; }
       /// <summary>
       /// 是否通过平台标准认证"
       /// </summary>
       public virtual bool IsCertificated { get; set; }
       /// <summary>
       /// 最低服务费
       /// </summary>
       public virtual decimal MinPrice { get; set; }
       /// <summary>
       /// 单位时间费用 25/小时.
       /// </summary>
       public virtual decimal UnitPrice { get; set; }
       /// <summary>
       /// 计费单位: 小时, 天,次等
       /// </summary>
       public virtual enum_ChargeUnit ChargeUnit { get; set; }
       /// <summary>
       /// 一口价
       /// </summary>
       public virtual decimal FixedPrice { get; set; }

       /// <summary>
       /// 服务提供方式:上门, 不上门..等.
       /// </summary>
       public virtual enum_ServiceMode ServiceMode { get; set; }

       public virtual DateTime CreatedTime { get; set; }
       public virtual DateTime LastModifiedTime { get; set; }

       public virtual bool Enabled { get; set; }
       public virtual IList<ServiceOpenTime> OpenTimes { get; set; }
       public virtual bool AddOpenTime(ServiceOpenTime openTime, out string errMsg)
       {
           errMsg = string.Empty;
           if (openTime.Day < 1 || openTime.Day > 7)
           {
               errMsg = "星期数有误,只能是1到7";
               return false;

           }
           if (OpenTimes.Any(x => x.Day == openTime.Day))
           {
               errMsg = "已经定义了星期" + openTime.Day + "的时间";
               return false;
           }
           return true;
       }
    }
}
