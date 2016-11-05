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
public class ResponseCHAT001006:BaseResponse
{
    public ResponseCHAT001006(BaseRequest request):base(request)
    {
        //
        // TODO: Add constructor logic here
        //
    }
    protected override void BuildRespData()
    {
        ReqDataCHAT001006 requestData = this.request.ReqData.ToObject<ReqDataCHAT001006>();
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
        Guid orderId;
        string target = requestData.target; 
        IList<ReceptionChatDto> chatList;

        int pageIndex = 1, pageSize = 5;
        try
        {
            pageIndex = Convert.ToInt32(requestData.pageNum);            
        }
        catch
        {
            pageIndex = 1;
           
        }
        try
        {
            pageSize = Convert.ToInt32(requestData.pageSize);
        }
        catch
        {
            pageSize = 5;
        }
        enum_ChatTarget chatTarget;
        bool is_EnumTarget= Enum.TryParse<enum_ChatTarget>(target, out chatTarget);
        if (!is_EnumTarget)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = "传入的聊天类型有误！";
            return;
        }

        if (requestData.orderID == "")
        {
            chatList = bllReceptionChat.GetReceptionChatList(userId.ToString(), string.Empty, string.Empty, DateTime.MinValue, DateTime.MaxValue, pageIndex-1, pageSize, chatTarget.ToString(), out rowCount);
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

            chatList = bllReceptionChat.GetReceptionChatList(userId.ToString(), string.Empty, orderId.ToString(), DateTime.MinValue, DateTime.MaxValue, pageIndex, pageSize, chatTarget.ToString(), out rowCount);
        }
        
        try
        {
            RespDataCHAT001006 respData = new RespDataCHAT001006();
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