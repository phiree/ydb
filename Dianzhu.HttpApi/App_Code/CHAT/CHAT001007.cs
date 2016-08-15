using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.Api.Model;
/// <summary>
/// Summary description for CHAT001001
/// </summary>
public class ResponseCHAT001007:BaseResponse
{
    public ResponseCHAT001007(BaseRequest request):base(request)
    {
        //
        // TODO: Add constructor logic here
        //
    }
    
    protected override void BuildRespData()
    {
        ReqDataCHAT001007 requestData = this.request.ReqData.ToObject<ReqDataCHAT001007>();
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();

        IBLLServiceOrder bllServiceOrder = Bootstrap.Container.Resolve<IBLLServiceOrder>();
        BLLReceptionChat bllReceptionChat = Bootstrap.Container.Resolve<BLLReceptionChat>();

        string user_id = requestData.userID;
        string order_id = requestData.orderID;

        Guid userId, orderId;
        bool isUserId = Guid.TryParse(user_id, out userId);
        if (!isUserId)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = "userId格式有误";
            return;
        }

        if (string.IsNullOrEmpty(order_id))
        {
            orderId = Guid.Empty;
        }
        else
        {
            bool isGuid = Guid.TryParse(requestData.orderID, out orderId);
            if (!isGuid)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "OrderId格式有误";
                return;
            }

            ServiceOrder order = bllServiceOrder.GetOne(orderId);
            if (order == null)
            {
                this.state_CODE = Dicts.StateCode[1];
                this.err_Msg = "Order不存在";
                return;
            }
        }        

        DZMembership member;
        if (request.NeedAuthenticate)
        {
            bool validated = new Account(p).ValidateUser(userId, requestData.pWord, this, out member);
            if (!validated)
            {
                return;
            } 
        }
        else
        {
            member = p.GetUserById(userId);
        }
        if (member == null)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = "用户不存在";
            return;
        }

        int rowCount;
        int pageSize = 10;
        try
        {
            pageSize = Convert.ToInt32(requestData.pageSize);
        }
        catch (Exception ex)
        {
            pageSize = 10;
        }
        string target = requestData.target;
        enum_ChatTarget chatTarget;
        bool is_EnumTarget = Enum.TryParse<enum_ChatTarget>(target, out chatTarget);
        if (!is_EnumTarget)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = "传入的聊天类型有误！";
            return;
        }

        Guid targetId;
        bool isGuidtarget = Guid.TryParse(requestData.targetID, out targetId);
        if (!isGuidtarget)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = "TargetId格式有误";
            return;
        }

        ReceptionChat targetChat = bllReceptionChat.GetOne(targetId);
        if (targetChat == null)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = "TargetId不存在";
            return;

        }

        try
        {
            IList<ReceptionChat> chatList = bllReceptionChat.GetReceptionChatListByTargetIdAndSize(userId, Guid.Empty, orderId, DateTime.MinValue, DateTime.MaxValue, pageSize, targetChat, requestData.low, chatTarget);

            RespDataCHAT001007 respData = new RespDataCHAT001007();
            respData.AdapList(chatList);
            this.RespData = respData;
            this.state_CODE = Dicts.StateCode[0];
            return;
        }
        catch (Exception ex)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = ex.Message;
            return;
        }
    }
}