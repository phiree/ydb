using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.CSClient.IInstantMessage;
 
using Dianzhu.Model;
namespace InstantMessageMock
{
    public class IMMock : InstantMessage
    {
        
        public string Domain
        {
            get
            {
                return string.Empty;
            }
        }

        public string Server
        {
            get
            {
                return string.Empty;
            }
        }

        public event Dianzhu.CSClient.IInstantMessage.IMAuthError IMAuthError;
        public event Dianzhu.CSClient.IInstantMessage.IMClosed IMClosed;
        public event Dianzhu.CSClient.IInstantMessage.IMConnectionError IMConnectionError;
        public event Dianzhu.CSClient.IInstantMessage.IMError IMError;
        public event Dianzhu.CSClient.IInstantMessage.IMIQ IMIQ;
        public event Dianzhu.CSClient.IInstantMessage.IMLogined IMLogined;
        public event Dianzhu.CSClient.IInstantMessage.IMPresent IMPresent;
        public event Dianzhu.CSClient.IInstantMessage.IMReceivedMessage IMReceivedMessage;
        public event Dianzhu.CSClient.IInstantMessage.IMStreamError IMStreamError;

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void OpenConnection(string userName, string password)
        {
             
        }

        public void SendMessage(string xml)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(Dianzhu.Model.ReceptionChat chat)
        {
            throw new NotImplementedException();
        }

        public void SendPresent()
        {
            throw new NotImplementedException();
        }
    }
}
