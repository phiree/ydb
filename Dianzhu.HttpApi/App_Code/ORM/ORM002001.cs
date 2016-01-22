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
/// 获取用户的服务订单列表
/// </summary>

public class ResponseORM002001 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");
    public ResponseORM002001(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataORM002001 requestData = this.request.ReqData.ToObject<ReqDataORM002001>();

 
        DZMembershipProvider p = new DZMembershipProvider();
        BLLReceptionStatus bllReceptionStatus = new BLLReceptionStatus();
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
            
                ilog.Debug("1");
                RespDataORM002001 respData = new RespDataORM002001();
                IIMSession imSession = new IMSessionsDB();
                ilog.Debug("2");
                ReceptionAssigner ra = new ReceptionAssigner(imSession);
                if (!string.IsNullOrEmpty(requestData.manualAssignedCsId))
                {
                    Guid mcsid = Guid.Empty;
                    bool isGuid = Guid.TryParse(requestData.manualAssignedCsId, out mcsid);
                    if (isGuid)
                    {
                        IAssignStratage ias = new AssignStratageManually(mcsid);
                        ra = new ReceptionAssigner(ias, imSession);
                    }
                }
                ilog.Debug("3");
                Dictionary<DZMembership, DZMembership> assignedPair = ra.AssignCustomerLogin(member);
                if (assignedPair.Count == 0)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "没有在线客服";
                    return;
                }
                ilog.Debug("4");
                if (assignedPair.Count > 1)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "返回了多个客服";
                    return;
                }
                ilog.Debug("5");
                string reqOrderId = requestData.orderID;
                Guid order_ID;
                if (reqOrderId != "")
                {
                    bool isValidGuid = Guid.TryParse(reqOrderId, out order_ID);
                    if (!isValidGuid)
                    {
                        this.state_CODE = Dicts.StateCode[1];
                        this.err_Msg = "OrderId格式有误";
                        return;
                    }
                }
                ilog.Debug("6");
                //bool hasOrder = false;
                //bool needNewOrder = false;
                ServiceOrder orderToReturn = null;
                //if (isValidGuid)
                //{
                orderToReturn = bllOrder.GetDraftOrder(member, assignedPair[member]);
                    if (orderToReturn == null)
                    {
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
                //}
                ilog.Debug("7");
                // if (!hasOrder||needNewOrder)
                // {
                //     /* string serviceName,string serviceBusinessName,string serviceDescription,decimal serviceUnitPrice,string serviceUrl,
                //DZMembership member,
                //string targetAddress, int unitAmount, decimal orderAmount*/
                //       orderToReturn = ServiceOrder.Create(
                //          enum_ServiceScopeType.OSIM
                //        , string.Empty //serviceName
                //        , string.Empty//serviceBusinessName
                //        , string.Empty//serviceDescription
                //        , 0//serviceUnitPrice
                //        , string.Empty//serviceUrl
                //        , member //member
                //        , string.Empty
                //        , 0
                //        , 0);
                //     orderToReturn.CustomerService = assignedPair[member];
                //     bllOrder.SaveOrUpdate(orderToReturn);
                // }
                respData.orderID = orderToReturn.Id.ToString();
                ilog.Debug("8");
                //更新 ReceptionStatus 中订单
                bllReceptionStatus.UpdateOrder(member, assignedPair[member], orderToReturn);
                ilog.Debug("9");
                RespDataORM002001_cerObj cerObj = new RespDataORM002001_cerObj().Adap(assignedPair[member]);
                ilog.Debug("10");
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
    public string manualAssignedCsId { get; set; }
    

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
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");
    public string userID { get; set; }
    public string alias { get; set; }
    public string userName { get; set; }
    public string imgUrl { get; set; }
    public RespDataORM002001_cerObj Adap(DZMembership customerService)
    {
        ilog.Debug("11");
        this.userID = customerService.Id.ToString();
        ilog.Debug("12");
        this.imgUrl = string.Empty;
        ilog.Debug("13");
        this.alias = customerService.DisplayName;
        ilog.Debug("14");
        this.userName = customerService.UserName;
        ilog.Debug("15");
        return this;
    }
}

 


