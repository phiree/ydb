<%@ Application Language="C#" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {

        PHSuit.Logging.Config("Dianzhu.Web.Pay");
        Bootstrap.Boot();
    }
 
    //keep site alive
    

</script>
