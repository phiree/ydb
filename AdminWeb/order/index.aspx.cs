using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
using System.Data;

public partial class order_index : System.Web.UI.Page
{
  //  Dianzhu.IDAL.IUnitOfWork iuow = Installer.Container.Resolve<Dianzhu.IDAL.IUnitOfWork>();
    public IBLLServiceOrder bllServiceOrder = Installer.Container.Resolve<IBLLServiceOrder>();
    ServiceOrder serviceorder;
    public int page;
    string linkStr;//链接字符串
    PagedDataSource pds;
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
      //  iuow.BeginTransaction();
    }
    protected override void OnUnload(EventArgs e)
    {
       // iuow.Commit();
        base.OnUnload(e);
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
        page = Convert.ToInt32(Request.QueryString["page"]);
        IList<ServiceOrder> allServiceOrder;
        if (Request.QueryString["status"] == "" || Request.QueryString["status"] == null)
        {
            allServiceOrder = bllServiceOrder.GetAll().OrderByDescending(x => x.OrderCreated).ToList();
        }
        else
        {

            StatusSelect.Value = "index.aspx?status=" + Request.QueryString["status"].ToString();
            Dianzhu.Model.Enums.enum_OrderStatus status = (Dianzhu.Model.Enums.enum_OrderStatus)Enum.Parse(typeof(Dianzhu.Model.Enums.enum_OrderStatus), Request.QueryString["status"].ToString());
            allServiceOrder = bllServiceOrder.GetAllByOrderStatus(status).OrderByDescending(x => x.OrderCreated).ToList();

        }
        pds = config.pds(allServiceOrder, page, 10);
        Repeater1.DataSource = pds;
        Repeater1.DataBind();

    }
    BLLPayment bllPayment = new BLLPayment();
    protected void data_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ServiceOrder so = (ServiceOrder)e.Item.DataItem;
            Repeater rptPayment = e.Item.FindControl("rptPayment") as Repeater;


        }

        if (e.Item.ItemType == ListItemType.Footer)
        {
            DropDownList ddlp = (DropDownList)e.Item.FindControl("ddlp");
            HyperLink lpfirst = (HyperLink)e.Item.FindControl("hlfir");
            HyperLink lpprev = (HyperLink)e.Item.FindControl("hlp");
            HyperLink lpnext = (HyperLink)e.Item.FindControl("hln");
            HyperLink lplast = (HyperLink)e.Item.FindControl("hlla");

            int n = Convert.ToInt32(pds.PageCount);//n为分页数
            int i = Convert.ToInt32(pds.CurrentPageIndex);//i为当前页

            Label lblpc = (Label)e.Item.FindControl("lblpc");
            lblpc.Text = n.ToString();
            Label lblp = (Label)e.Item.FindControl("lblp");
            lblp.Text = Convert.ToString(pds.CurrentPageIndex + 1);
            if (!IsPostBack)
            {
                for (int j = 0; j < n; j++)
                {
                    ddlp.Items.Add(Convert.ToString(j + 1));
                }
            }

            if (i <= 0)
            {
                lpfirst.Enabled = false;
                lpprev.Enabled = false;
                lplast.Enabled = true;
                lpnext.Enabled = true;
            }
            else
            {
                lpprev.NavigateUrl = linkStr + "page=" + (i - 1);
            }
            if (i >= n - 1)
            {
                lpfirst.Enabled = true;
                lplast.Enabled = false;
                lpnext.Enabled = false;
                lpprev.Enabled = true;
            }
            else
            {
                lpnext.NavigateUrl = linkStr + "page=" + (i + 1);
            }

            lpfirst.NavigateUrl = linkStr + "page=0";//向本页传递参数page
            lplast.NavigateUrl = linkStr + "page=" + (n - 1);

            ddlp.SelectedIndex = Convert.ToInt32(pds.CurrentPageIndex);//更新下拉列表框中的当前选中页序号
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