using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;

public partial class DZService_ServiceShelf : BasePage
{

    public DZService CurrentService = new DZService();
    BLLDZService bllServcie = Bootstrap.Container.Resolve<BLLDZService>();

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
            CurrentService = bllServcie.GetOne(new Guid(strId));
        }
    }
}