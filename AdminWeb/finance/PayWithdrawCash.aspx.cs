using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ydb.Finance.Application;
using Dianzhu.Pay;
using Dianzhu.BLL;

public partial class finance_PayWithdrawCash : BasePage
{
    IWithdrawApplyService withdrawApplyService = Bootstrap.Container.Resolve<IWithdrawApplyService>();
    Ydb.Common.Infrastructure.ISerialNoBuilder iserialno = Bootstrap.Container.Resolve<Ydb.Common.Infrastructure.ISerialNoBuilder>();
    BLLPay bllPay = Bootstrap.Container.Resolve<BLLPay>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindList();
        }
    }
    private void BindList()
    {
        long totalRecord;
        int currentPageIndex = 1;
        string paramPage = Request.Params["page"];
        if (!string.IsNullOrEmpty(paramPage))
        {
            currentPageIndex = int.Parse(paramPage);
        }
        WithdrawApplyFilter withdrawApplyFilter = new WithdrawApplyFilter();
        withdrawApplyFilter.ApplyStatus = ApplyStatusEnums.ApplyWithdraw;
        gvWithdrawApply.DataSource = withdrawApplyService.GetWithdrawApplyList(new Ydb.Common.Specification.TraitFilter(), withdrawApplyFilter);
        gvWithdrawApply.DataBind();
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)sender;
        for (int i=0;i< gvWithdrawApply.Rows.Count;i++)
        {
            CheckBox chkOne = (CheckBox)gvWithdrawApply.Rows[i].FindControl("chkOne");
            chkOne.Checked = chkAll.Checked;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        IList<Guid> guidId = new List<Guid>();
        int i = 0;
        int c = 0;
        for (i = 0; i < gvWithdrawApply.Rows.Count; i++)
        {
            CheckBox chkOne = (CheckBox)gvWithdrawApply.Rows[i].FindControl("chkOne");
            Label lblId= (Label)gvWithdrawApply.Rows[i].FindControl("lblId");
            if (chkOne.Checked)
            {
                guidId.Add(new Guid(lblId.Text));
                c++;
            }
        }
        if (c > 1000)
        {
            PHSuit.Notification.Alert(Page, "一次不能超过1000笔！");
            return;
        }
        string errMsg = "";
        string strSerialNo = iserialno.GetSerialNo("PW" + DateTime.Now.ToString("yyyyMMddHHmmssfff"), 2);
        IList <WithdrawCashDto> withdrawCashDtoList= withdrawApplyService.PayByWithdrawApply(guidId,"admin", strSerialNo, out errMsg);
        if (withdrawCashDtoList.Count > 0)
        {
            string strSubject = "";
            for (i = 0; i < withdrawCashDtoList.Count; i++)
            {
                strSubject = strSubject + withdrawCashDtoList[i].ApplySerialNo + "^" + withdrawCashDtoList[i].Account + "^" + withdrawCashDtoList[i].AccountName + "^" + String.Format("{0:F}", withdrawCashDtoList[i].Amount) + "^" +(string.IsNullOrEmpty(withdrawCashDtoList[i].Remark)?"提现": withdrawCashDtoList[i].Remark) + "|";
            }
            strSubject.TrimEnd('|');
            IPayRequest pay = bllPay.CreatePayBatch(withdrawCashDtoList.Count, strSerialNo, strSubject);
            string requestString = pay.CreatePayRequest();
            Response.Write(requestString);
        }
        else
        {
            PHSuit.Notification.Alert(Page, "要支付的提现单出错！");
        }
    }
}