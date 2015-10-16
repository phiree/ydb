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
public class ResponseVCM001002 : BaseResponse
{
    public ResponseVCM001002(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataVCM001002 requestData = this.request.ReqData.ToObject<ReqDataVCM001002>();

        //todo:用户验证的复用.
        DZMembershipProvider p = new DZMembershipProvider();
        BLLCashTicket bllCashTicket = new BLLCashTicket();
        string raw_id = requestData.uid;

        try
        {
            Guid uid = new Guid(PHSuit.StringHelper.InsertToId(raw_id));
            DZMembership member = p.GetUserById(uid);
            if (member == null)
            {
                this.state_CODE = Dicts.StateCode[8];
                this.err_Msg = "用户不存在,可能是传入的uid有误";
                return;
            }
            //验证用户的密码
            if (member.Password != FormsAuthentication.HashPasswordForStoringInConfigFile(requestData.userPWord, "MD5"))
            {
                this.state_CODE = Dicts.StateCode[9];
                this.err_Msg = "用户密码错误";
                return;
            }
            try
            {
                string srvTarget = requestData.vcsTarget;
                string strPageSize = requestData.pageSize;
                string strPageNum = requestData.pageNum;//base on 1
                int pageSize, pageNum;
                if (!int.TryParse(strPageSize, out pageSize) ||
                 !int.TryParse(strPageNum, out pageNum))
                {
                    this.state_CODE = Dicts.StateCode[1];
                    this.err_Msg = "分页大小或者分页索引不是数值格式";
                    return;
                }
                enum_CashTicketSearchType searchType = (enum_CashTicketSearchType)Enum.Parse(typeof(enum_CashTicketSearchType), srvTarget);

                IList<CashTicket> cashTicketList = bllCashTicket.GetCashTicketList(uid, searchType, pageNum, pageSize);

                RespDataVCM001002 respData = new RespDataVCM001002();

                respData.AdapList(cashTicketList);

                this.RespData =  respData ;
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

public class ReqDataVCM001002
{
    public string uid { get; set; }
    public string userPWord { get; set; }
    public string vcsTarget { get; set; }
    public string pageSize { get; set; }
    public string pageNum { get; set; }

}
public class RespDataVCM001002
{
    public IList<RespDataVCM001002_Cashticket> arrayData { get; set; }
    public RespDataVCM001002()
    {

        arrayData = new List<RespDataVCM001002_Cashticket>();
    }

    public void AdapList(IList<CashTicket> serviceOrderList)
    {
        foreach (CashTicket order in serviceOrderList)
        {
            RespDataVCM001002_Cashticket adapted_order = new RespDataVCM001002_Cashticket().Adap(order);
            arrayData.Add(adapted_order);
        }

    }
}
public class RespDataVCM001002_Cashticket
{
    public string vcsID { get; set; }
    public string srvBiz { get; set; }
    public string srvBizID { get; set; }
    public string vcsStartTime { get; set; }
    public string vcsEndTime { get; set; }
    public string vcsMoney { get; set; }
    public string vcsStatus { get; set; }
    public string vcsExdes { get; set; }

    public RespDataVCM001002_Cashticket Adap(CashTicket cashTicket)
    {
        this.vcsID = cashTicket.TicketCode;
        this.srvBiz = cashTicket.CashTicketTemplate.Business.Name;
        this.srvBizID = cashTicket.CashTicketTemplate.Business.Id.ToString();
        this.vcsStartTime = cashTicket.CashTicketTemplate.ValidDate.ToString("yyyyMMddHHmm");// ServiceTimeBegin;
        this.vcsEndTime = cashTicket.CashTicketTemplate.ExpiredDate.ToString("yyyyMMddHHmm");
        ///这个是服务单价
        this.vcsMoney = cashTicket.CashTicketTemplate.Amount.ToString("#.#");
        this.vcsStatus = string.Empty;
        this.vcsExdes = cashTicket.CashTicketTemplate.Conditions;
        return this;
    }
}


