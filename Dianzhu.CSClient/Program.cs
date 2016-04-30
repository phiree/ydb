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
using ViewWPF = Dianzhu.CSClient.ViewWPF;
 
using cw = Castle.Windsor;
using cmr = Castle.MicroKernel.Registration;
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

            //systemconfig
            AppDomain cDomain = AppDomain.CurrentDomain;
            cDomain.UnhandledException += new UnhandledExceptionEventHandler(cDomain_UnhandledException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //log

            log.Debug("开始启动助理工具");
            bool isValidConfig = CheckConfig();
            if (!isValidConfig)
            {
                MessageBox.Show("配置错误,程序即将退出");
                Application.ExitThread();
                return;
            }

            var container = Install();

            string version = GetVersion();
            //  loginForm.FormText += "v" + version;
            Presenter.LoginPresenter loginPresenter = container.Resolve<Presenter.LoginPresenter>();
            bool? result = loginPresenter.ShowDialog();


            bool useWpf = true;
            //登录成功
            if (result.Value)// == DialogResult.OK)
            {


                log.Debug("登录成功");


                Presenter.PIdentityList pIdentityList = container.Resolve<Presenter.PIdentityList>(); ;
                Presenter.PChatList pChatList = container.Resolve<Presenter.PChatList>();
                Presenter.PNotice pNotice = container.Resolve<Presenter.PNotice>();

                Presenter.PSearch pSearch = container.Resolve<Presenter.PSearch>();
                Presenter.POrder pOrder = container.Resolve<Presenter.POrder>();
                Presenter.POrderHistory pOrderHistory = container.Resolve<Presenter.POrderHistory>();
                Presenter.PChatSend pChatSend = container.Resolve<Presenter.PChatSend>();


                Presenter.InstantMessageHandler imHander = container.Resolve<Presenter.InstantMessageHandler>();


                var mainPresenter = container.Resolve<Presenter.PMain>();


                mainPresenter.ShowDialog();


            }


            //Application.Run(new Views.Raw.ChatView());

            //var mainForm = new WPF.FormMain();
            //mainForm.ShowDialog();

        }

        static Castle.Windsor.WindsorContainer Install()
        {
            var container = new Castle.Windsor.WindsorContainer();
            container.Register(cmr.Component.For<Presenter.PMain>());


            container.Register(cmr.Component.For<CSClient.Presenter.LoginPresenter>());
            container.Register(cmr.Component.For<CSClient.Presenter.IdentityManager>());
            container.Register(cmr.Component.For<CSClient.Presenter.PIdentityList>());
            container.Register(cmr.Component.For<CSClient.Presenter.PChatList>());
            container.Register(cmr.Component.For<CSClient.Presenter.PChatSend>());
            container.Register(cmr.Component.For<CSClient.Presenter.PNotice>());
            container.Register(cmr.Component.For<CSClient.Presenter.POrder>());
            container.Register(cmr.Component.For<CSClient.Presenter.POrderHistory>());
            container.Register(cmr.Component.For<CSClient.Presenter.PSearch>());
            container.Register(cmr.Component.For<Presenter.InstantMessageHandler>());

            container.Register(cmr.Component.For<IView.IViewMainForm>().ImplementedBy<ViewWPF.FormMain>());
            container.Register(cmr.Component.For<IView.ILoginForm>().ImplementedBy<ViewWPF.FormLogin>());
            container.Register(cmr.Component.For<IViewChatList>().ImplementedBy<ViewWPF.UC_ChatList>());
            container.Register(cmr.Component.For<IViewChatSend>().ImplementedBy<ViewWPF.UC_ChatSend>());
            container.Register(cmr.Component.For<IViewIdentityList>().ImplementedBy<ViewWPF.UC_IdentityList>());
            container.Register(cmr.Component.For<IViewNotice>().ImplementedBy<ViewWPF.UC_Notice>());
            container.Register(cmr.Component.For<IViewOrder>().ImplementedBy<ViewWPF.UC_Order>());
            container.Register(cmr.Component.For<IViewOrderHistory>().ImplementedBy<ViewWPF.UC_OrderHistory>());
            container.Register(cmr.Component.For<IViewSearch>().ImplementedBy<ViewWPF.UC_Search>());
            container.Register(cmr.Component.For<IViewSearchResult>().ImplementedBy<ViewWPF.UC_SearchResult>());

            string server = Config.Config.GetAppSetting("ImServer");
            string domain = Config.Config.GetAppSetting("ImDomain");
            container.Register(cmr.Component.For<CSClient.IInstantMessage.InstantMessage>().ImplementedBy<XMPP.XMPP>()
                .DependsOn(cmr.Dependency.OnValue("server", server)
                            , cmr.Dependency.OnValue("domain", domain)
                            , cmr.Dependency.OnValue("resourceName", Model.Enums.enum_XmppResource.YDBan_CustomerService.ToString())
                            )
                            );

            container.Register(cmr.Component.For<CSClient.IMessageAdapter.IAdapter>()
                .ImplementedBy<CSClient.MessageAdapter.MessageAdapter>()


                );


            return container;

        }
        static bool CheckConfig()
        {
            log.Debug("--开始 检查配置是否冲突");
            //need: openfire服务器 数据库,api服务器,三者目标ip应该相等.
            bool isValidConfig = false;
            string connectionString = PHSuit.Security.Decrypt(System.Configuration.ConfigurationManager
                .ConnectionStrings["DianzhuConnectionString"].ConnectionString, false);
            System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(connectionString, @"(?<=data\s+source\=).+?(?=;uid)");
            string ofserver = Dianzhu.Config.Config.GetAppSetting("ImServer");
            System.Text.RegularExpressions.Match m2 = System.Text.RegularExpressions.Regex.Match(Dianzhu.Config.Config.GetAppSetting("APIBaseURL"), "(?<=https?://).+?(?=:8037)");

            if (ofserver == m.Value && m.Value == m2.Value)
            {
                isValidConfig = true;
            }
            else
            {
                log.Error(m.Value + "," + m2.Value + "," + ofserver);
            }
            log.Debug("--结束 检查配置是否冲突");
            return isValidConfig;




        }
        static void cDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                log.Error("异常崩溃:" + e.ExceptionObject.ToString());

                MessageBox.Show(e.ExceptionObject.ToString());
            }
            catch (Exception ex)
            {

            }
        }
        static string GetVersion()
        {
            Version myVersion = new Version();

            if (ApplicationDeployment.IsNetworkDeployed)
                myVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;
            return myVersion.ToString();
        }
    }


    // public interface IIdentityManagerFactory
}
