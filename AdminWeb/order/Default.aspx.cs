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
        IList<ServiceOrder> allServiceOrder;
        if(Request.QueryString["status"]==""|| Request.QueryString["status"] == null)
        { allServiceOrder = bllServiceOrder.GetAll().OrderByDescending(x => x.OrderCreated).ToList(); }
        else
        {
            StatusSelect.Value = "default.aspx?status="+Request.QueryString["status"].ToString();
            Dianzhu.Model.Enums.enum_OrderStatus status =(Dianzhu.Model.Enums.enum_OrderStatus)Enum.Parse(typeof(Dianzhu.Model.Enums.enum_OrderStatus), Request.QueryString["status"].ToString());
            allServiceOrder = bllServiceOrder.GetAllByOrderStatus(status).OrderByDescending(x => x.OrderCreated).ToList();
        }
       
        gv.DataSource = allServiceOrder;
        gv.AutoGenerateColumns = false;
        //this.gv.DataKeyNames = new string[] { "Id" };
        gv.DataBind();
        

        //BLLBusiness bllBusiness = new BLLBusiness();
        //IList<Business> allBusiness = bllBusiness.GetAll();
        //gvBusiness.DataSource = allBusiness;
        //gvBusiness.DataBind();
    }

    protected void pagechanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindOrder(); //重新绑定GridView数据的函数
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
        ServiceOrder order = null;
        BLLServiceOrder bllServiceOrder = new BLLServiceOrder();
        string id = gv.DataKeys[e.RowIndex].Values[0].ToString();
        order = bllServiceOrder.GetOne(new Guid(id));
        bllServiceOrder.Delete(order);
        BindOrder();

    }
}