using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
using System.Web.Security;
public partial class register : BasePage
{
    BLLBusiness bllBusiness = Bootstrap.Container.Resolve<BLLBusiness>();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["send"].ToLower() == "false")
        {
            lblSendError.Text = "验证邮件发送失败,您可以在账号安全页面重新发送<br/>";
        }
        if (Request.IsLocal)
        {
            lblSendError.Text += "本地测试,禁用邮箱发送功能";
        }
    }

    
    
    
}