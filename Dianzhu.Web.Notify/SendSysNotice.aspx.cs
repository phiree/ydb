﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SendSysNotice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblResult.Text = string.Empty;
        }
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        Dianzhu.NotifyCenter.IMNotify notify = Bootstrap.Container.Resolve<Dianzhu.NotifyCenter.IMNotify>();
        notify.SendSysNoitification(tbxContent.Text);
        lblResult.Text = "发送完成";
    }
}