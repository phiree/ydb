using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ydb.InstantMessage.DomainModel.Chat
{
    /// <summary>
    /// 消息转换接口
    /// </summary>
    public interface IMessageAdapter
    {

         ReceptionChat MessageToChat(agsXMPP.protocol.client.Message message);

        agsXMPP.protocol.client.Message ChatToMessage(ReceptionChat chat, string server);

        agsXMPP.protocol.client.Message RawXmlToMessage(string rawXml);
         ReceptionChat RawXmlToChat(string rawXml);
    }
}