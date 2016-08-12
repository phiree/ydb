using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.CSClient.IMessageAdapter
{
    /// <summary>
    /// 消息转换接口
    /// </summary>
    public interface IAdapter
    {

        Model.ReceptionChat MessageToChat(agsXMPP.protocol.client.Message message);

        agsXMPP.protocol.client.Message ChatToMessage(Model.ReceptionChat chat, string server);

        agsXMPP.protocol.client.Message RawXmlToMessage(string rawXml);
        Model.ReceptionChat RawXmlToChat(string rawXml);
    }
}