using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
using agsXMPP.protocol.client;
using Dianzhu.CSClient.IInstantMessage;
namespace Dianzhu.CSClient.XMPP
{
    /// <summary>
    /// 通讯接口的 XMPP实现. 使用 agsxmpp 类库.
    /// </summary>
    public class XMPP : IInstantMessage.InstantMessage
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.XMPP");
 
        static agsXMPP.XmppClientConnection XmppClientConnection;


        public event IMClosed IMClosed;
        public event IMLogined IMLogined;
        public event IMAuthError IMAuthError;
        public event IMPresent IMPresent;
        public event IMReceivedMessage IMReceivedMessage;
        public event IMError IMError;
        public event IMConnectionError IMConnectionError;
        public event IMIQ IMIQ;
        public event IMStreamError IMStreamError;
        IMessageAdapter.IAdapter messageAdapter;
        private string server = string.Empty;
        private string domain = string.Empty;
        public string Server
        {
            get { return server; }
        }
        public string Domain
        {
            get { return domain; }
        }
        public XMPP(string server,string domain, IMessageAdapter.IAdapter messageAdapter, string resourceName) : this(server,domain, messageAdapter)
        {
            XmppClientConnection.Resource = resourceName;
        }
        public XMPP(string server,string domain, IMessageAdapter.IAdapter messageAdapter)
        {
            this.server = server;
            this.domain = domain;
            this.messageAdapter = messageAdapter;
            if (XmppClientConnection == null)
            {
                XmppClientConnection = new agsXMPP.XmppClientConnection();
                XmppClientConnection.Server =domain;
                XmppClientConnection.ConnectServer = server;
                XmppClientConnection.AutoResolveConnectServer = false;
                XmppClientConnection.OnLogin += new agsXMPP.ObjectHandler(Connection_OnLogin);
                XmppClientConnection.OnPresence += new PresenceHandler(Connection_OnPresence);
                XmppClientConnection.OnMessage += new MessageHandler(XmppClientConnection_OnMessage);
                XmppClientConnection.OnAuthError += new XmppElementHandler(XmppClientConnection_OnAuthError);
                XmppClientConnection.OnError += new ErrorHandler(XmppClientConnection_OnError);
                XmppClientConnection.OnSocketError += new ErrorHandler(XmppClientConnection_OnSocketError);
                XmppClientConnection.OnClose += new ObjectHandler(XmppClientConnection_OnClose);
                XmppClientConnection.OnIq += XmppClientConnection_OnIq;
                XmppClientConnection.OnStreamError += XmppClientConnection_OnStreamError;

            }
        }

        private void XmppClientConnection_OnStreamError(object sender, agsXMPP.Xml.Dom.Element e)
        {
            if(IMStreamError != null)
            {
                IMStreamError();
            }            
        }

        private void XmppClientConnection_OnIq(object sender, IQ iq)
        {
           
            log.Debug("receive_iq:" + iq.ToString());
            if (iq.Type == IqType.get)
            {
                var pong = new IQ(IqType.result);
                pong.To = iq.From;
                SendMessage(pong.ToString());
                log.Debug("send_iq:" + pong);
            }
        }
        void XmppClientConnection_OnSocketError(object sender, Exception ex)
        {
            if (IMConnectionError == null) return;
            IMConnectionError(ex.Message);
        }
        void XmppClientConnection_OnError(object sender, Exception ex)
        {
            if (IMError == null) return;
            IMError(ex.Message);
        }
        void XmppClientConnection_OnAuthError(object sender, agsXMPP.Xml.Dom.Element e)
        {
            if (IMAuthError == null) return;
            IMAuthError();
        }
        void XmppClientConnection_OnMessage(object sender, Message msg)
        {
            //接受消息,由presenter构建chat
            //message-->chat
            //1 转换为chat对象
            Model.ReceptionChat chat = messageAdapter.MessageToChat(msg);// new Model.ReceptionChat();// MessageAdapter.MessageToChat(msg);

            if (IMReceivedMessage != null)
            {
                IMReceivedMessage(chat);
            }
        }
        void Connection_OnPresence(object sender, Presence pres)
        {
            if (IMPresent != null)
            {
                IMPresent(pres.From.User, (int)pres.Type);
            }
        }
        void Connection_OnLogin(object sender)
        {
            if (IMLogined == null) return;
            IMLogined(XmppClientConnection.Username);

            //每隔一段时间给服务发送一个ping,防止连接超时.
            System.Timers.Timer tmHeartBeat = new System.Timers.Timer();
            tmHeartBeat.Elapsed += TmHeartBeat_Elapsed;
            tmHeartBeat.Interval = 5 * 60 * 1000;
            tmHeartBeat.Start();
        }
        private void TmHeartBeat_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            IQ iqHeartBeat = new IQ(IqType.get, XmppClientConnection.MyJID, server);
            var pingNode = new agsXMPP.Xml.Dom.Element("ping");
            pingNode.Namespace = "urn:xmpp:ping";
            iqHeartBeat.AddChild(pingNode);

            XmppClientConnection.Send(iqHeartBeat);
        }
        public void SendPresent()
        {
            Presence p = new Presence(ShowType.chat, "Online");
            p.Type = PresenceType.available;
            XmppClientConnection.Send(p);
        }
        public void SendMessage(Model.ReceptionChat chat)
        {
            //判断用户对应的tokoen
            //chat-->message
            Message msg = messageAdapter.ChatToMessage(chat, domain);
            log.Debug("receive___" + msg.ToString());
            XmppClientConnection.Send(msg);

        }
        //todo:发送消息 应该分离出来, 用来处理 xmpp发送和 push.
        
        public void SendMessage(string xml)
        {
            XmppClientConnection.Send(xml);
        }
        public void Close()
        {
            XmppClientConnection.Close();
        }
        public void OpenConnection(string userName, string password)
        {
            XmppClientConnection.Open(userName, password);
        }
        public void XmppClientConnection_OnClose(object sender)
        {
            if (IMClosed == null) return;
            IMClosed();
        }


    }
}
