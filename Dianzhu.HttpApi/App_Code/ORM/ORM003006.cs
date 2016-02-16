using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;

/// <summary>
/// 获取一条服务信息的详情
/// </summary>
public class ResponseORM003006 : BaseResponse
{
    public ResponseORM003006(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataORM003006 requestData = this.request.ReqData.ToObject<ReqDataORM003006>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
        BLLServiceOrder bllServiceOrder = new BLLServiceOrder();
        BLLServiceOrderStateChangeHis bllServiceOrderHis = new BLLServiceOrderStateChangeHis();
        string raw_id = requestData.userID;

        try
        {
            Guid userId,orderId;
            bool isUserId = Guid.TryParse(requestData.userID, out userId);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "用户Id格式有误";
                return;
            }

            bool isOrderId = Guid.TryParse(requestData.orderID, out orderId);
            if (!isOrderId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "用户orderId格式有误";
                return;
            }

            DZMembership member;
            bool validated = new Account(p).ValidateUser(new Guid(raw_id), requestData.pWord, this, out member);
            if (!validated)
            {
                return;
            }
            try
            {
                ServiceOrder order = bllServiceOrder.GetOne(new Guid(requestData.orderID));
                if (order == null)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "没有对应的订单,请检查传入的orderID";
                    return;
                }

                List<RespDataORM_orderStatusObj> RespOrderStatusList = new List<RespDataORM_orderStatusObj>();

                RespDataORM_orderStatusObj orderStatusObj = null;
                orderStatusObj = new RespDataORM_orderStatusObj();
                orderStatusObj.status = enum_OrderStatus.Draft.ToString();
                orderStatusObj.time = string.Format("{0:yyyyMMddHHmmss}", order.OrderCreated);
                orderStatusObj.lastStatus = "";
                RespOrderStatusList.Add(orderStatusObj);

                IList< ServiceOrderStateChangeHis> OrderStateHisList = bllServiceOrderHis.GetOrderHisList(order);
                if (OrderStateHisList.Count > 0)
                {
                    
                    for (int i = 0; i < OrderStateHisList.Count; i++)
                    {
                        if (OrderStateHisList[i].Status == enum_OrderStatus.Created)
                        {
                            orderStatusObj = new RespDataORM_orderStatusObj().Adap(OrderStateHisList[i], null);
                        }
                        else
                        {
                            orderStatusObj = new RespDataORM_orderStatusObj().Adap(OrderStateHisList[i], OrderStateHisList[i - 1]);
                        }
                        RespOrderStatusList.Add(orderStatusObj);
                    }
                }

                RespDataORM003006 respData = new RespDataORM003006();

                respData.AdapList(RespOrderStatusList);

                this.state_CODE = Dicts.StateCode[0];
                this.RespData = respData;
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

public class ReqDataORM003006
{
    public string userID { get; set; }
    public string pWord { get; set; }
    public string orderID { get; set; }
}

public class RespDataORM003006
{
    public string orderID { get; set; }
    public IList<RespDataORM_orderStatusObj> arrayData { get; set; }
 
    public RespDataORM003006()
    {
        arrayData = new List<RespDataORM_orderStatusObj>();
    }

    public void AdapList(IList<RespDataORM_orderStatusObj> serviceOrderList)
    {
        foreach (RespDataORM_orderStatusObj order in serviceOrderList)
        {
            arrayData.Add(order);
        }
    }
}


