using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.Api.Model;
using Newtonsoft.Json;

/// <summary>
/// 删除员工
/// </summary>
public class ResponseASN002004 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseASN002004(BaseRequest request) : base(request) { }
    public IBLLServiceOrder bllServiceOrder { get; set; }
    protected override void BuildRespData()
    {
        ReqDataASN002004 requestData = this.request.ReqData.ToObject<ReqDataASN002004>();

        //todo:用户验证的复用.
        DZMembershipProvider p = Installer.Container.Resolve<DZMembershipProvider>();
        BLLBusiness bllBusiness = Installer.Container.Resolve<BLLBusiness>(); BLLStaff bllStaff = new BLLStaff();
         BLLOrderAssignment bllOrderAssignment = new BLLOrderAssignment();

        try
        {
            string merchant_id = requestData.merchantID;
            string store_id = requestData.storeID;
            string user_id = requestData.userID;
            string order_id = requestData.orderID;

            Guid merchantID, storeID;
            bool isMerchantId = Guid.TryParse(merchant_id, out merchantID);
            if (!isMerchantId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "merchantID格式有误";
                return;
            }

            bool isStoreId = Guid.TryParse(store_id, out storeID);
            if (!isStoreId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "storeID格式有误";
                return;
            }

            if (request.NeedAuthenticate)
            {
                DZMembership member;
                bool validated = new Account(p).ValidateUser(merchantID, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                } 
            }
            try
            {
                Business store = bllBusiness.GetOne(storeID);
                if (store == null)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该店铺不存在！";
                    return;
                }

                if (store.Owner.Id != merchantID)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "您没有该店铺！";
                    return;
                }

                IList<RespDataASN_assignObj> arrayData = new List<RespDataASN_assignObj>();                

                if (!string.IsNullOrEmpty(user_id))
                {
                    Guid userId;
                    bool isUserId = Guid.TryParse(user_id, out userId);
                    if (!isUserId)
                    {
                        this.state_CODE = Dicts.StateCode[1];
                        this.err_Msg = "userId格式有误";
                        return;
                    }
                    
                    Staff staff = bllStaff.GetOne(userId);
                    if (staff == null)
                    {
                        this.state_CODE = Dicts.StateCode[1];
                        this.err_Msg = "不存在该员工！";
                        return;
                    }

                    if (!string.IsNullOrEmpty(order_id))
                    {
                        Guid orderId;
                        bool isOrderId = Guid.TryParse(order_id, out orderId);
                        if (!isOrderId)
                        {
                            this.state_CODE = Dicts.StateCode[1];
                            this.err_Msg = "orderId格式有误";
                            return;
                        }

                        ServiceOrder order = bllServiceOrder.GetOne(orderId);
                        if (order == null)
                        {
                            this.state_CODE = Dicts.StateCode[1];
                            this.err_Msg = "不存在该订单！";
                            return;
                        }

                        //查询该订单下的该员工指派的情况
                        OrderAssignment oa = bllOrderAssignment.FindByOrderAndStaff(order, staff);
                        if (oa != null)
                        {
                            RespDataASN_assignObj assObj = new RespDataASN_assignObj().Adapt(oa);
                            arrayData.Add(assObj);
                        }
                    }
                    else
                    {
                        //查询该店铺下的该员工，所有已指派的订单情况
                        IList<OrderAssignment> oaList = bllOrderAssignment.GetOAListByStaff(staff);
                        RespDataASN_assignObj assObj;
                        foreach (OrderAssignment oaObj in oaList)
                        {
                            assObj = new RespDataASN_assignObj().Adapt(oaObj);
                            arrayData.Add(assObj);
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(order_id))
                    {
                        Guid orderId;
                        bool isOrderId = Guid.TryParse(order_id, out orderId);
                        if (!isOrderId)
                        {
                            this.state_CODE = Dicts.StateCode[1];
                            this.err_Msg = "orderId格式有误";
                            return;
                        }

                        ServiceOrder order = bllServiceOrder.GetOne(orderId);
                        if (order == null)
                        {
                            this.state_CODE = Dicts.StateCode[1];
                            this.err_Msg = "不存在该订单！";
                            return;
                        }

                        //查询该店铺下的该订单，所有已指派的员工情况
                        IList<OrderAssignment> oaList = bllOrderAssignment.GetOAListByOrder(order);
                        RespDataASN_assignObj assObj;
                        foreach (OrderAssignment oaObj in oaList)
                        {
                            assObj = new RespDataASN_assignObj().Adapt(oaObj);
                            arrayData.Add(assObj);
                        }
                    }
                    else
                    {
                        //查询该店铺下，所有已指派的员工的所有订单的情况
                        IList<OrderAssignment> oaList = bllOrderAssignment.GetAllListForAssign(store.Id);
                        RespDataASN_assignObj assObj;
                        foreach (OrderAssignment oaObj in oaList)
                        {
                            assObj = new RespDataASN_assignObj().Adapt(oaObj);
                            arrayData.Add(assObj);
                        }
                    }
                }

                RespDataASN002004 respData = new RespDataASN002004();
                respData.arrayData = arrayData;
                this.state_CODE = Dicts.StateCode[0];
                this.RespData = respData;
            }
            catch (Exception ex)
            {
                ilog.Error(ex.Message);
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

    public override string BuildJsonResponse()
    {
        return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }
}


