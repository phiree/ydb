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
        NHibernateUnitOfWork.UnitOfWork.Start();
        base.OnPreInit(e);
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);
        NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
        //  NHibernateUnitOfWork.UnitOfWork.CurrentSession.Dispose();
    }
}