using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Windsor;
/// <summary>
/// Global 的摘要说明
/// </summary>
public class Global:HttpApplication 
{
    
    void Application_Start(object sender, EventArgs e)
    {
        PHSuit.Logging.Config("Dianzhu.Web.Pay");
        Bootstrap.Boot();


    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        NHibernateUnitOfWork.UnitOfWork.Start();
    }

    protected void Application_EndRequest(object sender, EventArgs e)
    {
        NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
    }
}