<%@ Application Language="C#" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        //_SetupRefreshJob();
        //// Code that runs on application startup
        // PHSuit.Logging.Config();
        //Dianzhu.CSClient.IMessageAdapter.IAdapter adapter = new Dianzhu.CSClient.MessageAdapter.MessageAdapter();
        //Dianzhu.CSClient.IInstantMessage.InstantMessage im = new Dianzhu.CSClient.XMPP.XMPP(adapter);

        ////im.OpenConnection("4f088d5c-be94-43bc-9644-a4d1008be129", "123456");
        //Application["IM"] = im;
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }


    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

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
    //keep site alive
    private static void _SetupRefreshJob()
    {

        //remove a previous job
        Action remove = HttpContext.Current.Cache["Refresh"] as Action;
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
                    refresh.UploadString("http://localhost:8168", string.Empty);
                }
                catch (Exception ex)
                {
                    log4net.ILog log = log4net.LogManager.GetLogger("error");
                    log.Error(ex.Message);
                }
                finally
                {
                    refresh.Dispose();
                }
            }
        };
        log4net.ILog log2 = log4net.LogManager.GetLogger("debug");
        log2.Debug("Invoke.");
        work.BeginInvoke(null, null);

        //add this job to the cache
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

</script>
