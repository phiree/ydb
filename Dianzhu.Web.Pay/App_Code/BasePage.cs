using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// BasePage 的摘要说明
/// </summary>
public class BasePage:System.Web.UI.Page
{
    public BasePage()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        NHibernateUnitOfWork.UnitOfWork.Start();
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
    }
}