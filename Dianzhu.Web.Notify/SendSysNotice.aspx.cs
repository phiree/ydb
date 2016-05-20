using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SendSysNotice : System.Web.UI.Page
{
    Dianzhu.NotifyCenter.IMNotify notify = Installer.Container.Resolve<Dianzhu.NotifyCenter.IMNotify>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblResult.Text = string.Empty;
        }
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
      
        notify.SendSysNoitification(tbxContent.Text);
        lblResult.Text = "发送完成";
    }
}