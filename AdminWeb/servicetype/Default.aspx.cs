﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using PHSuit;
using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;

public partial class servicetype_Default : BasePage
{
     
    IServiceTypeService bllServiceType =  Bootstrap.Container.Resolve<IServiceTypeService>();
    Ydb.Finance.Application.IServiceTypePointService bllPoint = Bootstrap.Container.Resolve<Ydb.Finance.Application.IServiceTypePointService>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Bind();
            BindTop();
        }
    }
    
    private void BindTop()
    {
       
       rpt.DataSource = bllServiceType.GetTopList();
       rpt.ItemDataBound += new RepeaterItemEventHandler(r_ItemDataBound);
       rpt.DataBind();
    }
    string[] colors = new string[] {"000","333","666","999","ccc"};
    void r_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        ServiceType st = e.Item.DataItem as ServiceType;
        
       
        HyperLink hy = e.Item.Controls[1] as HyperLink;
        hy.NavigateUrl = "edit.aspx?id=" + st.Id;
        hy.Style.Add("font-size", 24 - (st.DeepLevel * 2) + "px");
        hy.Style.Add("color","#"+colors[st.DeepLevel] +"");

         

        hy.Text = st.Name;
        if (st.Children.Count > 0)
        {
            Repeater rt = e.Item.Controls[3] as Repeater;
            rt.DataSource = st.Children;
            rt.ItemDataBound += new RepeaterItemEventHandler(r_ItemDataBound);
            rt.DataBind();

        }
    }
    private void Bind()
    {
        //gvServiceType.DataSource = bllServiceType.GetAll();
       // gvServiceType.DataBind();
    }



}