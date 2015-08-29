using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
using agsXMPP.protocol.client;
namespace Dianzhu.CSClient.IInstantMessage
{
    public delegate void IMReceivedMessage(string userFrom, string message);
    public delegate void IMLogined();
    public delegate void IMAuthError();
    public delegate void IMPresent(string userFrom,int presentType);
    public delegate void IMError(string error);
    
    public interface IXMPP
    {
  
        void OpenConnection(string userName, string password);
        void SendPresent();

        event IMLogined IMLogined;
        event IMPresent IMPresent;
        event IMAuthError IMAuthError;
        event IMError IMError;
        void SendMessage(string message, string to);
        event IMReceivedMessage IMReceivedMessage;
      //  void SendMessage(agsXMPP.protocol.client.Message message);
    }
}
