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
        string raw_id = requestData.userID;
        DZMembership member;
        if (request.NeedAuthenticate)
        {
            bool validated = new Account(p).ValidateUser(new Guid(raw_id), requestData.pWord, this, out member);
            if (!validated)
            {
                return;
            } 
        }
        else
        {
            member = p.GetUserById(new Guid(raw_id));
        }
        BLLReceptionChat bllReceptionChat = Bootstrap.Container.Resolve<BLLReceptionChat>();
        int rowCount;
        Guid orderId;
        string target = requestData.target; 
        IList<ReceptionChat> chatList;

        int pageIndex = 0, pageSize = 10;
        try
        {
            pageIndex = Convert.ToInt32(requestData.pageNum);
            pageSize = Convert.ToInt32(requestData.pageSize);
        }
        catch (Exception ex)
        {
            pageIndex = 0;
            pageSize = 10;
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
            chatList = bllReceptionChat.GetReceptionChatList(member, null, Guid.Empty, DateTime.MinValue, DateTime.MaxValue, pageIndex, pageSize, chatTarget, out rowCount);
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

            chatList = bllReceptionChat.GetReceptionChatList(member, null, orderId, DateTime.MinValue, DateTime.MaxValue, pageIndex, pageSize, chatTarget, out rowCount);
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