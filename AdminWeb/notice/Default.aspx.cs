﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ydb.Notice.Application;
using Ydb.ApplicationService.Application.AgentService;
using N = Ydb.Notice.DomainModel;
using Ydb.InstantMessage.Application;
using Ydb.Common.Application;

public partial class notice_Default : System.Web.UI.Page
{
    INoticeService noticeService = Bootstrap.Container.Resolve<INoticeService>();
    IAgentNoticeService agentNoticeService = Bootstrap.Container.Resolve<IAgentNoticeService>();
    IInstantMessage im = Bootstrap.Container.Resolve<IInstantMessage>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindList(noticeService.GetAll());
        }
    }
    private void BindList(IList<N.Notice> list)
    {
        gvNotice.DataSource = list.OrderByDescending(x => x.TimeCreated);
        gvNotice.DataBind();
    }

    Guid noticeId;
    protected void gvNotice_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        noticeId = new Guid(e.CommandArgument.ToString());
        GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

        if (e.CommandName.ToLower() == "refuse")
        {
            string reason = ((TextBox)row.FindControl("tbxRefuseReason")).Text;
            noticeService.CheckRefuse(noticeId.ToString(), Guid.NewGuid().ToString(), reason);
          
        }
        else if (e.CommandName.ToLower() == "approve")
        {
            
            var im = (IInstantMessage)Application["im"];
            //openconnection
            ActionResult result = agentNoticeService.SendNotice(im, noticeId.ToString());
            // todo: authorId 应该是当前后台的登录用户.
            noticeService.CheckPass(noticeId.ToString(), Guid.NewGuid().ToString());
        }
        BindList(noticeService.GetAll());

    }
}