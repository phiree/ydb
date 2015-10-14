using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class error : System.Web.UI.Page
{
    Dictionary<string, string> ErrMsg = new Dictionary<string, string>();
    log4net.ILog log = log4net.LogManager.GetLogger("dzpay");
    protected override void OnInit(EventArgs e)
    {
        ErrMsg.Add("1", "订单编号有误");
        ErrMsg.Add("2", "没有这个订单");
        ErrMsg.Add("3", "订单状态有误.");
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string errCode = Request["err"];
       
        if (ErrMsg.ContainsKey(errCode))
        {
            string errMsg = ErrMsg[errCode];
            log.Error(errCode+":" +errMsg);
            Response.Write(errMsg);
        }
        else
        {
            Response.Write("未知错误");
        }
    }
}