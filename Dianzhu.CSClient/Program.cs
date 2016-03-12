using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Dianzhu.CSClient.MessageAdapter;
using log4net;
 
using System.Deployment;
using System.Deployment.Application;
using System.Threading;
using System.ComponentModel;
using Dianzhu.BLL;
using Dianzhu.CSClient.IView;
using ViewWPF=Dianzhu.CSClient.ViewWPF;
using ViewWinForm = Dianzhu.CSClient.WinformView;
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


            var loginForm = new ViewWPF.FormLogin();
            string version = GetVersion();
            loginForm.FormText += "v" + version;
            Presenter.LoginPresenter loginPresenter =
            new Presenter.LoginPresenter(loginForm, xmpp);
            bool? result = loginForm.ShowDialog();


            bool useWpf = false;
            //登录成功
            if (result.Value)// == DialogResult.OK)
            {
                IViewChatList viewChatList=null;
                IViewIdentityList viewIdentityList = null;
                IViewOrder viewOrder = null;
                IViewSearch viewSearch = null;
                IViewSearchResult viewSearchResult = null;

                if (useWpf)
                {
                    viewIdentityList = new ViewWPF.UC_IdentityList();
                    viewChatList = new ViewWPF.UC_ChatList();
                    viewOrder = new ViewWPF.UC_Order();
                    viewSearch = new ViewWPF.UC_Search();
                    viewSearchResult = new ViewWPF.UC_SearchResult();
                }
                else
                {
                     viewChatList = new WinformView.UC_ChatList();
                     viewIdentityList = new WinformView.UC_IdentityList();
                     viewOrder = new WinformView.UC_Order();
                     viewSearch = new WinformView.UC_Search();
                     viewSearchResult = new WinformView.UC_SearchResult();

                }
                //初始化Presenter
                Presenter.PIdentityList pIdentityList = new Presenter.PIdentityList(viewIdentityList, viewChatList, viewOrder);
                Presenter.PChatList pChatList = new Presenter.PChatList(viewChatList, viewIdentityList, xmpp);
                Presenter.IdentityManager pIdentityManager = new Presenter.IdentityManager(pIdentityList, pChatList);
                Presenter.InstantMessageHandler imHander = new Presenter.InstantMessageHandler(xmpp, pIdentityManager, pIdentityList);
                Presenter.PSearch pSearch = new Presenter.PSearch(viewSearch, viewSearchResult, viewOrder);
                Presenter.POrder pOrder = new Presenter.POrder(xmpp, viewOrder);

                if (useWpf)
                {

                    
                    var mainForm = new ViewWPF.FormMain((ViewWPF.UC_IdentityList)viewIdentityList,
                        (ViewWPF.UC_ChatList)viewChatList,
                        (ViewWPF.UC_Order)viewOrder,
                        (ViewWPF.UC_Search)viewSearch,
                        (ViewWPF.UC_SearchResult)viewSearchResult);
                    mainForm.Title += "v" + version;
                     
                    mainForm.ShowDialog();
                }
                else {
                   
                    var mainForm2 = new WinformView.FormMain2((ViewWinForm.UC_ChatList)viewChatList, 
                        (ViewWinForm.UC_IdentityList) viewIdentityList,
                        
                        (ViewWinForm.UC_Order) viewOrder,
                        (ViewWinForm.UC_Search) viewSearch,
                        (ViewWinForm.UC_SearchResult) viewSearchResult);
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
