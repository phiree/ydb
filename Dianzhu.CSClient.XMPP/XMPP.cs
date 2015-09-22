using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
using agsXMPP.protocol.client;
using Dianzhu.CSClient.IInstantMessage;
namespace Dianzhu.CSClient.XMPP
{
    public class XMPP : IInstantMessage.InstantMessage
    {

        public static readonly string Server = System.Configuration.ConfigurationManager.AppSettings["server"];
        public static readonly string Domain = System.Configuration.ConfigurationManager.AppSettings["domain"];
        static agsXMPP.XmppClientConnection XmppClientConnection;


        public event IMClosed IMClosed;
        public event IMLogined IMLogined;
        public event IMAuthError IMAuthError;
        public event IMPresent IMPresent;
        public event IMReceivedMessage IMReceivedMessage;
        public event IMError IMError;
        public event IMConnectionError IMConnectionError;
        IMessageAdapter.IAdapter messageAdapter;
        public XMPP(IMessageAdapter.IAdapter messageAdapter)
        {
            this.messageAdapter = messageAdapter;
            if (XmppClientConnection == null)
            {
                XmppClientConnection = new agsXMPP.XmppClientConnection(Server);
                XmppClientConnection.AutoResolveConnectServer = false;
                XmppClientConnection.OnLogin += new agsXMPP.ObjectHandler(Connection_OnLogin);
                XmppClientConnection.OnPresence += new PresenceHandler(Connection_OnPresence);
                XmppClientConnection.OnMessage += new MessageHandler(XmppClientConnection_OnMessage);
                XmppClientConnection.OnAuthError += new XmppElementHandler(XmppClientConnection_OnAuthError);
                XmppClientConnection.OnError += new ErrorHandler(XmppClientConnection_OnError);
                XmppClientConnection.OnSocketError+=new ErrorHandler(XmppClientConnection_OnSocketError);
                XmppClientConnection.OnClose+=new ObjectHandler(XmppClientConnection_OnClose);
                
            }
        }
        void XmppClientConnection_OnSocketError(object sender, Exception ex)
        {
            IMConnectionError(ex.Message);
        }
        void XmppClientConnection_OnError(object sender, Exception ex)
        {
            IMError(ex.Message);
        }

        void XmppClientConnection_OnAuthError(object sender, agsXMPP.Xml.Dom.Element e)
        {
            IMAuthError();
        }

        void XmppClientConnection_OnMessage(object sender, Message msg)
        {
            //接受消息,由presenter构建chat
            //message-->chat
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
            IMLogined();
        }

        public void SendPresent()
        {
            Presence p = new Presence(ShowType.chat, "Online");
            p.Type = PresenceType.available;
            XmppClientConnection.Send(p);
        }

        public void SendMessage(Model.ReceptionChat chat)
        {
            //chat-->message
            Message msg = messageAdapter.ChatToMessage(chat,Server);
            XmppClientConnection.Send(msg);
        }
        public void Close()
        {
            XmppClientConnection.Close();
        }



        public void OpenConnection(string userName, string password)
        {
            XmppClientConnection.Open( userName , password);
        }





        public void XmppClientConnection_OnClose(object sender)
        {
            IMClosed();
        }
        
    }
}
