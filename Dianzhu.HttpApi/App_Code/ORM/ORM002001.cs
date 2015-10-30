﻿using System;
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
/// 获取用户的服务订单列表
/// </summary>
public class ResponseORM002001 : BaseResponse
{
    public ResponseORM002001(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataORM002001 requestData = this.request.ReqData.ToObject<ReqDataORM002001>();

 
        DZMembershipProvider p = new DZMembershipProvider();
        BLLReceptionStatus BLLReceptionStatus = new BLLReceptionStatus();
        BLLServiceOrder bllOrder = new BLLServiceOrder();
        string raw_id = requestData.userID;

        try
        {
            DZMembership member;
            bool validated = new Account(p).ValidateUser(new Guid(raw_id), requestData.pWord, this, out member);
            if (!validated)
            {
                return;
            }
            try
            {

                RespDataORM002001 respData = new RespDataORM002001();
                IMSessionsOpenfire imSession = new IMSessionsOpenfire(
              System.Configuration.ConfigurationManager.AppSettings.Get("OpenfireRestApiSessionListUrl"),
              System.Configuration.ConfigurationManager.AppSettings.Get("OpenfireRestApiAuthKey"));

                Dictionary<DZMembership, DZMembership> assignedPair =
                    new ReceptionAssigner(imSession)
                    .AssignCustomerLogin(member);
                if (assignedPair.Count == 0)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "没有在线客服";
                    return;
                }
                if (assignedPair.Count > 1)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "返回了多个客服";
                    return;
                }

                string reqOrderId = requestData.orderID;
                Guid order_ID;
                bool isValidGuid = Guid.TryParse(reqOrderId, out order_ID);
                bool hasOrder = false;
                bool needNewOrder = false;
                ServiceOrder orderToReturn = null;
                if (isValidGuid)
                {
                    orderToReturn = bllOrder.GetOne(order_ID);
                    if (orderToReturn != null)
                    {
                        if (orderToReturn.Customer.Id == member.Id)
                        {
                            hasOrder = true;
                        }
                        int orderTimeout = Convert.ToInt32(
                            System.Configuration.ConfigurationManager.AppSettings.Get("OrderTimeout"));
                        if ((DateTime.Now - orderToReturn.OrderCreated) > new TimeSpan(0, orderTimeout, 0))
                        {
                            needNewOrder = true;
                            this.err_Msg = "原订单已过期,已生成新订单";
                        }

                    }
                }

                if (!hasOrder||needNewOrder)
                {


                    /* string serviceName,string serviceBusinessName,string serviceDescription,decimal serviceUnitPrice,string serviceUrl,
               DZMembership member,
               string targetAddress, int unitAmount, decimal orderAmount*/
                      orderToReturn = ServiceOrder.Create(
                         enum_ServiceScopeType.OSIM
                       , string.Empty //serviceName
                       , string.Empty//serviceBusinessName
                       , string.Empty//serviceDescription
                       , 0//serviceUnitPrice
                       , string.Empty//serviceUrl
                       , member //member
                       , string.Empty
                       , 0
                       , 0);
                    orderToReturn.CustomerService = assignedPair[member];
                    bllOrder.SaveOrUpdate(orderToReturn);
                }
                respData.orderID = orderToReturn.Id.ToString();

                RespDataORM002001_cerObj cerObj = new RespDataORM002001_cerObj().Adap(assignedPair[member]);
                respData.cerObj = cerObj;
                this.RespData = respData;
                this.state_CODE = Dicts.StateCode[0];

            }
            catch (Exception ex)
            {
                this.state_CODE = Dicts.StateCode[2];
                this.err_Msg = ex.Message;
                return;
            }

        }
        catch (Exception e)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = e.Message;
            return;
        }

    }

}

public class ReqDataORM002001
{
    
    public string userID { get; set; }
    public string pWord { get; set; }
    public string orderID { get; set; }
    

}
public class RespDataORM002001
{
    public RespDataORM002001()
    {
        orderID = string.Empty;
    }
    public string orderID { get; set; }
    public  RespDataORM002001_cerObj cerObj{ get; set; }
 
}
public class RespDataORM002001_cerObj
{
    public string userID { get; set; }
    public string alias { get; set; }
    public string userName { get; set; }
    public string imgUrl { get; set; }
    public RespDataORM002001_cerObj Adap(DZMembership customerService)
    {
        this.userID = customerService.Id.ToString();
        this.imgUrl = string.Empty;
        this.alias = customerService.DisplayName;
        this.userName = customerService.UserName;
        return this;
    }
}

 


