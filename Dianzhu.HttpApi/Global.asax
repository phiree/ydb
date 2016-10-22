<%@ Application Language="C#" %>

<script runat="server">

    log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.HttpApi");

    void Application_Start(object sender, EventArgs e)
    {

        PHSuit.Logging.Config("Dianzhu.HttpAPI");

        Bootstrap.Boot();
 
    }



    void Application_Error(object sender, EventArgs e)
    {

        Exception ex = Server.GetLastError();
        if (ex.InnerException != null)
        {
            log.Error("InnerException:" + ex.InnerException.Message);
        }
        log.Error(ex.Message);
    }


</script>
