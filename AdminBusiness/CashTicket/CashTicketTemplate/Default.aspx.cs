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
    BLLBusiness bllBusiness = Installer.Container.Resolve<BLLBusiness>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindList();
        }
    }
    private void BindList()
    {
        gv.DataSource = CurrentBusiness.CashTicketTemplates;// bll.GetTemplateList(CurrentBusiness);
        gv.DataBind();
    }
}