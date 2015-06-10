using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using agsXMPP;
using agsXMPP.protocol.client;

namespace Dianzhu.XmppClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitXmpp();
        }
        const string JID_SENDER = "yuanfei@yuanfei-pc";
        const string PASSWORD = "1";   // password of the JIS_SENDER account

        const string JID_RECEIVER = "yuanfei2@yuanfei-pc";
        Jid jidSender = new Jid(JID_SENDER);
        XmppClientConnection xmpp =new XmppClientConnection();
        public void InitXmpp()
        {
           

            Jid jidSender = new Jid(JID_SENDER);
       
            xmpp = new XmppClientConnection(jidSender.Server);
            xmpp.OnMessage += new MessageHandler(xmpp_OnMessage);
            xmpp.Open(jidSender.User, PASSWORD);
        }

        void xmpp_OnMessage(object sender, agsXMPP.protocol.client.Message msg)
        {
            listBox1.Items.Add(msg.Body);
        }

        void sendMsg(string msg)
        {
            xmpp.Send(new agsXMPP.protocol.client.Message(new Jid(JID_RECEIVER), MessageType.chat, msg));
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
          sendMsg(textBox1.Text);
        }

        private void label1_Click(object sender, System.EventArgs e)
        {

        }
    }
}
