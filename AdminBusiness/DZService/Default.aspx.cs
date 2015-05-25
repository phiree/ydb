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
    private void BindList()
    {
        int totalRecords;
        gvServices.DataSource = bllService.GetServiceByBusiness(((BusinessUser)CurrentUser).BelongTo.Id, Guid.Empty, 0, 999, out totalRecords);
        gvServices.DataBind();
    }
}