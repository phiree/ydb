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
       public virtual DateTime ServiceTimeBegin { get; set; }

       public virtual DateTime ServiceTimeEnd { get; set; }
       /// <summary>
       /// 每日接单总量
       /// </summary>
       public virtual int TotalOrdersPerDay { get; set; }

       /// <summary>
       /// 服务范围,商圈名称
       /// </summary>
       public virtual string ServiceScope { get; set; }

       public virtual PayType PayType { get; set; }

       /// <summary>
       /// 服务保障:是否先行赔付
       /// </summary>
       public virtual bool IsCompensationAdvance
       { get; set; }
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
       public virtual ChargeUnit ChargeUnit { get; set; }
       /// <summary>
       /// 一口价
       /// </summary>
       public virtual decimal FixedPrice { get; set; }

       /// <summary>
       /// 服务提供方式:上门, 不上门..等.
       /// </summary>
       public virtual ServiceMode ServiceMode { get; set; }

    }
}
