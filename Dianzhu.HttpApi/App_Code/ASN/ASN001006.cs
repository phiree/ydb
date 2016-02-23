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
public class ResponseASN001006 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseASN001006(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataASN001006 requestData = this.request.ReqData.ToObject<ReqDataASN001006>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
        BLLBusiness bllBusiness = new BLLBusiness();
        BLLServiceOrder bllServiceOrder = new BLLServiceOrder();
        BLLStaff bllStaff = new BLLStaff();
        BLLOrderAssignment bllOrderAssignment = new BLLOrderAssignment();

        try
        {
            string raw_id = requestData.userID;
            string business_id = requestData.businessId;
            string staff_id = requestData.staffId;
            int pageSize = Int32.Parse(requestData.pageSize);
            int pageNum = Int32.Parse(requestData.pageNum);

            Guid userId,businessId,staffId;
            bool isUserId = Guid.TryParse(raw_id, out userId);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "userId格式有误";
                return;
            }

            bool isOrdrId = Guid.TryParse(business_id, out businessId);
            if (!isOrdrId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "businessId格式有误";
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
                Business business = bllBusiness.GetOne(businessId);
                if (business == null)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "没有对应的订单,请检查传入的businessID";
                    return;
                }

                Staff staff = bllStaff.GetOne(staffId);
                if (staff == null)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "没有对应的员工,请检查传入的staffID";
                    return;
                }

                int totalAmount;
                IList<ServiceOrder> orderList = bllServiceOrder.GetListForBusiness(business, pageNum, pageSize, out totalAmount);
                OrderAssignment oa;
                RespDataASN_arrayData arrObj;
                IList<RespDataASN_arrayData> arrList = new List<RespDataASN_arrayData>();
                foreach (ServiceOrder order in orderList)
                {
                    arrObj = new RespDataASN_arrayData();
                    arrObj.orderId = order.Id.ToString();

                    oa = bllOrderAssignment.FindByOrderAndStaff(order, staff);
                    if (oa != null)
                    {
                        arrObj.assigned = oa.Enabled == true ? "1" : "0";
                    }
                    else
                    {
                        arrObj.assigned = "0";
                    }

                    arrList.Add(arrObj);
                }

                RespDataASN001006 respData = new RespDataASN001006();
                respData.arrayData = arrList;

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


