using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
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
        Guid orderId;
        bool isGuid = Guid.TryParse(requestData.orderID, out orderId);
        if (!isGuid)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = "OrderId格式有误";
            return;
        }
        try
        {
            IList<ReceptionChat> chatList = bllReception.GetReceptionChatList(member,null, orderId,DateTime.MinValue,DateTime.Now,-1,-1, out rowCount);

            var chatListNew = bllReception.GetReceptionChatListByTarget(chatList, requestData.target);

            RespDataCHAT001004 respData = new RespDataCHAT001004 { sum = chatListNew.Count.ToString() };
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
    public CHATTarget Target
    {
        get
        {
            CHATTarget tar;
            bool isType = Enum.TryParse<CHATTarget>(target, out tar);
            if (!isType) { throw new Exception("不可识别的用户类型"); }
            return tar;
        }
    }
}
public class RespDataCHAT001004
{
    public string sum { get; set; }
}