using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Dianzhu.CSClient.MessageAdapter;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            log4net.Config.XmlConfigurator.Configure();
            DialogResult result;
            IMessageAdapter.IAdapter messageAdapter = new MessageAdapter.MessageAdapter(
                BLLFactory.BLLMember,BLLFactory.BLLDZService,BLLFactory.BLLServiceOrder);
            XMPP.XMPP xmpp = new XMPP.XMPP(messageAdapter);
            using (var loginForm = new WinformView.FormLogin())
            {
                Presenter.LoginPresenter loginPresenter = new Presenter.LoginPresenter(loginForm,xmpp,
                    BLLFactory.BLLMember);
                  result = loginForm.ShowDialog();
            }
            if (result == DialogResult.OK)
            {
                var mainForm = new WinformView.FormMain();

                Presenter.MainPresenter MainPresenter = new Presenter.MainPresenter(
                    mainForm, xmpp, messageAdapter,
                    BLLFactory.BLLMember,
                    BLLFactory.BLLReception,
                    BLLFactory.BLLDZService,
                    BLLFactory.BLLServiceOrder
                    );
                Application.Run(mainForm);
            }
            //Application.Run(new Views.Raw.ChatView());

            
        }
    }
}
