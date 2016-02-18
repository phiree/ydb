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
public class ResponseASN001007 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseASN001007(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataASN001007 requestData = this.request.ReqData.ToObject<ReqDataASN001007>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
        BLLServiceOrder bllServiceOrder = new BLLServiceOrder();
        BLLComplaint bllComplaint = new BLLComplaint();
        BLLStaff bllStaff = new BLLStaff();
        BLLOrderAssignment bllOrderAssignment = new BLLOrderAssignment();

        string raw_id = requestData.userID;
        string staff_id = requestData.staffId;
        IList<string> order_ids = requestData.arrayOrderId;
        bool assign = requestData.assign;

        try
        {
            Guid userId,staffId;
            bool isUserId = Guid.TryParse(raw_id, out userId);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "用户Id格式有误";
                return;
            }

            bool isStaffId = Guid.TryParse(staff_id, out staffId);
            if (!isStaffId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "员工Id格式有误";
                return;
            }

            if (order_ids.Count==0)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "arrayOrderId不能为空！";
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
                Staff staff = bllStaff.GetOne(staffId);
                if (staff == null)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "没有对应的员工,请检查传入的staffID";
                    return;
                }

                ServiceOrder order;
                Guid orderId;
                bool isOrderId;
                IList<ServiceOrder> orderList = new List<ServiceOrder>();
                for (int i = 0; i < order_ids.Count; i++)
                {
                    isOrderId = Guid.TryParse(order_ids[i], out orderId);
                    if (!isOrderId)
                    {
                        this.state_CODE = Dicts.StateCode[1];
                        this.err_Msg = "orderId格式有误";
                        return;
                    }

                    order = bllServiceOrder.GetOne(orderId);
                    if (order == null)
                    {
                        this.state_CODE = Dicts.StateCode[4];
                        this.err_Msg = "没有对应的订单,请检查传入的orderID";
                        return;
                    }
                    orderList.Add(order);
                }
                
                OrderAssignment orderAssignment;
                foreach(ServiceOrder sorder in orderList)
                {
                    orderAssignment = bllOrderAssignment.FindByOrderAndStaff(sorder,staff);
                    if (orderAssignment == null)
                    {
                        orderAssignment = new OrderAssignment();
                    }
                    orderAssignment.Order = sorder;
                    orderAssignment.AssignedStaff = staff;
                    if (assign)
                    {
                        orderAssignment.AssignedTime = DateTime.Now;
                    }
                    else
                    {
                        orderAssignment.DeAssignedTime = DateTime.Now;
                    }
                    orderAssignment.Enabled = assign;
                    bllOrderAssignment.SaveOrUpdate(orderAssignment);
                }

                this.state_CODE = Dicts.StateCode[0];
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


