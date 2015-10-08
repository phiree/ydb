using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
/// <summary>
/// Summary description for CHAT001001
/// </summary>
public class ResponseCHAT001001:BaseResponse
{
    public ResponseCHAT001001(BaseRequest request):base(request)
    {
        //
        // TODO: Add constructor logic here
        //
    }
    protected override void BuildRespData()
    {
        ReqDataCHAT001001 requestData = this.request.ReqData.ToObject<ReqDataCHAT001001>();
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
            IList<ReceptionChat> chatList = bllReception.GetHistoryReceptionChat(member, orderId, out rowCount);
            RespDataCHAT001001 respData = new RespDataCHAT001001 { sum = rowCount.ToString() };
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
public class ReqDataCHAT001001
{
    public string userID { get; set; }
    public string pWord { get; set; }
    public string orderID { get; set; }
}
public class RespDataCHAT001001
{
    public string sum { get; set; }
}