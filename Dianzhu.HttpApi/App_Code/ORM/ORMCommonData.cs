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
    public string alias { get; set; }
    public string merID { get; set; }
    public string type { get; set; }
    public string startTime { get; set; }
    public string endTime { get; set; }
    public string money { get; set; }
    public string status { get; set; }
    public string address { get; set; }
    public string exDoc { get; set; }
    public RespDataORM_UserObj userObj { get; set; }
    public RespDataORM_MerObj merObj { get; set; }
    public RespDataORM_Order Adap(ServiceOrder order)
    {
        //todo: 如果是外部订单?

        this.orderID = order.Id.ToString();
    
         
        this.alias = order.ServiceName;
        this.merID = order.Service !=null? order.Service.Business.Id.ToString():string.Empty;
        this.type =  order.Service !=null?order.Service.ServiceType.ToString():string.Empty;
        this.startTime = order.Service != null ? order.Service.ServiceTimeBegin : string.Empty;
        this.endTime = order.Service !=null?order.Service.ServiceTimeEnd : string.Empty;
        ///这个是服务单价
        this.money = order.ServiceUnitPrice.ToString("#.#");
        this.status = order.OrderStatus.ToString();
        this.address = order.TargetAddress ?? string.Empty;
        this.exDoc = order.ServiceDescription ?? string.Empty;
        if (order.Customer != null)
        {
            this.userObj = new RespDataORM_UserObj().Adap(order.Customer);
        }
        //todo,这里只能获取系统内订单
        if (order.Service != null)
        {
            this.merObj = new RespDataORM_MerObj().Adap(order.Service.Business);
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
        this.imgUrl = "需要增加这个属性";
        return this;
    }
    
}
public class RespDataORM_MerObj
{
    public string merID { get; set; }
    public string alias { get; set; }
    public string imgUrl { get; set; }
    public RespDataORM_MerObj Adap(Business business)
    {
        this.merID = business.Id.ToString();
        this.alias = business.Name;
        this.imgUrl = business.BusinessAvatar.ImageName;
        return this;
    }
}


