using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
public partial class DZService_Detail : BasePage
{
    public DZService CurrentService = new DZService();
    BLLDZService bllService = Bootstrap.Container.Resolve<BLLDZService>();
    protected void Page_Load(object sender, EventArgs e)
    {
        string strId = Request["serviceId"];
        if (!string.IsNullOrEmpty(strId))
        {
            CurrentService = bllService.GetOne(new Guid(strId));
        }
    }
}