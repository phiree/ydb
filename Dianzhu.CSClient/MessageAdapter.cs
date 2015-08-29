using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
namespace Dianzhu.CSClient
{
    class MessageAdapter:IMessageAdapter.IAdapter
    {
        public Model.ReceptionChat MessageToChat(agsXMPP.protocol.client.Message message)
        {
            ReceptionChat chat = null;
            string messageType = message.GetAttribute("messageType");
            switch (messageType.ToLower())
            {
                case "text":
                    
                    break;
            }
            return chat;
        }

        public agsXMPP.protocol.client.Message ChatToMessage(Model.ReceptionChat chat)
        {
            throw new NotImplementedException();
        }
    }
}
