﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;

public partial class DZService_Default : BasePage
{
    IDZServiceService bllService = Bootstrap.Container.Resolve<IDZServiceService>();

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
           
             
            BindList();
        }

    }
    protected void delbt_Command(object sender, CommandEventArgs e)
    {
        Guid id =new Guid(e.CommandArgument.ToString());
        bllService.Delete(bllService.GetOne2(id));
        if (NHibernateUnitOfWork.UnitOfWork.Current != null)
        {
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
        }
        Response.Redirect(Request.UrlReferrer.ToString());

    }
    private void BindList()
    {
        int totalRecords;
        //处理太简单粗暴,需要优化.
 
     rptServiceList.DataSource = bllService.GetServiceByBusiness(CurrentBusiness.Id, 0, 999, out totalRecords);
        System.Diagnostics.Debug.WriteLine("----------------------------------");
 
        rptServiceList.ItemDataBound += RptServiceList_ItemDataBound;
        rptServiceList.DataBind();
    }

    private void RptServiceList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DZService service = e.Item.DataItem as DZService;

            System.Diagnostics.Debug.WriteLine(service.Enabled);
        }
    }
}