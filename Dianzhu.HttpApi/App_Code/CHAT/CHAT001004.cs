using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
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
        DZMembershipProvider p = new DZMembershipProvider();
        string raw_id = requestData.userID;
        DZMembership member;
        bool validated = new Account(p).ValidateUser(new Guid(raw_id), requestData.pWord, this, out member);
        if (!validated)
        {
            return;
        }
        BLLReception bllReception = new BLLReception();
        int rowCount;
        string target = requestData.target;
        IList<ReceptionChat> chatList;
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
            chatList = bllReception.GetReceptionChatList(member, null, Guid.Empty, DateTime.MinValue, DateTime.Now, -1, -1, chatTarget, out rowCount);
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

            chatList = bllReception.GetReceptionChatList(member, null, orderId, DateTime.MinValue, DateTime.Now, -1, -1, chatTarget, out rowCount);            
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
public class ReqDataCHAT001004
{
    public string userID { get; set; }
    public string pWord { get; set; }
    public string orderID { get; set; }
    public string target{ get; set; }
    public Dianzhu.Model.Enums.enum_ChatTarget Target
    {
        get
        {
            Dianzhu.Model.Enums.enum_ChatTarget tar;
            bool isType = Enum.TryParse<Dianzhu.Model.Enums.enum_ChatTarget>(target, out tar);
            if (!isType) { throw new Exception("不可识别的用户类型"); }
            return tar;
        }
    }
}
public class RespDataCHAT001004
{
    public string sum { get; set; }
}