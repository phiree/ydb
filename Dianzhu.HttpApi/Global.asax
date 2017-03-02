<%@ Application Language="C#" %>

<script runat="server">

    log4net.ILog log = log4net.LogManager.GetLogger("Ydb.HttpApi");

    void Application_Start(object sender, EventArgs e)
    {

       
        Bootstrap.Boot();
        log.Debug("begin");
 
    }



    void Application_Error(object sender, EventArgs e)
    {

        Exception ex = Server.GetLastError();
         
        log.Error(ex.ToString());
    }


</script>
