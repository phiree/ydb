using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// BasePage 的摘要说明
/// </summary>
public class BasePage:System.Web.UI.Page
{
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);
        //  NHibernateUnitOfWork.UnitOfWork.CurrentSession.Dispose();
    }
}