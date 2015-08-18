using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormsMvp.Forms;
using agsXMPP;
using xmppMessage = agsXMPP.protocol.client;
using agsXMPP.protocol.client;
using dzModels = Dianzhu.Model;
namespace Dianzhu.CSClient.Views
{
    public partial class MainView : MvpForm<Models.MainModel>,ViewsContracts.IMainView
    {
        private MvpUserControl<Models.ChatModel> chatPanel;
        private MvpUserControl<Models.CustomerListModel> customerListPanel;
        public MainView()
        {
            Form fmLogin = new fmLogin();
            if (fmLogin.ShowDialog() == DialogResult.OK)
            {
                this.Show();
            }
            else
            {


                InitializeComponent();
                GlobalViables.XMPPConnection.OnMessage += new MessageHandler(XMPPConnection_OnMessage);
            }

        }
        void XMPPConnection_OnMessage(object sender, xmppMessage.Message msg)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MessageHandler(XMPPConnection_OnMessage), new object[] { sender, msg });
                return;
            }

            //判断该客户是否已经出现在列表中.
            
        }


        public event EventHandler OnMessage;
    }
}
