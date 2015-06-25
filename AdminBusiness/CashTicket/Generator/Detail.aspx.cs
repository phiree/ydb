using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
public partial class CashTicket_Generator_Detail : System.Web.UI.Page
{
    BLLCashticketCreateRecord bllRecord = new BLLCashticketCreateRecord();
    BLLCashTicket bllCashTicket = new BLLCashTicket();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string recordId = Request.Params["id"];
            if (!string.IsNullOrEmpty(recordId))
            {
                Guid id = new Guid(recordId);
                CashTicketCreateRecord record = bllRecord.GetOne(id);
                BindList(record.CashTickets);
            }
            
        }
    }
    
    private void BindList(IList<CashTicket> list)
    {
        gvDetail.DataSource = list;
        gvDetail.DataBind();
    }
}