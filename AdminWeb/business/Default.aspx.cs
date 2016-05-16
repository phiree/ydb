using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
public partial class business_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            BindBusinesses();
        }
    }

    private void BindBusinesses()
    {
        //BLLBusiness bllBusiness = new BLLBusiness();
        //IList<Business> allBusiness = bllBusiness.GetAll();
        //gvBusiness.DataSource = allBusiness;
        //gvBusiness.DataBind();

        long totalRecord;
        int currentPageIndex = 1;
        string paramPage = Request.Params["page"];
        if (!string.IsNullOrEmpty(paramPage))
        {
            currentPageIndex = int.Parse(paramPage);
        }
        BLLBusiness bllBusiness = new BLLBusiness();
        string query = "select b from Business b";
        IList<VMShop> allBusiness = new VMBusinessAdapter(new BLLServiceOrder()).
            AdaptList(bllBusiness.GetListByPage(currentPageIndex - 1, pager.PageSize, out totalRecord));
        gvBusiness.DataSource = allBusiness;
        //gvBusiness.DataBind();
        pager.RecordCount = (int)totalRecord;
        //gvBusiness.RowDataBound += new GridViewRowEventHandler(gvBusiness_RowDataBound);
        gvBusiness.DataBind();
    }

    //void gvBusiness_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        Literal litType = e.Row.FindControl("litType") as Literal;
    //        Business member = e.Row.DataItem as Business;

    //        if (member is Business)
    //        {
    //            Business memberBusiness = (Business)member;
    //            HyperLink hy = e.Row.FindControl("hlRelative") as HyperLink;
    //            litType.Text = "商家用户";
    //            hy.Text = memberBusiness.BelongTo.Name;
    //            hy.NavigateUrl = "/business/detail.aspx?id=" + memberBusiness.BelongTo.Id;
    //        }
    //    }
    //}
}