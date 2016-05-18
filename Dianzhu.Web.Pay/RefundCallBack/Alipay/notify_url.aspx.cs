using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Collections.Generic;
using Com.Alipay;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.Pay;

public partial class notify_url : System.Web.UI.Page
{


    log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Web.Pay");
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

}