using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;

public partial class order_Default : System.Web.UI.Page
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
        IList<ServiceOrder> allServiceOrder = bllServiceOrder.GetAll( );
        gv.DataSource = allServiceOrder;
        gv.DataBind();
        //BLLBusiness bllBusiness = new BLLBusiness();
        //IList<Business> allBusiness = bllBusiness.GetAll();
        //gvBusiness.DataSource = allBusiness;
        //gvBusiness.DataBind();
    }
}