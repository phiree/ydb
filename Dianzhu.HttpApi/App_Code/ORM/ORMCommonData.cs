using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
/// <summary>
 ///orm接口 公用的类
/// </summary>
public class RespDataORM_Order
{
   
    public string orderID { get; set; }
    public string title { get; set; }
    public string type { get; set; }
    public string startTime { get; set; }
    public string endTime { get; set; }
    public string money { get; set; }
    public string status { get; set; }
    public string address { get; set; }
    public string exDoc { get; set; }
   // public string paylink { get; set; }
    public string km { get; set; }
    public RespDataORM_UserObj userObj { get; set; }
    public RespDataORM_storeObj storeObj { get; set; }
    public RespDataORM002001_cerObj cerObj { get; set; }
    public RespDataORM_Order Adap(ServiceOrder order)
    {
        //todo: 如果是外部订单?
        this.km = string.Empty;
        this.orderID = order.Id.ToString();
    
         
        this.title = order.ServiceName;
         this.type =  order.Service !=null?order.Service.ServiceType.ToString():string.Empty;
        this.startTime = order.Service != null ? order.Service.ServiceTimeBegin : string.Empty;
        this.endTime = order.Service !=null?order.Service.ServiceTimeEnd : string.Empty;
        ///这个是服务单价
        this.money = order.OrderAmount.ToString("0.00");
        this.status = order.OrderStatus.ToString();
        this.address = order.TargetAddress ?? string.Empty;
        this.exDoc = order.ServiceDescription ?? string.Empty;
      //  this.paylink = order.BuildPayLink(System.Configuration.ConfigurationManager.AppSettings["PayServer"]);
        if (order.Customer != null)
        {
            this.userObj = new RespDataORM_UserObj().Adap(order.Customer);
        }
        //todo,这里只能获取系统内订单
        if (order.Service != null)
        {
            this.storeObj = new RespDataORM_storeObj().Adap(order.Service.Business);
        }
        if (order.CustomerService != null)
        {
            this.cerObj = new RespDataORM002001_cerObj().Adap(order.CustomerService);
        }
        return this;
    }
}
public class RespDataORM_UserObj
{
    public string userID { get; set; }
    public string alias { get; set; }
    public string imgUrl { get; set; }
    public RespDataORM_UserObj Adap(DZMembership member)
    {
        this.userID = member.Id.ToString();
        this.alias = member.NickName;
        this.imgUrl = string.IsNullOrEmpty(member.AvatarUrl) ? string.Empty
                : (
                  System.Configuration.ConfigurationManager.AppSettings["MediaGetUrl"]
                + member.AvatarUrl);
        return this;
    }
    
}
public class RespDataORM_storeObj
{
    public string storeID { get; set; }
    public string alias { get; set; }
    public string imgUrl { get; set; }
    public RespDataORM_storeObj Adap(Business business)
    {
        this.storeID = business.Id.ToString();
        this.alias = business.Name;
        this.imgUrl = business.BusinessAvatar.ImageName;
        return this;
    }
}


 


