using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Push;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
     
    }
    private void Push()
    {
        string appType = rblAppType.SelectedValue;
        string message = tbxMessage.Text;
        string deviceToken = tbxDeviceToken.Text;
        string orderId = tbxOrderId.Text;

        
        IPush push = PushFactory.Create( PushType.UserAndCustomerService, appType, orderId);
        push.Push(message, deviceToken);
    }


    protected void btnPush_Click(object sender, EventArgs e)
    {
        
        Push();
        tbxLog.Text += "推送完成:" + DateTime.Now + Environment.NewLine;
    }
}