using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;

public partial class order_ceshi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindOrder();
        }
    }

    private void BindOrder()
    {

        BLLServiceOrder bllServiceOrder = new BLLServiceOrder();
        Guid orderId;
        orderId = new Guid("0846077e-164c-4c5b-870b-a4fe010fe205");
        IList<ServiceOrder> allServiceOrder=bllServiceOrder.GetListForBusiness(orderId);       
        gv.DataSource = allServiceOrder;
        //this.gv.DataKeyNames = new string[] { "Id" };
        gv.DataBind();
    }
}