using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
public partial class CashTicket_Default :BasePage
{
    BLLCashTicketTemplate bll = new BLLCashTicketTemplate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindList();
        }
    }
    private void BindList()
    {
        gv.DataSource = bll.GetTemplateList(((BusinessUser)CurrentUser).BelongTo);
        gv.DataBind();
    }
}