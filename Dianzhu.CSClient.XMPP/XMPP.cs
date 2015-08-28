using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
using agsXMPP.protocol.client;
namespace Dianzhu.CSClient.XMPP
{
    public class XMPP : IInstantMessage.IXMPP
    {

       public static readonly string Server = "192.168.1.140";
        public static readonly string Domain = "192.168.1.140";
        static agsXMPP.XmppClientConnection XmppClientConnection;
        public XMPP()
        {
            if (XmppClientConnection == null)
            {
                XmppClientConnection = new agsXMPP.XmppClientConnection(Server);
                XmppClientConnection.OnLogin += new agsXMPP.ObjectHandler(Connection_OnLogin);
                XmppClientConnection.OnPresence += new PresenceHandler(Connection_OnPresence);
            }
        }

        void Connection_OnPresence(object sender, Presence pres)
        {
            if (OnPresent != null)
            {
                OnPresent(sender, pres);
            }
        }

        void Connection_OnLogin(object sender)
        {
            OnLogin(sender);
        }

        public void SendPresent()
        {
            Presence p = new Presence(ShowType.chat, "Online");
            p.Type = PresenceType.available;
            XmppClientConnection.Send(p);
        }

        public void SendMessage(string message,   string to)
        {
            Message msg = new Message(StringHelper.EnsureOpenfireUserName(to) + "@" + Server,
                message);
            XmppClientConnection.Send(msg);
        }



        public event ObjectHandler OnLogin;

        public event PresenceHandler OnPresent;


        public void OpenConnection(string userName, string password)
        {
            XmppClientConnection.Open(StringHelper.EnsureOpenfireUserName(userName), password);
        }




    }
}
