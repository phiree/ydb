using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
public partial class membership_Default : System.Web.UI.Page
{
    DZMembershipProvider dzmp = new DZMembershipProvider();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindList(SortDirection.Ascending,"username");
        }
    }
 
     public SortDirection GetSortDirection(string SortExpression)
    {
        if (ViewState[SortExpression] == null)
            ViewState[SortExpression] =SortDirection.Ascending;
        else
            ViewState[SortExpression] =(SortDirection) ViewState[SortExpression] == SortDirection.Ascending ? SortDirection.Descending :SortDirection.Ascending;

        return (SortDirection) ViewState[SortExpression];
    }
    protected void gvMember_Sorting(object sender, GridViewSortEventArgs e)
    {
        SortDirection dirct = GetSortDirection(e.SortExpression);
        BindList(dirct, e.SortExpression);
    }

    private void BindList(SortDirection direction, string sortField)
    {
        long totalRecord=0;

        int currentPageIndex =1;
        string paramPage = Request.Params["page"];
        if (!string.IsNullOrEmpty(paramPage))
        {
            currentPageIndex = int.Parse(paramPage);
        }
        string cache_key = "ck_vmlist";
        string cache_key_vmcount = "ck_vmcount";
        IList<VMCustomer> vmList = new List<VMCustomer>();
        var cached_list = System.Web.HttpRuntime.Cache[cache_key];
        if (cached_list == null)
        {

            IList<DZMembership> list = dzmp.GetAllCustomer(currentPageIndex, pager.PageSize, out totalRecord);
            vmList = new VMCustomerAdapter().AdaptList(list);
            System.Web.HttpRuntime.Cache.Insert(cache_key, vmList);
            System.Web.HttpRuntime.Cache.Insert(cache_key_vmcount, totalRecord);
        }
        else
        {
            vmList = (IList<VMCustomer>)cached_list;
            totalRecord = (long)System.Web.HttpRuntime.Cache[cache_key_vmcount];
        }

      
        switch (sortField.ToLower())
        {
            case "username":
                vmList = direction == SortDirection.Ascending ? vmList.OrderBy(x => x.UserName).ToList() : vmList.OrderByDescending(x => x.UserName).ToList();
                break;
            case "phone":
                vmList = direction == SortDirection.Ascending ? vmList.OrderBy(x => x.Phone).ToList() : vmList.OrderByDescending(x => x.Phone).ToList();

                break;
            case "email":
                vmList = direction == SortDirection.Ascending ? vmList.OrderBy(x => x.Email).ToList() : vmList.OrderByDescending(x => x.Email).ToList();
                break;
            case "timecreated":
                vmList = direction == SortDirection.Ascending ? vmList.OrderBy(x => x.TimeCreated).ToList() : vmList.OrderByDescending(x => x.TimeCreated).ToList();
                break;
            case "logintimes":
                vmList = direction == SortDirection.Ascending ? vmList.OrderBy(x => x.LoginTimes).ToList() : vmList.OrderByDescending(x => x.LoginTimes).ToList();
                break;
            case "friendlyusertype":
                vmList = direction == SortDirection.Ascending ? vmList.OrderBy(x => x.FriendlyUserType).ToList() : vmList.OrderByDescending(x => x.FriendlyUserType).ToList();
                break;
            
            case "calltimes":
                vmList = direction == SortDirection.Ascending ? vmList.OrderBy(x => x.CallTimes).ToList() : vmList.OrderByDescending(x => x.CallTimes).ToList();
                break;
            case "orderamount":
                vmList = direction == SortDirection.Ascending ? vmList.OrderBy(x => x.OrderAmount).ToList() : vmList.OrderByDescending(x => x.OrderAmount).ToList();
                break;
            case "ordercount":
                vmList = direction == SortDirection.Ascending ? vmList.OrderBy(x => x.OrderCount).ToList() : vmList.OrderByDescending(x => x.OrderCount).ToList();
                break;
        }

        gvMember.DataSource = vmList;


        pager.RecordCount = (int)totalRecord;
        
        gvMember.DataBind();
    }
    
}