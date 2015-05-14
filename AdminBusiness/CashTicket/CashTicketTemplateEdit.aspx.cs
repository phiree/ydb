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
    DZMembershipProvider dzp = new DZMembershipProvider();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        BLLCashTicketTemplate bllctt = new BLLCashTicketTemplate();
       
      
       

     CashTicketTemplate ctt=bllctt.Create(tbx_name.Text.Trim(),((BusinessUser)CurrentUser).BelongTo, DateTime.Today.AddDays(1), DateTime.Today.AddMonths(1),
            Convert.ToInt32(tbx_amount.Text.Trim()), tbx_conditions.Text.Trim(), Convert.ToSingle(tbx_coverage.Text.Trim()));
        
    }
    
}