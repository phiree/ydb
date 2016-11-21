using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
public partial class DZService_ServiceOverview :BasePage
{
    dzServiceService bllService = Bootstrap.Container.Resolve<dzServiceService>();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {


            BindList();
        }

    }
    protected void delbt_Command(object sender, CommandEventArgs e)
    {
        Guid id = new Guid(e.CommandArgument.ToString());
        bllService.Delete(bllService.GetOne(id));
        Response.Redirect(Request.UrlReferrer.ToString());

    }
    private void BindList()
    {
        int totalRecords;
        //处理太简单粗暴,需要优化.
        rptServiceList.DataSource = bllService.GetServiceByBusiness(CurrentBusiness.Id, 0, 999, out totalRecords);
        rptServiceList.DataBind();
    }
}