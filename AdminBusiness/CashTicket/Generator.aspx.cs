using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
public partial class CashTicket_Generator : BasePage
{
    BLLCashTicketTemplate bllCashTicketTemplate = new BLLCashTicketTemplate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            
        }
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        BLLCashTicket bllcashticket = new BLLCashTicket();
        BusinessUser businessuser = (BusinessUser)CurrentUser;
        Guid templateId=new Guid(Request.Params["templateid"]);
        CashTicketTemplate cashTicketTemplate=bllCashTicketTemplate.GetOne(templateId);
        string result=  bllcashticket.CreateBatch(businessuser.BelongTo, Convert.ToInt32(tbxTotal.Text.Trim()), cashTicketTemplate);
        Response.Write(result);

    }
}