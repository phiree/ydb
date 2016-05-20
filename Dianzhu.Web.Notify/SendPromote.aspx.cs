using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SendPromote : System.Web.UI.Page
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
       
        Uri uri;
        try
        {
            uri = new Uri(tbxContent.Text.Trim());
        }
        catch (UriFormatException ex)
        {
            lblResult.Text = "错误:不是有效的url.请重新输入";
            return;
        }

        notify.SendPromote(tbxContent.Text);
        lblResult.Text = "发送完成";
    }
}