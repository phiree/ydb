using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
using agsXMPP.protocol.client;
namespace Dianzhu.CSClient.IInstantMessage
{
    public delegate void ReceiveMessageHandler(string userFrom,string message);
    public interface IXMPP
    {
        
      
        void OpenConnection(string userName, string password);
        void SendPresent();
        event ObjectHandler OnLogin;
        event PresenceHandler OnPresent;
        void SendMessage(string message, string to);
        event ReceiveMessageHandler ReceiveMessageHandler;
      //  void SendMessage(agsXMPP.protocol.client.Message message);
    }
}
