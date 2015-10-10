using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Dianzhu.CSClient.MessageAdapter;
using log4net;
using Dianzhu.CSClient.WPFView;
using System.Deployment;
using System.Deployment.Application;

namespace Dianzhu.CSClient
{
    static class Program
    {
        
        static ILog log = LogManager.GetLogger("cs");
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
               AppDomain cDomain = AppDomain.CurrentDomain;
            cDomain.UnhandledException += new UnhandledExceptionEventHandler(cDomain_UnhandledException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            log4net.Config.XmlConfigurator.Configure();
           
            log.Debug("Start");
            bool? result;
            
            IMessageAdapter.IAdapter messageAdapter = new MessageAdapter.MessageAdapter(
                BLLFactory.BLLMember,BLLFactory.BLLDZService,BLLFactory.BLLServiceOrder);
            string version = GetVersion();
            XMPP.XMPP xmpp = new XMPP.XMPP(messageAdapter);
            var loginForm = new FormLogin();
            loginForm.FormText += "v"+version;
                Presenter.LoginPresenter loginPresenter = 
                new Presenter.LoginPresenter(loginForm,xmpp,
                    BLLFactory.BLLMember);
                  result = loginForm.ShowDialog();
            
            if (result.Value)// == DialogResult.OK)
            {
                var mainForm = new WinformView.FormMain();
                
                mainForm.Text += "v"+version;
                Presenter.MainPresenter MainPresenter = new Presenter.MainPresenter(
                    mainForm, xmpp, messageAdapter,
                    BLLFactory.BLLMember,
                    BLLFactory.BLLReception,
                    BLLFactory.BLLDZService,
                    BLLFactory.BLLServiceOrder,
                    BLLFactory.BLLRecetionStatus
                    );
                Application.Run(mainForm);
            }
            //Application.Run(new Views.Raw.ChatView());

            
        }

        static void cDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            log.Error(e.ExceptionObject.ToString());
            MessageBox.Show(e.ExceptionObject.ToString());
        }
        static string GetVersion()
        {
            Version myVersion = new Version();

            if (ApplicationDeployment.IsNetworkDeployed)
                myVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;
           return myVersion.ToString();
        }
    }
}
