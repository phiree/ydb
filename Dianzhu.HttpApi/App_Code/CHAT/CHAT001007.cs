using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
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

        Guid targetId;
        bool isGuidtarget = Guid.TryParse(requestData.targetID, out targetId);
        if (!isGuidtarget)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = "OrderId格式有误";
            return;
        }

        int pageSize = 10;
        try {
            pageSize = Convert.ToInt32(requestData.pageSize);
        }
        catch (Exception ex)
        {
            pageSize = 10;
        }
        try
        {
            IList<ReceptionChat> chatList = bllReception.GetReceptionChatListByTargetIdAndSize(member,null, orderId, DateTime.MinValue
                ,DateTime.MaxValue,pageSize, targetId, requestData.low);

            //var chatListNew = bllReception.GetReceptionChatListByTarget(chatList, requestData.target);

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
public class ReqDataCHAT001007
{
    public string userID { get; set; }
    public string pWord { get; set; }
    public string orderID { get; set; }
    public string target { get; set; }
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
    public string pageSize { get; set; }
    public string targetID { get; set; }
    public string low { get; set; }
}
public class RespDataCHAT001007
{
    public IList<RespDataCHAT_chatObj> arrayData { get; set; }
    public RespDataCHAT001007()
    {
        arrayData = new List<RespDataCHAT_chatObj>();
    }
    public void AdapList(IList<ReceptionChat> chatList)
    {
        foreach (ReceptionChat chat in chatList)
        {
            RespDataCHAT_chatObj chatObj = new RespDataCHAT_chatObj().Adapt(chat);
            arrayData.Add(chatObj);
        }
    }
}