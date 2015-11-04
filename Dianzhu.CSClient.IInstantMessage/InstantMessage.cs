using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Dianzhu.CSClient.IInstantMessage
{
    public delegate void IMReceivedMessage(Model.ReceptionChat chat);
    public delegate void IMLogined(string jidUser);
    public delegate void IMAuthError();
    public delegate void IMPresent(string userFrom, int presentType);
    public delegate void IMError(string error);
    public delegate void IMConnectionError(string error);
    public delegate void IMClosed();
    public delegate void IMIQ();
    public delegate void IMStreamError();

    public interface InstantMessage
    {

        string Server { get; }
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
        void SendMessage(string xml);
        
        event IMReceivedMessage IMReceivedMessage;
        event IMIQ IMIQ;

        event IMStreamError IMStreamError;//登录相同账号冲突错误
      //  void SendMessage(agsXMPP.protocol.client.Message message);
    }
}
