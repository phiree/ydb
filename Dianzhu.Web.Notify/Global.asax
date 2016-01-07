<%@ Application Language="C#" %>

<script RunAt="server">
    
    Dianzhu.CSClient.IMessageAdapter.IAdapter adapter
            = new Dianzhu.CSClient.MessageAdapter.MessageAdapter();
   static log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.Web.Notify");
    void Application_Start(object sender, EventArgs e)
    {
        //Code that runs on application startup
        //init xmpp conenction 
        //防止网站被iis喀嚓,导致发送通知的用户从openfire掉线.
        _SetupRefreshJob();
        log4net.Config.XmlConfigurator.Configure();
        string server = Dianzhu.Config.Config.GetAppSetting("server");

        string server = Dianzhu.Config.Config.GetAppSetting("ImServer");

        Dianzhu.CSClient.IInstantMessage.InstantMessage im
            = new Dianzhu.CSClient.XMPP.XMPP(server, adapter, "YDB_IMServer");
        //login in
        string noticesenderId = Dianzhu.Config.Config.GetAppSetting("NoticeSenderId");
        string noticesenderPwdCrypted = Dianzhu.Config.Config.GetAppSetting("NoticeSenderPwd");
        string noticesenderPwd = PHSuit.Security.Decrypt(noticesenderPwdCrypted, false);

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
        log.Error("IMError:"+error);
    }
    void IMAuthError()
    { log.Error("IMAuthError"); }
    void IMConnectionError(string error)
    { 
        log.Error("ConnectionError:" + error);
    }
    void IMReceivedMessage(Dianzhu.Model.ReceptionChat chat)
    { 
        log.Debug("ReceiveMsg:"+adapter.ChatToMessage(chat,Dianzhu.Config.Config.GetAppSetting("server")).InnerXml);
    }
    void IMIQ()
    { 
        log.Debug("Received IQ");
    }
    void IMStreamError()
    {
       
        log.Error("StreamError"); }

    void Application_End(object sender, EventArgs e)
    {
        log.Error("ApplicationEnd"); 
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
         
        log.Error("ApplicationError:"+Server.GetLastError().Message);
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
    private static void _SetupRefreshJob()
    {

        //remove a previous job
        log.Debug("Begin Refres");
        Action remove = null;
        if (HttpContext.Current != null)
        {
            remove= HttpContext.Current.Cache["Refresh"] as Action;
        }
        if (remove is Action)
        {
            HttpContext.Current.Cache.Remove("Refresh");
            remove.EndInvoke(null);
        }

        //get the worker
        Action work = () =>
        {
            while (true)
            {
                System.Threading.Thread.Sleep(10000);
                System.Net.WebClient refresh = new System.Net.WebClient();
                try
                {
                    log.Debug("    Begin request");
                    refresh.UploadString("http://localhost:8039?refresh=1", string.Empty);
                }
                catch (Exception ex)
                {

                    log.Error(ex.Message);
                }
                finally
                {
                    refresh.Dispose();
                }
            }
        };

        log.Debug("Invoke.");
        work.BeginInvoke(null, null);

        //add this job to the cache
        if(HttpContext.Current!=null)
        { 
        HttpContext.Current.Cache.Add(
            "Refresh",
            work,
            null,
            Cache.NoAbsoluteExpiration,
            Cache.NoSlidingExpiration,
            CacheItemPriority.Normal,
            (s, o, r) => { _SetupRefreshJob(); }
            );
    }
    }

</script>
