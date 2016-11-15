using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ydb.Finance.Application;
public partial class Finance_ThirdParty : BasePage
{
    IBalanceAccountService balanceAccountService = Bootstrap.Container.Resolve<IBalanceAccountService>();
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
                    lblAccountType.Text = balanceAccountDto.AccountTypeName;
                }
                else
                {
                    lblAccount.Text = "";
                    lblAccountType.Text = "";
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
            BalanceAccountDto balanceAccountDto = new BalanceAccountDto();
            balanceAccountDto.UserId =lblUserId.Text;
            balanceAccountDto.Account = txtAccount.Text.Trim();
            balanceAccountDto.AccountName = txtAccountName.Text.Trim();
            balanceAccountDto.AccountType = AccountTypeEnums.Alipay;
            balanceAccountDto.AccountCode = txtAccountCode.Text.Trim();
            balanceAccountDto.AccountPhone = txtAccountPhone.Text.Trim();
            balanceAccountDto.flag = 1;
            if (lblAccount.Text == "")
            {
                balanceAccountService.BindingAccount(balanceAccountDto);
            }
            else
            {
                balanceAccountService.UpdateAccount(balanceAccountDto);
            }
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