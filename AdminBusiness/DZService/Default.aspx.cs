using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
public partial class DZService_Default : BasePage
{
    BLLDZService bllService = new BLLDZService();

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
           
             
            BindList();
        }

    }
    protected void delbt_Command(object sender, CommandEventArgs e)
    {
        Guid id =new Guid(e.CommandArgument.ToString());
        bllService.Delete(bllService.GetOne(id));
        Response.Redirect(Request.UrlReferrer.ToString());

    }
    private void BindList()
    {
        string strIndex = Request["page"];
        int index = 1;
        if (!string.IsNullOrEmpty(strIndex))
        {
            index = int.Parse(strIndex);
        }
        int totalRecords;
       
     rptServiceList.DataSource = bllService.GetServiceByBusiness(CurrentBusiness.Id, index, pager.PageSize, out totalRecords);
     pager.RecordCount = totalRecords;
        rptServiceList.DataBind();
    }
    
}