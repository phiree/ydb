using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
public partial class CashTicket_Default : BasePage
{
    BLLCashTicketTemplate BllTemplate = new BLLCashTicketTemplate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindList();
        }
    }
    private void BindList()
    {
       gvCashTickets.DataSource= BllTemplate.GetAll();
       gvCashTickets.DataBind();
    }
    protected void gvCashTickets_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblAmount = e.Row.FindControl("lblAmount") as Label;
            CashTicketTemplate t =(CashTicketTemplate) e.Row.DataItem;
            lblAmount.Text = t.CashTicketsReadyForClaim.Count.ToString();
        }
    }
    protected void gvCashTickets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strTemplateId = gvCashTickets.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
        Guid templateId = new Guid(strTemplateId);
        if (e.CommandName == "claim")
        { 
             
            string errMsg;
            CashTicket ct = BllTemplate.ClaimForAnCashTicket(CurrentUser, templateId, out errMsg);

            if (!string.IsNullOrEmpty(errMsg))
            {
                PHSuit.Notification.Alert(this.Page, errMsg);
            }
            else
            {
                PHSuit.Notification.Alert(this.Page, "领取成功");
                BindList();
            }
        }

    }
}