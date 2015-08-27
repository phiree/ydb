using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
namespace Dianzhu.CSClient.IInstantMessage
{
    public interface IXMPP
    {
        
      
        void OpenConnection(string userName, string password);
        event ObjectHandler OnLogin;
        event EventHandler OnPresent;
        void SendMessage(string message, string from, string to);
    }
}
