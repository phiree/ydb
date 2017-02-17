using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
using Ydb.BusinessResource.Application;
using Ydb.Order.Application;
using Ydb.Order.DomainModel;
public partial class business_ToExcel : BasePage
{
    IBusinessService bllBusiness = Bootstrap.Container.Resolve<IBusinessService>();
     IServiceOrderService orderService = Bootstrap.Container.Resolve<IServiceOrderService>();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            int intSize = int.Parse(Request.QueryString["intSize"].ToString());
            BindBusinesses(intSize);
        }
    }

    private void BindBusinesses(int intSize)
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
        
        string query = "select b from Business b";
        IList<VMShop> allBusiness = new VMBusinessAdapter(orderService).
            AdaptList(bllBusiness.GetListByPage(currentPageIndex - 1, intSize, out totalRecord));
        gvBusiness.DataSource = allBusiness;
        //gvBusiness.DataBind();
        //gvBusiness.RowDataBound += new GridViewRowEventHandler(gvBusiness_RowDataBound);
        gvBusiness.DataBind();
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

}