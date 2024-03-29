﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ydb.Common;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
using Ydb.InstantMessage.Application;
public partial class membership_Default : BasePage
{
    IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindList(SortDirection.Ascending,"username");
        }
        BindSummary();
    }

    private void BindSummary()
    {
        IIMUserStatusService bllIMUS = Bootstrap.Container.Resolve<IIMUserStatusService>();
       var onlineUser= bllIMUS.GetOnlineListByClientName(enum_XmppResource.YDBan_User.ToString());
        lblTotalOnline.Text = onlineUser.Count.ToString();
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
        if (!config.EnableCache || cached_list == null)
        {
            var VMCustomerAdapter = Bootstrap.Container.Resolve<VMCustomerAdapter>();
            IList<MemberDto> list = memberService.GetAllCustomer(currentPageIndex, pager.PageSize, out totalRecord);
            vmList = VMCustomerAdapter.AdaptList(list);
            System.Web.HttpRuntime.Cache.Insert(cache_key, vmList);
            System.Web.HttpRuntime.Cache.Insert(cache_key_vmcount, totalRecord);
        }
        else
        {
            vmList = (IList<VMCustomer>)cached_list;
            totalRecord = (long)System.Web.HttpRuntime.Cache[cache_key_vmcount];
           
        }
        lblTotalRegister.Text = totalRecord.ToString();

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
            case "logindates":
                vmList = direction == SortDirection.Ascending ? vmList.OrderBy(x =>x.LoginDates).ToList() : vmList.OrderByDescending(x => x.LoginDates).ToList();
                break;
            case "loginrate":
                vmList = direction == SortDirection.Ascending ? vmList.OrderBy(x => x.LoginRate).ToList() : vmList.OrderByDescending(x => x.LoginRate).ToList();
                break;
             
        }

        gvMember.DataSource = vmList;


        pager.RecordCount = (int)totalRecord;
        
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

    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ToExcel.aspx?intSize="+pager.RecordCount.ToString ());
    }
}