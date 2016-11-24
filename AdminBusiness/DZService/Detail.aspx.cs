using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
using Ydb.BusinessResource.DomainModel;
using Ydb.BusinessResource.Application;
public partial class DZService_Detail : BasePage
{
    public DZService CurrentService = new DZService();
    IDZServiceService bllService = Bootstrap.Container.Resolve<IDZServiceService>();
    protected void Page_Load(object sender, EventArgs e)
    {
        string strId = Request["serviceId"];
        if (!string.IsNullOrEmpty(strId))
        {
            CurrentService = bllService.GetOne2(new Guid(strId));
        }
    }
}