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
    public RespDataORM_orderObj orderObj { get; set; }
    public RespDataORM_Order Adap(ServiceOrder order)
    {
        if (order != null)
        {
            this.orderObj = new RespDataORM_orderObj().Adap(order);
        }
        return this;
    }
}
public class RespDataORM_orderObj
{
    public string orderID { get; set; }
    public string title { get; set; }
    public string status { get; set; }
    public string startTime { get; set; }
    public string endTime { get; set; }
    public string exDoc { get; set; }
    public string money { get; set; }
    public string address { get; set; }
    public string km { get; set; }
    // public string paylink { get; set; }
    public RespDataORM_svcObj svcObj { get; set; }
    public RespDataORM_UserObj userObj { get; set; }
    public RespDataORM_storeObj storeObj { get; set; }    

    public RespDataORM_orderObj Adap(ServiceOrder order)
    {
        this.orderID = order.Id.ToString();
        this.title = order.ServiceName;
        this.status = order.OrderStatus.ToString();
        if (order.OrderCreated > DateTime.MinValue)
        {
            this.startTime = string.Format("{0:yyyyMMddHHmmss}", order.OrderCreated);
        }
        else
        {
            this.startTime = string.Empty;
        }
        if (order.OrderFinished > DateTime.MinValue)
        {
            this.endTime = string.Format("{0:yyyyMMddHHmmss}", order.OrderCreated);
        }
        else
        {
            this.endTime = string.Empty;
        }
        this.exDoc = order.ServiceDescription ?? string.Empty;
        this.money = order.OrderAmount.ToString("0.00");
        this.address = order.TargetAddress ?? string.Empty;
        this.km = string.Empty;

        if (order != null)
        {
            this.svcObj = new RespDataORM_svcObj().Adap(order);
        }
        if (order.Customer != null)
        {
            this.userObj = new RespDataORM_UserObj().Adap(order.Customer);
        }
        //todo,这里只能获取系统内订单
        if (order.Service != null)
        {
            this.storeObj = new RespDataORM_storeObj().Adap(order.Service.Business);
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
                 Dianzhu.Config.Config.GetAppSetting("MediaGetUrl")
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

public class RespDataORM_svcObj
{
    public string svcID { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public string startTime { get; set; }
    public string endTime { get; set; }
    

    public RespDataORM_svcObj Adap(ServiceOrder order)
    {
        this.svcID = order.Service != null ? order.Service.Id.ToString() : order.Id.ToString();
        this.name = order.Service != null ? order.Service.Name : order.ServiceName;
        this.type = order.Service != null ? order.Service.ServiceType.ToString() : string.Empty;
        if (order.OrderServerStartTime > DateTime.MinValue)
        {
            this.startTime = string.Format("{0:yyyyMMddHHmmss}", order.OrderServerStartTime);
        }
        else
        {
            this.startTime = string.Empty;
        }
        if (order.OrderServerFinishedTime > DateTime.MinValue)
        {
            this.endTime = string.Format("{0:yyyyMMddHHmmss}", order.OrderServerFinishedTime);
        }
        else
        {
            this.endTime = string.Empty;
        }

        return this;
    }
    
}


 


