using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
 
using agsXMPP;
using agsXMPP.protocol;
using agsXMPP.protocol.iq;
using agsXMPP.protocol.iq.disco;
using agsXMPP.protocol.iq.roster;
using agsXMPP.protocol.iq.version;
using agsXMPP.protocol.iq.oob;
using agsXMPP.protocol.client;
using agsXMPP.protocol.extensions.shim;
using agsXMPP.protocol.extensions.si;
using agsXMPP.protocol.extensions.bytestreams;

using agsXMPP.protocol.x;
using agsXMPP.protocol.x.data;

using agsXMPP.Xml;
using agsXMPP.Xml.Dom;

using agsXMPP.sasl;
 
 
using agsXMPP.Collections;
namespace Dianzhu.XMPPPool
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            //System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false; 
            Jid jid = new Jid("dianzhu@" + GlobalViables.ServerName);
            GlobalViables.XMPPConnection = new XmppClientConnection(jid.Server);
            GlobalViables.XMPPConnection.SocketConnectionType = agsXMPP.net.SocketConnectionType.Direct;
            GlobalViables.XMPPConnection.Open(jid.User, "1");
            GlobalViables.XMPPConnection.OnLogin += new ObjectHandler(XMPPConnection_OnLogin);//开启了新的进程.
            GlobalViables.XMPPConnection.OnMessage +=new MessageHandler(XMPPConnection_OnMessage);
            GlobalViables.XMPPConnection.OnPresence += new PresenceHandler(XMPPConnection_OnPresence);
           
           
            
        }

        void XMPPConnection_OnMessage(object sender, agsXMPP.protocol.client.Message msg)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MessageHandler(XMPPConnection_OnMessage), new object[] { sender, msg });
                return;
            }
            AddLog("接收到来自" + msg.From.User + "的消息:" + msg.Body);
        }
        void MessageCallBack(object sender,
                                 agsXMPP.protocol.client.Message msg,
                                 object data)
        {
            if (msg.Body != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0}>> {1}", msg.From.User, msg.Body);
                Console.ForegroundColor = ConsoleColor.Green;
                AddLog("接收到" + msg.From.User + "的消息:" + msg.Body);
            }
        }
        void XMPPConnection_OnPresence(object sender, Presence pres)
        {
            if (InvokeRequired)
            {
                // Windows Forms are not Thread Safe, we need to invoke this :(
                // We're not in the UI thread, so we need to call BeginInvoke				
                BeginInvoke(new PresenceHandler(XMPPConnection_OnPresence), new object[] { sender ,pres});
                return;
            }
            Console.WriteLine("Available Contacts: ");
            Console.WriteLine("{0}@{1}  {2}", pres.From.User, pres.From.Server, pres.Type);
            //Console.WriteLine(pres.From.User + "@" + pres.From.Server + "  " + pres.Type);
            Console.WriteLine();
            
        }

        void XMPPConnection_OnLogin(object sender)
        {
            Presence p = new Presence(ShowType.chat, "Online");
            p.Type = PresenceType.available;
            GlobalViables.XMPPConnection.Send(p);
            
           // AddLogCallback alcb = new AddLogCallback(AddLog);
            if (InvokeRequired)
            {
                // Windows Forms are not Thread Safe, we need to invoke this :(
                // We're not in the UI thread, so we need to call BeginInvoke				
                BeginInvoke(new ObjectHandler(XMPPConnection_OnLogin), new object[] { sender });
                return;
            }
            AddLog("登录成功");
            
        }
        
        void AddLog(string msg)
        {

            tbxLog.Text += msg+Environment.NewLine;
        }

    }
}
