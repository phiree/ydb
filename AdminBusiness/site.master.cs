﻿using System;

public partial class site : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BrowserCheck.CheckVersion();

    }
}
