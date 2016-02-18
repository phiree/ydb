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
public class ResponseASN002007 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    public ResponseASN002007(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataASN002007 requestData = this.request.ReqData.ToObject<ReqDataASN002007>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
        BLLServiceOrder bllServiceOrder = new BLLServiceOrder();
        BLLComplaint bllComplaint = new BLLComplaint();
        BLLStaff bllStaff = new BLLStaff();
        BLLOrderAssignment bllOrderAssignment = new BLLOrderAssignment();

        string raw_id = requestData.userID;
        string order_id = requestData.orderId;
        IList<string> staff_ids = requestData.arrayStaffId;
        bool assign = requestData.assign;

        try
        {
            Guid userId,orderId;
            bool isUserId = Guid.TryParse(raw_id, out userId);
            if (!isUserId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "用户Id格式有误";
                return;
            }

            bool isOrderId = Guid.TryParse(order_id, out orderId);
            if (!isOrderId)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "orderId格式有误";
                return;
            }

            if (staff_ids.Count==0)
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
                ServiceOrder order= bllServiceOrder.GetOne(orderId);
                if (order == null)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "没有对应的订单,请检查传入的orderID";
                    return;
                }

                Staff staff;
                Guid staffId;
                bool isStaffId;
                IList<Staff> orderList = new List<Staff>();
                for (int i = 0; i < staff_ids.Count; i++)
                {
                    isStaffId = Guid.TryParse(staff_ids[i], out staffId);
                    if (!isStaffId)
                    {
                        this.state_CODE = Dicts.StateCode[1];
                        this.err_Msg = "staffId格式有误";
                        return;
                    }

                    staff = bllStaff.GetOne(staffId);
                    if (staff == null)
                    {
                        this.state_CODE = Dicts.StateCode[4];
                        this.err_Msg = "没有对应的员工,请检查传入的staffID";
                        return;
                    }
                    orderList.Add(staff);
                }
                
                OrderAssignment orderAssignment;
                foreach(Staff st in orderList)
                {
                    orderAssignment = bllOrderAssignment.FindByOrderAndStaff(order, st);
                    if (orderAssignment == null)
                    {
                        orderAssignment = new OrderAssignment();
                    }
                    orderAssignment.Order = order;
                    orderAssignment.AssignedStaff = st;
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


