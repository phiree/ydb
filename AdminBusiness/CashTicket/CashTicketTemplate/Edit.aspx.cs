using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
using PHSuit;
using System.Web.Security;
public partial class CashTicketTemplateEdit :BasePage
{
    DZMembershipProvider dzp = Installer.Container.Resolve<DZMembershipProvider>();
    private bool IsNew=true;
    CashTicketTemplate currentCashTicketTemplate=new CashTicketTemplate();
    BLLCashTicketTemplate bllctt = new BLLCashTicketTemplate();
    protected void Page_Load(object sender, EventArgs e)
    {
        string paramId = Request.Params["id"];
        if (!string.IsNullOrEmpty(paramId))
        {
            IsNew = false;
            currentCashTicketTemplate = bllctt.GetOne(new Guid(paramId));
        }
        if (!IsPostBack)
        {
            if (!IsNew)
            {
                LoadForm();
            }
        }
    }
    private void LoadForm()
    {
        tbx_amount.Text = currentCashTicketTemplate.Amount.ToString();
        tbx_conditions.Text = currentCashTicketTemplate.Conditions;
        tbx_coverage.Text = currentCashTicketTemplate.Coverage.ToString();
        tbx_expiredDate.Text = currentCashTicketTemplate.ExpiredDate.ToString();
        tbx_name.Text = currentCashTicketTemplate.Name;
        cbxEnable.Checked = currentCashTicketTemplate.Enabled;
    }
    private void UpdateForm()
    {
        currentCashTicketTemplate.Amount = Convert.ToInt32(tbx_amount.Text);
        currentCashTicketTemplate.Conditions = tbx_conditions.Text;
        currentCashTicketTemplate.Coverage = Convert.ToSingle(tbx_coverage.Text);
        currentCashTicketTemplate.ExpiredDate = Convert.ToDateTime(tbx_expiredDate.Text);
        currentCashTicketTemplate.Name = tbx_name.Text;
        currentCashTicketTemplate.Enabled = cbxEnable.Checked;
        currentCashTicketTemplate.Business = CurrentBusiness;
         
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        UpdateForm();
         bllctt.SaveOrUpdate(currentCashTicketTemplate);
         string result_url = Request.RawUrl;
         if (IsNew)
         {
             result_url = result_url + "?id=" + currentCashTicketTemplate.Id;
         }

         Notification.Show(Page, "", "保存成功", result_url);
 
        
    }
    
}