using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
 
using System.Data;
using Ydb.Common;
using Ydb.Order.Application;
using Ydb.Order.DomainModel;



 
public partial class order_index : BasePage
{
    public IServiceOrderService bllServiceOrder = Bootstrap.Container.Resolve<IServiceOrderService>();
    ServiceOrder serviceorder;
    public int page;
    string linkStr;//链接字符串
    PagedDataSource pds;
    
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
       // UnitOfWork.Start();
         }
    protected override void OnUnload(EventArgs e)
    {
         base.OnUnload(e);
     //   UnitOfWork.Commit();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["status"] == "" || Request.QueryString["status"] == null)
        {
            linkStr = "index.aspx?";
        }
        else
        {
            linkStr = "index.aspx?status=" + Request.QueryString["status"].ToString() + "&";
        }
        if (!IsPostBack)
        {
            BindOrder();
        }
    }

    private void BindOrder()
    {
        string strPage= Request.QueryString["page"];
        if (string.IsNullOrEmpty(strPage))
        {
            page = 1;
        }
        else
        { 
        page = Convert.ToInt32(Request.QueryString["page"]);
        }
        IList<ServiceOrder> allServiceOrder;
        long totalRecords;
        if (Request.QueryString["status"] == "" || Request.QueryString["status"] == null)
        {
            allServiceOrder = bllServiceOrder.GetAll(page, pager.PageSize, out totalRecords ).OrderByDescending(x => x.LatestOrderUpdated).ToList();
        }
        else
        {

            StatusSelect.Value = "index.aspx?status=" + Request.QueryString["status"].ToString();
             enum_OrderStatus status = ( enum_OrderStatus)Enum.Parse(typeof( enum_OrderStatus), Request.QueryString["status"].ToString());
            allServiceOrder = bllServiceOrder.GetAllByOrderStatus(status,page,pager.PageSize,out totalRecords).OrderByDescending(x => x.LatestOrderUpdated).ToList();

        }
        pager.RecordCount = (int)totalRecords;
        //  pds = config.pds(allServiceOrder, page, 10);
        Repeater1.DataSource = allServiceOrder;
        Repeater1.DataBind();

    }
    //long totalRecord
    protected void data_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ServiceOrder so = (ServiceOrder)e.Item.DataItem;
            Repeater rptPayment = e.Item.FindControl("rptPayment") as Repeater;


        }

    

    }
    protected void delbt_Command(object sender, CommandEventArgs e)
    {
        Guid id = Guid.Parse(e.CommandArgument.ToString());
        serviceorder = bllServiceOrder.GetOne(id);
        bllServiceOrder.Delete(serviceorder);
        Response.Redirect(Request.UrlReferrer.ToString());
    }
    protected void ddlp_SelectedIndexChanged(object sender, EventArgs e)
    {//脚模板中的下拉列表框更改时激发
     //string pg = Convert.ToString((Convert.ToInt32(((DropDownList)sender).SelectedValue) - 1));//获取列表框当前选中项
        string pg = Convert.ToString((Convert.ToInt32(((DropDownList)sender).SelectedValue) - 1));
        Response.Redirect(linkStr + "page=" + pg);//页面转向
    }

    protected void alldel_Click(object sender, EventArgs e)
    {

        string idList = Request.Form["chbItem"];
        string[] ID = idList.Split(',');

        foreach (string Item in ID)
        {
            serviceorder = bllServiceOrder.GetOne(Guid.Parse(Item));
            bllServiceOrder.Delete(serviceorder);
        }
        Response.Redirect(Request.UrlReferrer.ToString());

    }
}