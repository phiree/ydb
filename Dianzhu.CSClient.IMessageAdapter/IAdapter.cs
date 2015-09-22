using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.CSClient.IMessageAdapter
{
    public interface IAdapter
    {
        
        Model.ReceptionChat MessageToChat(agsXMPP.protocol.client.Message message);

        agsXMPP.protocol.client.Message ChatToMessage(Model.ReceptionChat chat,string server);
    }
}