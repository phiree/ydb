using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeiPayWeb
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //发起微信支付

            Response.Redirect("https://open.weixin.qq.com/connect/oauth2/authorize?appid=你的公众号appid&redirect_uri=http://你的网站/App/Pay/PayOrder.aspx?showwxpaytitle=1&response_type=code&scope=snsapi_base&state=" + OrderAmount + "_" + onlineOrder.OrderID + "#wechat_redirect");

        }

 
    }
}