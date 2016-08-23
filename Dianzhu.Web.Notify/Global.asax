<%@ Application Language="C#" %>

<script RunAt="server">

    Dianzhu.CSClient.IMessageAdapter.IAdapter adapter
            = new Dianzhu.CSClient.MessageAdapter.MessageAdapter();
    static log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Web.Notify");
    void Application_Start(object sender, EventArgs e)
    {
        Bootstrap.Boot();
        //Code that runs on application startup
        //init xmpp conenction 
        //防止网站被iis喀嚓,导致发送通知的用户从openfire掉线.
        PHSuit.Logging.Config("Dianzhu.Web.Notify");
        string host= System.Net.Dns.GetHostName();
         PHSuit.HttpHelper. _SetupRefreshJob(8039);
        string server = Dianzhu.Config.Config.GetAppSetting("ImServer");
        string domain = Dianzhu.Config.Config.GetAppSetting("ImDomain");

        Dianzhu.CSClient.IInstantMessage.InstantMessage im
            = Bootstrap.Container.Resolve<Dianzhu.CSClient.IInstantMessage.InstantMessage>
            //();
            (new { resourceName = Dianzhu.Model.Enums.enum_XmppResource.YDBan_IMServer.ToString() });
        //= new Dianzhu.CSClient.XMPP.XMPP(server, domain,adapter, Dianzhu.Model.Enums.enum_XmppResource.YDBan_IMServer.ToString());
        //login in
        string noticesenderId = Dianzhu.Config.Config.GetAppSetting("NoticeSenderId");

        string noticesenderPwd = Dianzhu.Config.Config.GetAppSetting("NoticeSenderPwd");

        im.IMClosed += IMClosed;
        im.IMLogined += IMLogined;
        im.IMError += IMError;
        im.IMAuthError += IMAuthError;
        im.IMConnectionError += IMConnectionError;
        im.IMReceivedMessage += IMReceivedMessage;
        im.IMIQ += IMIQ;
        im.IMStreamError += IMStreamError;
        im.OpenConnection(noticesenderId, noticesenderPwd);
        Application["IM"] = im;
    }



    void IMClosed()
    {
        log.Warn("Closed");
    }
    void IMLogined(string jidUser)
    {
        log.Info("Logined:" + jidUser);
    }
    void IMError(string error)
    {
        log.Error("IMError:" + error);
    }
    void IMAuthError()
    {
        log.Error("IMAuthError");
    }
    void IMConnectionError(string error)
    {
        log.Error("ConnectionError:" + error);
    }
    void IMReceivedMessage(Dianzhu.Model.ReceptionChat chat)
    {
        log.Debug("ReceiveMsg:" + adapter.ChatToMessage(chat, Dianzhu.Config.Config.GetAppSetting("ImServer")).InnerXml);
    }
    void IMIQ()
    {
        log.Debug("Received IQ");
    }
    void IMStreamError()
    {

        log.Error("StreamError");
    }

    void Application_End(object sender, EventArgs e)
    {
        log.Error("ApplicationEnd");
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

        log.Error("ApplicationError:" + Server.GetLastError().Message);
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
    void Application_BeginRequest(Object source, EventArgs e)
    {
        //HttpApplication app = (HttpApplication)source;
        //PHSuit.FirstRequestInitialisation.Initialise(app.Context);

        //NHibernateUnitOfWork.UnitOfWork.Start();
    }
    void Application_EndRequest(object sender, EventArgs e)
    {
        //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
    }
     

</script>
