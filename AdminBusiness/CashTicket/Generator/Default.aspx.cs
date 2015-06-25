using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
public partial class CashTicket_Generator_Default : BasePage
{
    BLLCashticketCreateRecord bllRecord = new BLLCashticketCreateRecord();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            DateTime now=DateTime.Now;
            BindList(now.Year, now.Month);
        }
    }
    private void BindList(int year,int month)
    {
        gv.DataSource = bllRecord.GetMonthRecord(CurrentBusiness, year, month);
        gv.DataBind();
    }
}