using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;

public partial class DZService_ServiceShelf : BasePage
{

    public DZService CurrentService = new DZService();
    IDZServiceService bllServcie = Bootstrap.Container.Resolve<IDZServiceService>();

    public string merchantID {
        get {
            return System.Web.Security.Membership.GetUser().ProviderUserKey.ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string strId = Request["serviceId"];
        if (!string.IsNullOrEmpty(strId))
        {
            CurrentService = bllServcie.GetOne2(new Guid(strId));
        }
    }
}