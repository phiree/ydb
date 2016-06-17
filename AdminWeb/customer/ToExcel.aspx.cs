using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;

public partial class customer_ToExcel : System.Web.UI.Page
{
    DZMembershipProvider dzmp = Bootstrap.Container.Resolve<DZMembershipProvider>();
    VMCustomerAdapter vmcAdapter = Bootstrap.Container.Resolve<VMCustomerAdapter>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int intSize = int.Parse(Request.QueryString["intSize"].ToString());
            BindList(intSize);
        }
        BindSummary();
    }

    private void BindSummary()
    {
        BLLIMUserStatus bllIMUS = Bootstrap.Container.Resolve<BLLIMUserStatus>();
        var onlineUser = bllIMUS.GetOnlineListByClientName(Dianzhu.Model.Enums.enum_XmppResource.YDBan_User.ToString());
        lblTotalOnline.Text = onlineUser.Count.ToString();
    }

    public SortDirection GetSortDirection(string SortExpression)
    {
        if (ViewState[SortExpression] == null)
            ViewState[SortExpression] = SortDirection.Ascending;
        else
            ViewState[SortExpression] = (SortDirection)ViewState[SortExpression] == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;

        return (SortDirection)ViewState[SortExpression];
    }
   

    private void BindList(int intSize)
    {
        long totalRecord = 0;

        int currentPageIndex = 1;
        string paramPage = Request.Params["page"];
        if (!string.IsNullOrEmpty(paramPage))
        {
            currentPageIndex = int.Parse(paramPage);
        }
        string cache_key = "ck_vmlist";
        string cache_key_vmcount = "ck_vmcount";
        IList<VMCustomer> vmList = new List<VMCustomer>();
        var cached_list = System.Web.HttpRuntime.Cache[cache_key];
        if (!config.EnableCache || cached_list == null)
        {

            IList<DZMembership> list = dzmp.GetAllCustomer(currentPageIndex, intSize, out totalRecord);
            vmList = vmcAdapter.AdaptList(list);
            System.Web.HttpRuntime.Cache.Insert(cache_key, vmList);
            System.Web.HttpRuntime.Cache.Insert(cache_key_vmcount, totalRecord);
        }
        else
        {
            vmList = (IList<VMCustomer>)cached_list;
            totalRecord = (long)System.Web.HttpRuntime.Cache[cache_key_vmcount];

        }
        lblTotalRegister.Text = totalRecord.ToString();
        
        gvMember.DataSource = vmList;

        

        gvMember.DataBind();
    }


    protected void gvMember_DataBound(object sender, EventArgs e)
    {

    }

    protected void gvMember_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

}