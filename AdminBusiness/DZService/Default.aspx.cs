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
        string id = Request.Params["id"];
        
        if (!IsPostBack)
        {
           
             
            BindList();
        }

    }
    private void BindList()
    {
        int totalRecords;
        //处理太简单粗暴,需要优化.
      IList<DZService> serviceList=  bllService.GetServiceByBusiness(((BusinessUser)CurrentUser).BelongTo.Id, Guid.Empty, 0, 999, out totalRecords);
      if (serviceList.Count > 0 && string.IsNullOrEmpty(Request.Params["id"]))
      {
          Response.Redirect(Request.RawUrl + "?id=" + serviceList.Last<DZService>().Id,true);
      }
        rptServiceList.DataSource = bllService.GetServiceByBusiness(((BusinessUser)CurrentUser).BelongTo.Id, Guid.Empty, 0, 999, out totalRecords);
        rptServiceList.DataBind();
    }
}