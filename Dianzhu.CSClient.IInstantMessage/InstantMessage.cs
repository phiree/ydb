using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace Dianzhu.CSClient.IInstantMessage
{
    public delegate void IMReceivedMessage(Model.ReceptionChat chat);
    public delegate void IMLogined();
    public delegate void IMAuthError();
    public delegate void IMPresent(string userFrom,int presentType);
    public delegate void IMError(string error);
    public delegate void IMConnectionError(string error);
    public delegate void IMClosed();

    public interface InstantMessage
    {
  
        void OpenConnection(string userName, string password);
        void SendPresent();

        event IMClosed IMClosed;
        event IMLogined IMLogined;
        event IMPresent IMPresent;
        event IMAuthError IMAuthError;
        event IMError IMError;
        void Close();
        event IMConnectionError IMConnectionError;

        void SendMessage(Model.ReceptionChat chat);
        
        event IMReceivedMessage IMReceivedMessage;
      //  void SendMessage(agsXMPP.protocol.client.Message message);
    }
}
