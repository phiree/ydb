using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Pay;
using Dianzhu.BLL;

public partial class Pay_BatchTest : System.Web.UI.Page
{
    BLLPay bllPay = Bootstrap.Container.Resolve<BLLPay>();
    Dianzhu.BLL.Common.SerialNo.ISerialNoBuilder iserialno = Bootstrap.Container.Resolve<Dianzhu.BLL.Common.SerialNo.ISerialNoBuilder>();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        IPayRequest pay = bllPay.CreatePayBatch(2, iserialno.GetSerialNo("PAYB"+DateTime.Now.ToString("yyyyMMddHHmm")), "");
        string requestString = pay.CreatePayRequest();
        Response.Write(requestString);
    }
}