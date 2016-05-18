<%@ Application Language="C#" %>
<%@ Import Namespace="Castle.Windsor" %>
<%@ Import Namespace="Castle.Windsor.Installer" %>
<script runat="server">

    //  Dianzhu.IDAL.IUnitOfWork uow = Installer.Container.Resolve<Dianzhu.IDAL.IUnitOfWork>();
    void Application_Start(object sender, EventArgs e)
    {
        // 在应用程序启动时运行的代码
        PHSuit.Logging.Config("Dianzhu.AdminWeb");
        //InitializeWindsor();
    }

    void Application_End(object sender, EventArgs e)
    {
        //  在应用程序关闭时运行的代码

    }

    void Application_Error(object sender, EventArgs e)
    {
        // 在出现未处理的错误时运行的代码

    }

    void Session_Start(object sender, EventArgs e)
    {
        // 在新会话启动时运行的代码


    }

    void Session_End(object sender, EventArgs e)
    {
        // 在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer
        // 或 SQLServer，则不引发该事件。


    }
    void Application_BeginRequest()
    {
        //  uow.BeginTransaction();
    }

    // Do all of your work ( Read, insert, update, delete )

    void Application_EndRequest()
    {
        //  uow.Commit();
        try
        {
            // UnitOfWork.Current.Transaction.Commit();
        }
        catch( Exception e )
        {
            // UnitOfWork.Current.Transaction.Rollback();
        }
    }
    private WindsorContainer _windsorContainer;

    private void InitializeWindsor()
    {
        _windsorContainer = new WindsorContainer();
        _windsorContainer.Install(FromAssembly.Containing<Dianzhu.DependencyInstaller.DianzhuDependencyInstaller>());
        _windsorContainer.Install(FromAssembly.This());

        //ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(_windsorContainer.Kernel));
    }
</script>
