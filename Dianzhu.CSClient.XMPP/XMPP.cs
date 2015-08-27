using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP.protocol.client;
namespace Dianzhu.CSClient.XMPP
{
    public class XMPP:IInstantMessage.IXMPP
    {

        static readonly string Server = "192.168.1.140";
        static readonly string Domain = "192.168.1.140";
        static agsXMPP.XmppClientConnection Connection;
        
        public XMPP()
        {
             
            Connection = new agsXMPP.XmppClientConnection(Server);
            Connection.OnLogin += new agsXMPP.ObjectHandler(Connection_OnLogin);
        }

        void Connection_OnLogin(object sender)
        {
            OnLogin(sender);
        }
        
        
        public void SendMessage(string message, string from, string to)
        {
            
        }
        
        public event agsXMPP.ObjectHandler OnPresent;

        public event agsXMPP.ObjectHandler OnLogin;


       

        public void OpenConnection(string userName, string password)
        {
            Connection.Open(StringHelper.EnsureOpenfireUserName(userName), password);
        }


        event EventHandler IInstantMessage.IXMPP.OnPresent
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }
    }
}
