using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Dianzhu.CSClient.MessageAdapter;
using log4net;
using WPF=Dianzhu.CSClient.ViewWPF;
using System.Deployment;
using System.Deployment.Application;
using System.Threading;
using System.ComponentModel;
using Dianzhu.BLL;
namespace Dianzhu.CSClient
{
    static class Program
    {
        
        static ILog log = LogManager.GetLogger("Dianzhu.CSClient");
       
        static int progressPercent = 0;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //systemconfig
            AppDomain cDomain = AppDomain.CurrentDomain;
            cDomain.UnhandledException += new UnhandledExceptionEventHandler(cDomain_UnhandledException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //log
             PHSuit.Logging.Config("Dianzhu.CSClient");
            log.Debug("Start");
            
            //prepare parameters for IM instance's constructor
            //init messageadapter
            IMessageAdapter.IAdapter messageAdapter = new MessageAdapter.MessageAdapter(
                 );
            //get im server config
            string server = Config.Config.GetAppSetting("ImServer");
            string domain= Config.Config.GetAppSetting("ImDomain");

            IInstantMessage.InstantMessage xmpp = new XMPP.XMPP(server,domain, messageAdapter, Model.Enums.enum_XmppResource.YDBan_Win_CustomerService.ToString());


            var loginForm = new WPF.FormLogin();
            string version = GetVersion();
            loginForm.FormText += "v" + version;
            Presenter.LoginPresenter loginPresenter =
            new Presenter.LoginPresenter(loginForm, xmpp);
            bool? result = loginForm.ShowDialog();

            if (result.Value)// == DialogResult.OK)
            {
                if (false)
                {
                    var mainForm = new WPF.FormMain();

                    mainForm.Title += "v" + version;
                    Presenter.MainPresenter MainPresenter = new Presenter.MainPresenter(
                        mainForm, xmpp, messageAdapter
                        //BLLFactory.BLLMember,
                        //BLLFactory.BLLReception,
                        //BLLFactory.BLLDZService,
                        //BLLFactory.BLLServiceOrder,
                        //BLLFactory.BLLRecetionStatus
                        );
                    //Application.Run(mainForm);
                    mainForm.ShowDialog();
                }
                else {
                    var viewChatList = new WinformView.UC_ChatList();
                    var viewIdentityList = new WinformView.UC_IdentityList();
                    
                   
                    Presenter.PIdentityList pIdentityList = new Presenter.PIdentityList(viewIdentityList, viewChatList);
                    Presenter.PChatList pChatList = new Presenter.PChatList(viewChatList, viewIdentityList, xmpp);
                    Presenter.PGlobal pGlobal = new Presenter.PGlobal(xmpp, pIdentityList, pChatList);

                    var mainForm2 = new WinformView.FormMain2(viewChatList, viewIdentityList);
                    mainForm2.ShowDialog();
                }
            }


            //Application.Run(new Views.Raw.ChatView());

            //var mainForm = new WPF.FormMain();
            //mainForm.ShowDialog();

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
