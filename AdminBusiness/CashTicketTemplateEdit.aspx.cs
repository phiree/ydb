using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
using System.Web.Security;
public partial class CashTicketTemplateEdit :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        BLLCashTicketTemplate bllctt = new BLLCashTicketTemplate();
        
        MembershipUser b=Membership.GetUser();

       // bllctt.Create(tbx_name.Text.Trim(),
    }
    
}