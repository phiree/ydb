using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Dianzhu.Api.Model;

/// <summary>
/// 获取一条服务信息的详情
/// </summary>
public class ResponseASN001005 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseASN001005(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataASN001005 requestData = this.request.ReqData.ToObject<ReqDataASN001005>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
        BLLServiceOrder bllServiceOrder = new BLLServiceOrder();
        BLLStaff bllStaff = new BLLStaff();
        BLLOrderAssignment bllOrderAssignment = new BLLOrderAssignment();

        string raw_id = requestData.userID;
        string order_id = requestData.orderId;
        string staff_id = requestData.staffId;

        try
        {
            Guid userId,orderId,staffId;
            bool isUserId = Guid.TryParse(raw_id, out userId);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "userId格式有误";
                return;
            }

            bool isOrdrId = Guid.TryParse(order_id, out orderId);
            if (!isOrdrId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "orderId格式有误";
                return;
            }

            bool isStaffId = Guid.TryParse(staff_id, out staffId);
            if (!isStaffId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "staffId格式有误";
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
                ServiceOrder order = bllServiceOrder.GetOne(orderId);
                if (order == null)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "没有对应的订单,请检查传入的orderID";
                    return;
                }

                Staff staff = bllStaff.GetOne(staffId);
                if (staff == null)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "没有对应的员工,请检查传入的staffID";
                    return;
                }

                IList<OrderAssignment> oaList = bllOrderAssignment.GetOAListByOrder(order);
                IList<string> staffList = new List<string>();

                RespDataASN_orderObj orderObj = new RespDataASN_orderObj().Adapt(order, oaList);
                RespDataASN_assignObj assignObj = new RespDataASN_assignObj().Adapt(staff,oaList);
                assignObj.orderObj = orderObj;

                RespDataASN001005 respData = new RespDataASN001005();
                respData.assignObj = assignObj;

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
}


