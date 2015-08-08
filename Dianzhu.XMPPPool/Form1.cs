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
using agsclient= agsXMPP.protocol.client;
using agsXMPP.Xml;
using agsXMPP.Xml.Dom;

using agsXMPP.sasl;
using System.Threading;
 
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
            GlobalViables.XMPPConnection.OnError += new ErrorHandler(XMPPConnection_OnError);

            GlobalViables.XMPPConnection.OnPresence += new PresenceHandler(XMPPConnection_OnPresence);
           
           
            
        }

        void XMPPConnection_OnError(object sender, Exception ex)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ErrorHandler(XMPPConnection_OnError), new object[] { sender, ex });
                return;
            }
            AddLog("出错了:" + ex.Message);
        }
  
        
        void XMPPConnection_OnMessage(object sender, agsXMPP.protocol.client.Message msg)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MessageHandler(XMPPConnection_OnMessage), new object[] { sender, msg });
                return;
            }
            AddLog("接收到来自" + msg.From.User + "的消息:" + msg.Body);
            string　csName="yuanfei";
            AddLog("分配的接待客服:" + csName);
            //为该用户分配客服.
            Jid csId=new Jid("yuanfei@" + GlobalViables.ServerName);
            //XmppClientConnection assignConnection = new XmppClientConnection(GlobalViables.ServerName);//专用于分配.
            //assignConnection.Open(csName, "1");
            ////客服回复客户
            //assignConnection.Send(new agsclient.Message(msg.From, "我是客服" + csName));
            //assignConnection.Close();
            //直接将客户的消息转发给客服
            Thread t = new Thread(()=>RedirectToCs(msg.From.User,msg.Body));
            t.Start();
            

        }
       
        public void RedirectToCs(string customerName,string message)
        {
            string csid = "yuanfei";
             XmppClientConnection assignConnection2 = new XmppClientConnection(GlobalViables.ServerName);//专用于分配.

            assignConnection2.Open(customerName, "1");
            assignConnection2.Send(new agsclient.Message(csid+"@yuanfei-pc", message));
            assignConnection2.Close();
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

            tbxLog.Text = msg+Environment.NewLine+tbxLog.Text;
        }

    }
}
