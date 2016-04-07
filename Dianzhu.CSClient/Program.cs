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
            PHSuit.Logging.Config("Dianzhu.CSClient");
            bool isValidConfig=  CheckConfig();
            if (!isValidConfig)
            {
                MessageBox.Show("配置错误,程序即将退出");
                Application.ExitThread();
                return;
            }
            //systemconfig
            AppDomain cDomain = AppDomain.CurrentDomain;
            cDomain.UnhandledException += new UnhandledExceptionEventHandler(cDomain_UnhandledException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //log
           
            log.Debug( "开始启动助理工具");
            
            //prepare parameters for IM instance's constructor
            //init messageadapter
            IMessageAdapter.IAdapter messageAdapter = new MessageAdapter.MessageAdapter(
                 );
            //get im server config
            string server = Config.Config.GetAppSetting("ImServer");
            string domain= Config.Config.GetAppSetting("ImDomain");

            IInstantMessage.InstantMessage xmpp = new XMPP.XMPP(server,domain, messageAdapter, Model.Enums.enum_XmppResource.YDBan_CustomerService.ToString());


            var loginForm = new ViewWPF.FormLogin();
            string version = GetVersion();
            loginForm.FormText += "v" + version;
            Presenter.LoginPresenter loginPresenter =
            new Presenter.LoginPresenter(loginForm, xmpp);
            bool? result = loginForm.ShowDialog();


            bool useWpf = true;
            //登录成功
            if (result.Value)// == DialogResult.OK)
            {
                log.Debug("登录成功");
                IViewChatList viewChatList=null;
                IViewIdentityList viewIdentityList = null;
                IViewOrder viewOrder = null;
                IViewSearch viewSearch = null;
                IViewSearchResult viewSearchResult = null;
                IViewChatSend viewChatSend = null;
                IViewOrderHistory viewOrderHistory = null;
                IViewNotice viewNotice = null;

                if (useWpf)
                {
                    viewIdentityList = new ViewWPF.UC_IdentityList();
                    viewChatList = new ViewWPF.UC_ChatList();
                    viewChatSend = new ViewWPF.UC_ChatSend();
                    viewOrder = new ViewWPF.UC_Order();
                    viewOrderHistory = new ViewWPF.UC_OrderHistory();
                    
                    viewSearchResult = new ViewWPF.UC_SearchResult();
                    viewSearch = new ViewWPF.UC_Search(viewSearchResult);
                    viewNotice = new ViewWPF.UC_Notice();
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
                Presenter.PNotice pNotice = new Presenter.PNotice(viewNotice);
                Presenter.InstantMessageHandler imHander = new Presenter.InstantMessageHandler(xmpp, pIdentityManager, pIdentityList,pNotice);
                Presenter.PSearch pSearch = new Presenter.PSearch(xmpp,viewSearch, viewSearchResult, viewOrder,viewChatList);
                Presenter.POrder pOrder = new Presenter.POrder(xmpp, viewOrder);
                Presenter.POrderHistory pOrderHistory = new Presenter.POrderHistory(viewOrderHistory,viewIdentityList);
                Presenter.PChatSend pChatSend = new Presenter.PChatSend(viewChatSend, viewChatList, xmpp);
                Presenter.PMain pMain = new Presenter.PMain(new BLLReceptionStatus(), new BLLReceptionStatusArchieve(),
                    new BLLReceptionChatDD(), new BLLReceptionChat(), new BLLIMUserStatus(), xmpp, viewIdentityList);

                if (useWpf)
                {

                    
                    var mainForm = new ViewWPF.FormMain((ViewWPF.UC_IdentityList)viewIdentityList,
                        (ViewWPF.UC_ChatList)viewChatList,
                        (ViewWPF.UC_ChatSend)viewChatSend,
                        (ViewWPF.UC_Order)viewOrder,
                        (ViewWPF.UC_Search)viewSearch,
                        (ViewWPF.UC_SearchResult)viewSearchResult,
                        (ViewWPF.UC_OrderHistory)viewOrderHistory,
                        (ViewWPF.UC_Notice)viewNotice
                        );
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

        static bool CheckConfig()
        {
            log.Debug("--开始 检查配置是否冲突");
            //need: openfire服务器 数据库,api服务器,三者目标ip应该相等.
            bool isValidConfig = false;
            string connectionString = PHSuit.Security.Decrypt(System.Configuration.ConfigurationManager
                .ConnectionStrings["DianzhuConnectionString"].ConnectionString, false);
            System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(connectionString, @"(?<=data\s+source\=).+?(?=;uid)");
            string ofserver= Dianzhu.Config.Config.GetAppSetting("ImServer");
            System.Text.RegularExpressions.Match m2= System.Text.RegularExpressions.Regex.Match(Dianzhu.Config.Config.GetAppSetting("APIBaseURL"), "(?<=https?://).+?(?=:8037)");

            if (ofserver == m.Value && m.Value == m2.Value)
            {
                isValidConfig = true;
            }
            else
            {
                log.Error(m.Value+","+m2.Value+","+ofserver);
            }

            return isValidConfig;



            
        }
        static void cDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            
            log.Error("异常崩溃:"+ e.ExceptionObject.ToString());
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
