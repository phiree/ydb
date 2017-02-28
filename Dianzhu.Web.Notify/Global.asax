<%@ Application Language="C#" %>
<%@ Import Namespace="Ydb.InstantMessage.DomainModel.Chat" %>
<%@ Import Namespace="Ydb.Common.Infrastructure" %>

<script RunAt="server">

    static log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Web.Notify");
void Application_Start(object sender, EventArgs e)
{
    Bootstrap.Boot();
    //Code that runs on application startup
    //init xmpp conenction
    //防止网站被iis喀嚓,导致发送通知的用户从openfire掉线.
    //PHSuit.Logging.Config("Dianzhu.Web.Notify");
    string host = System.Net.Dns.GetHostName();
    PHSuit.HttpHelper._SetupRefreshJob(8039);
    Ydb.InstantMessage.Application.IInstantMessage im = Bootstrap.Container.Resolve<Ydb.InstantMessage.Application.IInstantMessage>();

    //= new Dianzhu.CSClient.XMPP.XMPP(server, domain,adapter, Dianzhu.enum_XmppResource.YDBan_IMServer.ToString());
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
    im.OpenConnection(noticesenderId, noticesenderPwd, "YDBan_IMServer");
    Application["IM"] = im;
}

void IMClosed()
{
    string emails = ConfigurationManager.AppSettings["MonitorEmails"];

    try
    {

        if (string.IsNullOrEmpty(emails)) { return; }
        string[] emailList = emails.Split(',');
        IEmailService emailService = Bootstrap.Container.Resolve<IEmailService>();
        emailService.SendEmail(emailList[0], "异常_" + log.Logger.Name, "IMServer掉线了",
          emailList);
    }
    catch (Exception ex)
    {
        log.Error(ex.ToString());
    }
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
void IMReceivedMessage(ReceptionChatDto chat)
{
    log.Debug("ReceiveMsg:" + chat.ToString());
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