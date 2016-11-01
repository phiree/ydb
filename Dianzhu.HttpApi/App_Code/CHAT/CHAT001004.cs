using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.Api.Model;
using Ydb.InstantMessage.Application;
using Ydb.InstantMessage.DomainModel.Chat;
/// <summary>
/// Summary description for CHAT001001
/// </summary>
public class ResponseCHAT001004:BaseResponse
{
    public ResponseCHAT001004(BaseRequest request):base(request)
    {
        //
        // TODO: Add constructor logic here
        //
    }
    protected override void BuildRespData()
    {
        ReqDataCHAT001004 requestData = this.request.ReqData.ToObject<ReqDataCHAT001004>();
        DZMembershipProvider p = Bootstrap.Container.Resolve<DZMembershipProvider>();
        string user_id = requestData.userID;

        Guid userId;
        bool isUserId = Guid.TryParse(user_id, out userId);
        if (!isUserId)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = "userId格式有误";
            return;
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

        IChatService bllReceptionChat = Bootstrap.Container.Resolve<IChatService>();
        int rowCount;
        string target = requestData.target;
        IList<ReceptionChatDto> chatList;
        Guid orderId;
        enum_ChatTarget chatTarget;
        bool is_EnumTarget = Enum.TryParse<enum_ChatTarget>(target, out chatTarget);
        if (!is_EnumTarget)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = "传入的聊天类型有误！";
            return;
        }
        if (requestData.orderID == "")
        {
            chatList = bllReceptionChat.GetReceptionChatList(userId, Guid.Empty, Guid.Empty, DateTime.MinValue, DateTime.Now, -1, -1, chatTarget.ToString(), out rowCount);
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

            chatList = bllReceptionChat.GetReceptionChatList(userId, Guid.Empty, orderId, DateTime.MinValue, DateTime.Now, -1, -1, chatTarget.ToString(), out rowCount);            
        }
        
        try
        {
            RespDataCHAT001004 respData = new RespDataCHAT001004 { sum = chatList.Count.ToString() };
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