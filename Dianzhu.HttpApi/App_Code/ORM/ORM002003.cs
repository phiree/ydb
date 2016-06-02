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
using Dianzhu.Api.Model;
/// <summary>
/// 获取用户的服务订单列表
/// </summary>

public class ResponseORM002003 : BaseResponse
{
    log4net.ILog ilog = log4net.LogManager.GetLogger("Dianzhu.HttpApi");
    IBLLServiceOrder bllOrder = Bootstrap.Container.Resolve<IBLLServiceOrder>();
    public ResponseORM002003(BaseRequest request) : base(request) {
         
    }
    protected override void BuildRespData()
    {
        ReqDataORM002003 requestData = this.request.ReqData.ToObject<ReqDataORM002003>();

 
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();
        BLLReceptionStatus bllReceptionStatus = new BLLReceptionStatus();
      
        BLLOrderAssignment bllOrderAssignment = new BLLOrderAssignment();
        string raw_id = requestData.userID;
        string reqOrderId = requestData.orderID;

        try
        {
            Guid user_ID, order_ID;
            bool isUserGuid = Guid.TryParse(raw_id, out user_ID);
            if (!isUserGuid)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "userId格式有误";
                return;
            }
            
            bool isValidGuid = Guid.TryParse(reqOrderId, out order_ID);
            if (!isValidGuid)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "OrderId格式有误";
                return;
            }

            DZMembership member;
            if (request.NeedAuthenticate)
            {
                bool validated = new Account(p).ValidateUser(user_ID, requestData.pWord, this, out member);
                if (!validated)
                {
                    return;
                } 
            }
            else
            {
                member = p.GetUserById(user_ID);
                if (member == null)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "用户不存在!";
                    return;
                }
            }
            try
            {
                ServiceOrder order = bllOrder.GetOrderByIdAndCustomer(order_ID, member);
                if (order == null)
                {
                    this.state_CODE = Dicts.StateCode[4];
                    this.err_Msg = "该订单不存在";
                    return;
                }

                if (order.Details.Count <= 0)
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "该订单没有确定的服务";
                    return;
                }

                ilog.Debug("开始分配客服");
                IIMSession imSession = new IMSessionsDB();
                ReceptionAssigner ra = new ReceptionAssigner(imSession);
                Guid targetId = Guid.Empty;
                IList<OrderAssignment> orderAssList = bllOrderAssignment.GetOAListByOrder(order);
                if (orderAssList.Count > 0)
                {
                    targetId = orderAssList[0].AssignedStaff.Id;
                }
                else
                {
                    targetId = order.Details[0].OriginalService.Business.Owner.Id;
                }
                IAssignStratage ias = new AssignStratageManually(targetId);
                ra = new ReceptionAssigner(ias, imSession);

                RespDataORM002003 respData = new RespDataORM002003();
                RespDataORM_storeObj storeObj = new RespDataORM_storeObj().Adap(order.Details[0].OriginalService.Business);
                respData.targetID = targetId.ToString();
                respData.storeObj = storeObj;
                
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

 


