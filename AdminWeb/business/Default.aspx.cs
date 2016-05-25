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

    /// <summary>
    /// 点击表头排序
    /// </summary>
    /// <param ></param>
    private void BindBusinesses(SortDirection direction, string sortField)
    {
        long totalRecord = 0;

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

        switch (sortField.ToLower())
        {
            case "businessname":
                allBusiness = direction == SortDirection.Ascending ? allBusiness.OrderBy(x => x.BusinessName).ToList() : allBusiness.OrderByDescending(x => x.BusinessName).ToList();
                break;
            case "cityname":
                allBusiness = direction == SortDirection.Ascending ? allBusiness.OrderBy(x => x.CityName).ToList() : allBusiness.OrderByDescending(x => x.CityName).ToList();

                break;
            case "score":
                allBusiness = direction == SortDirection.Ascending ? allBusiness.OrderBy(x => x.Score).ToList() : allBusiness.OrderByDescending(x => x.Score).ToList();
                break;
            case "servicetypesdisplay":
                allBusiness = direction == SortDirection.Ascending ? allBusiness.OrderBy(x => x.ServiceTypesDisplay).ToList() : allBusiness.OrderByDescending(x => x.ServiceTypesDisplay).ToList();
                break;
            case "registertime":
                allBusiness = direction == SortDirection.Ascending ? allBusiness.OrderBy(x => x.RegisterTime).ToList() : allBusiness.OrderByDescending(x => x.RegisterTime).ToList();
                break;
            case "ordercount":
                allBusiness = direction == SortDirection.Ascending ? allBusiness.OrderBy(x => x.OrderCount).ToList() : allBusiness.OrderByDescending(x => x.OrderCount).ToList();
                break;

            case "ordercompletecount":
                allBusiness = direction == SortDirection.Ascending ? allBusiness.OrderBy(x => x.OrderCompleteCount).ToList() : allBusiness.OrderByDescending(x => x.OrderCompleteCount).ToList();
                break;
            case "ordercancelcount":
                allBusiness = direction == SortDirection.Ascending ? allBusiness.OrderBy(x => x.OrderCancelCount).ToList() : allBusiness.OrderByDescending(x => x.OrderCancelCount).ToList();
                break;
            

        }

        gvBusiness.DataSource = allBusiness;
        //gvBusiness.DataBind();
        pager.RecordCount = (int)totalRecord;
        //gvBusiness.RowDataBound += new GridViewRowEventHandler(gvBusiness_RowDataBound);
        gvBusiness.DataBind();
    }

    /// <summary>
    /// 排序绑定
    /// </summary>
    /// <param ></param>
    protected void gvBusiness_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortDirection dirct = GetSortDirection(e.SortExpression);
        BindBusinesses(dirct, e.SortExpression);
    }

    /// <summary>
    /// 排序方式记录
    /// </summary>
    /// <param ></param>
    public SortDirection GetSortDirection(string SortExpression)
    {
        if (ViewState[SortExpression] == null)
            ViewState[SortExpression] = SortDirection.Ascending;
        else
            ViewState[SortExpression] = (SortDirection)ViewState[SortExpression] == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;

        return (SortDirection)ViewState[SortExpression];
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