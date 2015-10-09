﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.BLL;
using Dianzhu.Model;
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
        
        Guid andMemberId;
        bool isGuidMember=Guid.TryParse( requestData.andID,out andMemberId);
        if (!isGuidMember)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = "AndId格式有误";
            return;
        }
        DZMembership andMember = p.GetUserById(andMemberId);
        if(andMember==null)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = "没有找到该用户"+andMemberId;
            return;
        }
        int pageIndex = 0, pageSize = 10;
        try {
            pageIndex = Convert.ToInt32(requestData.pageNum);
            pageSize = Convert.ToInt32(requestData.pageSize);
        }
        catch (Exception ex)
        {
            pageIndex = 0;
            pageSize = 10;
        }
        try
        {
            IList<ReceptionChat> chatList = bllReception.GetReceptionChatList(member,andMember, orderId, DateTime.MinValue
                ,DateTime.MaxValue,pageIndex,pageSize, out rowCount);

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
public class ReqDataCHAT001006
{
    public string userID { get; set; }
    public string pWord { get; set; }
    public string orderID { get; set; }
    public string andID { get; set; }
    public string pageSize { get; set; }
    public string pageNum { get; set; }
}
public class RespDataCHAT001006
{
    public IList<RespDataCHAT_chatObj> arrayData { get; set; }
    public RespDataCHAT001006()
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