using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiPay;

namespace WeiPayWeb
{
    public partial class ceshi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string code_url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", PayConfig.AppId, PayConfig.AppSecret);
            //Response.Redirect(code_url);
            string returnStr = HttpUtil.Send("", code_url);
            Response.Write(returnStr);
        }
    }
}