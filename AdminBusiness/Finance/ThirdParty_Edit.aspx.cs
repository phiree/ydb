using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
using Ydb.Finance.Application;
public partial class Finance_ThirdParty : BasePage
{
    IBalanceAccountService balanceAccountService = Bootstrap.Container.Resolve<IBalanceAccountService>();
    IBusinessService businessService = Bootstrap.Container.Resolve< IBusinessService>();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                lblBusinessId.Text = Request.QueryString["businessid"].ToString();
             Business b = businessService.GetOne(new Guid(lblBusinessId.Text));
                if (b == null)
                {
                    PHSuit.Notification.Alert(Page, "商户不存在！");
                    return;
                }
                lblUserId.Text = b.OwnerId.ToString();
                BalanceAccountDto balanceAccountDto = balanceAccountService.GetAccount(lblBusinessId.Text);
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
            catch(Exception ex)
            {
                //PHSuit.Notification.Alert(Page, "没有businessid参数！");
                PHSuit.Notification.Show(Page, "", ex.Message, "default.aspx");
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            BalanceAccountDto balanceAccountDto = new BalanceAccountDto();
            balanceAccountDto.UserId = lblBusinessId.Text;
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
            Response.Redirect("default.aspx?businessid=" + lblBusinessId.Text);
        }
        catch (Exception ex)
        {
            PHSuit.Notification.Alert(Page, ex.Message);
        }
    }

    protected void btnConcel_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx?businessid=" + lblBusinessId.Text);
    }
}