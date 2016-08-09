using PHSuit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class servicetype_Import : BasePage
{
    Dianzhu.BLL.BLLServiceType bllServiceType = Bootstrap.Container.Resolve<Dianzhu.BLL.BLLServiceType>();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {

        bllServiceType.Import(fu.PostedFile.InputStream);
        Notification.Show(Page, "", "导入成功", string.Empty);
        lblMsg.Text = "导入完成";
    }
}