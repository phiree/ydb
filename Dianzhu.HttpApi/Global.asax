<%@ Application Language="C#" %>

<script runat="server">

    log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        PHSuit.Logging.Config("Dianzhu.HttpAPI");
        // var c = Installer.Container;
       // var container = Dianzhu.DependencyInstaller.Installer.Container;

       // Installer.InstallAPI(container);
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs


        Exception ex = Server.GetLastError();
        if (ex.InnerException != null)
        {
            log.Error("InnerException:" + ex.InnerException.Message);
        }
        log.Error(ex.Message);
    }



    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
