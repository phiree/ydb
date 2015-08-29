using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace Dianzhu.CSClient.IInstantMessage
{
    public delegate void IMReceivedMessage(string userFrom, Model.ReceptionChat chat);
    public delegate void IMLogined();
    public delegate void IMAuthError();
    public delegate void IMPresent(string userFrom,int presentType);
    public delegate void IMError(string error);


    public interface InstantMessage
    {
  
        void OpenConnection(string userName, string password);
        void SendPresent();

        event IMLogined IMLogined;
        event IMPresent IMPresent;
        event IMAuthError IMAuthError;
        event IMError IMError;

        void SendMessage(Model.ReceptionChat chat);
        event IMReceivedMessage IMReceivedMessage;
      //  void SendMessage(agsXMPP.protocol.client.Message message);
    }
}
