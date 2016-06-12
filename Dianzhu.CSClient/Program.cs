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
using Dianzhu.DAL;
using Dianzhu.IDAL;
using Dianzhu.Model;
using NHibernate;
using DDDCommon.Domain;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;

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
        static void Main(string[] args)
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

             Bootstrap.Boot();

            string version = GetVersion();
            //  loginForm.FormText += "v" + version;
            Presenter.LoginPresenter loginPresenter = Bootstrap.Container.Resolve<Presenter.LoginPresenter>();
            loginPresenter.Args = args;
            bool? result = loginPresenter.ShowDialog();


            bool useWpf = true;
            //登录成功
            if (result.Value)// == DialogResult.OK)
            {


                log.Debug("登录成功");


                Presenter.PIdentityList pIdentityList = Bootstrap.Container.Resolve<Presenter.PIdentityList>(); ;
                Presenter.PChatList pChatList = Bootstrap.Container.Resolve<Presenter.PChatList>();
                Presenter.PNotice pNotice = Bootstrap.Container.Resolve<Presenter.PNotice>();

                Presenter.PSearch pSearch = Bootstrap.Container.Resolve<Presenter.PSearch>();
                Presenter.POrder pOrder = Bootstrap.Container.Resolve<Presenter.POrder>();
                Presenter.POrderHistory pOrderHistory = Bootstrap.Container.Resolve<Presenter.POrderHistory>();
                Presenter.PChatSend pChatSend = Bootstrap.Container.Resolve<Presenter.PChatSend>();
               // Presenter.PShelfService pShelfService = Bootstrap.Container.Resolve<Presenter.PShelfService>();
                

                var mainPresenter = Bootstrap.Container.Resolve<Presenter.PMain>();


                mainPresenter.ShowDialog();


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
}
