using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ydb.Finance.Application;

public partial class Finance_withdraw : System.Web.UI.Page
{
    IBalanceAccountService balanceAccountService = Bootstrap.Container.Resolve<IBalanceAccountService>();
    IBalanceTotalService balanceTotalService = Bootstrap.Container.Resolve<IBalanceTotalService>();
    IWithdrawApplyService withdrawApplyService = Bootstrap.Container.Resolve<IWithdrawApplyService>();
    Dianzhu.BLL.BLLBusiness bllBusiness = Bootstrap.Container.Resolve<Dianzhu.BLL.BLLBusiness>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                lblBusinessId.Text = Request.QueryString["businessid"].ToString();
                Dianzhu.Model.Business b = bllBusiness.GetOne(new Guid(lblBusinessId.Text));
                if (b == null)
                {
                    PHSuit.Notification.Alert(Page, "商户不存在！");
                    return;
                }
                lblUserId.Text = b.OwnerId.ToString();
                BalanceAccountDto balanceAccountDto = balanceAccountService.GetAccount(lblUserId.Text);
                if (balanceAccountDto != null)
                {
                    lblAccount.Text = balanceAccountDto.Account;
                    lblAccountType.Text = balanceAccountDto.AccountType.ToString ();
                }
                else
                {
                    PHSuit.Notification.Alert(Page, "该商户还没有绑定提现收款账户！");
                    return;
                }
                BalanceTotalDto balanceTotalDto = balanceTotalService.GetOneByUserId(lblUserId.Text);
                if (balanceTotalDto != null)
                {
                    lblTotalAmount.Text = balanceTotalDto.Total.ToString();
                }
                else
                {
                    lblTotalAmount.Text = "0";
                }
            }
            catch
            {
                PHSuit.Notification.Alert(Page, "没有businessid参数！");
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            withdrawApplyService.SaveWithdrawApply(lblUserId.Text, lblAccount.Text, (AccountTypeEnums)Enum.Parse(typeof(AccountTypeEnums), lblAccountType.Text), decimal.Parse(txtAmount.Text.Trim()), "");
            Response.Redirect("default.aspx ? businessid =" + lblBusinessId.Text);
        }
        catch (Exception ex)
        {
            PHSuit.Notification.Alert(Page, ex.Message);
        }
    }

    protected void btnConcel_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx ? businessid =" + lblBusinessId.Text);
    }
}