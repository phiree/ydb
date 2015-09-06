using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Dianzhu.CSClient.MessageAdapter;
using log4net;
namespace Dianzhu.CSClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain cDomain = AppDomain.CurrentDomain;
            cDomain.UnhandledException += new UnhandledExceptionEventHandler(cDomain_UnhandledException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            log4net.Config.XmlConfigurator.Configure();
            ILog log = LogManager.GetLogger("cs");
            log.Debug("Start");
            DialogResult result;
            IMessageAdapter.IAdapter messageAdapter = new MessageAdapter.MessageAdapter(
                GlobalViables.BLLMembership, GlobalViables.BLLDZService, GlobalViables.BLLServiceOrder);
            XMPP.XMPP xmpp = new XMPP.XMPP(messageAdapter);
            using (var loginForm = new WinformView.FormLogin())
            {
                Presenter.LoginPresenter loginPresenter = new Presenter.LoginPresenter(loginForm,xmpp,
                    GlobalViables.BLLMembership);
                  result = loginForm.ShowDialog();
            }
            if (result == DialogResult.OK)
            {
                var mainForm = new WinformView.FormMain();

                Presenter.MainPresenter MainPresenter = new Presenter.MainPresenter(
                    mainForm, xmpp, messageAdapter,
                    GlobalViables.BLLMembership,
                    GlobalViables.BLLReception,
                    GlobalViables.BLLDZService,
                    GlobalViables.BLLServiceOrder
                    );
                Application.Run(mainForm);
            }
            //Application.Run(new Views.Raw.ChatView());

            
        }

        static void cDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ExceptionObject.ToString());
        }
    }
}
