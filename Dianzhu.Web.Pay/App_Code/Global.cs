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
    static log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Web.Pay");
    void Application_Start(object sender, EventArgs e)
    {
        //PHSuit.Logging.Config("Dianzhu.Web.Pay");
        //Ydb.Common.LoggingConfiguration.Config("mongodb://112.74.198.215/");
        Bootstrap.Boot();


    }
    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

        log.Error("ApplicationError:" + Server.GetLastError().ToString());
    }
    protected void Application_BeginRequest(object sender, EventArgs e)
    {
       // NHibernateUnitOfWork.UnitOfWork.Start();
    }

    protected void Application_EndRequest(object sender, EventArgs e)
    {
      //  NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
    }
}