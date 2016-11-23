using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
 
using agsXMPP.protocol.client;
using System.Xml;
using agsXMPP.Xml.Dom;
using System.Globalization;
using Ydb.InstantMessage.DomainModel.Enums;
namespace Ydb.InstantMessage.DomainModel.Chat
{
    /// <summary>
    /// 聊天消息 和 xmpp消息之间的转换.
    /// </summary>
    public class MessageAdapter : IMessageAdapter
    {
 
        log4net.ILog log = log4net.LogManager.GetLogger("Ydb.InstantMessage.DomainModel.Chat.MessageAdapter");
         
        /// <summary>
        /// 将im的 message为 系统设计的格式(chat)
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public  ReceptionChat MessageToChat(Message message)
        {
            var ext_element = message.SelectSingleElement("ext", true);
            string errMsg;
            if (message.SelectSingleElement("ext", true)==null)
            {
                errMsg = "没有ext节点";
                throw new Exception(errMsg);
            }
            ReceptionChat chat;
            //获取基本数据
            Guid id = string.IsNullOrEmpty(message.Id) ? Guid.NewGuid() : new Guid(message.Id);
            string fromId = message.From.User;
            string fromResourceRaw = message.From.Resource;
            string toId = message.To.User;
            string toResourceRaw = message.To.Resource;
            string messageBody = message.Body;
            string sessionId = ext_element.SelectSingleElement("orderID", true).Value;

            //Resource验证, 非系统值 为UnKnown.
            XmppResource fromResource = Enum.TryParse<XmppResource>(fromResourceRaw, true, out fromResource) ? fromResource : XmppResource.Unknow;
            XmppResource toResource = Enum.TryParse<XmppResource>(toResourceRaw, true, out toResource) ? toResource : XmppResource.Unknow;
            //判断消息类型

            ReceptionChatFactory chatFactory = new ReceptionChatFactory(id, fromId, toId, messageBody, sessionId, fromResource, toResource);


            var extNamespace = ext_element.Namespace;
 
            //通过 ext节点的 namespace 确定chat类型
            switch (extNamespace.ToLower())
            {
                case "ihelper:chat:text":
                    return chatFactory.CreateChatText();
                case "ihelper:chat:media":
                    var mediaNode = ext_element.SelectSingleElement("msgObj");
                    var mediaUrl = mediaNode.GetAttribute("url");
                    //只保留文件名称部分
                    string mediaUrl_FileName = System.Text.RegularExpressions.Regex.Replace(mediaUrl, @".+GetFile\.ashx\?fileName=", string.Empty);
                    var mediaType = mediaNode.GetAttribute("type");
                    return chatFactory.CreateChatMedia(mediaUrl_FileName, mediaType);

                case "ihelper:chat:orderobj":
                    var ext_service = ext_element.SelectSingleElement("svcObj");
                    var ext_store = ext_element.SelectSingleElement("storeObj");

                    var serviceId = ext_service.GetAttribute("svcID");
                    var svcName = ext_service.GetAttribute("name");
                    var svcType = ext_service.GetAttribute("type");
                    var svcStarttime = ext_service.GetAttribute("startTime");
                    var svcEndtime = ext_service.GetAttribute("endTime");
                    var storeId = ext_store.GetAttribute("storeID");
                    var storeAlias = ext_store.GetAttribute("alias");
                    var storeAvatar = ext_store.GetAttribute("imgUrl");

                    PushedServiceInfo serviceInfo = new PushedServiceInfo(serviceId, svcName, svcType, svcStarttime, svcEndtime, storeId,
                        storeAlias, storeAvatar);
                    return chatFactory.CreateChatPushService(new List<PushedServiceInfo>() { serviceInfo });

                case "ihelper:notice:system":
                    return chatFactory.CreateNoticeSys();
                case "ihelper:notice:order":
                    var orderNode = ext_element.SelectSingleElement("orderObj");
                    var orderTitle = orderNode.GetAttribute("title");
                    string orderStatus = orderNode.GetAttribute("status");
                    var orderType = orderNode.GetAttribute("type");
                    //只保留文件名称部分
                    return chatFactory.CreateNoticeOrder(orderTitle, orderStatus, orderType);

                case "ihelper:notice:promote":
                    string promoteUrl = ext_element.SelectSingleElement("url").Value;
                    return chatFactory.CreateNoticePromote(promoteUrl);

                case "ihelper:notice:cer:change":

                    var ext_cerobj = ext_element.SelectSingleElement("cerObj");
                    var csId = ext_cerobj.GetAttribute("userID");
                    var csAlias = ext_cerobj.GetAttribute("userID");
                    var csAvatar = ext_cerobj.GetAttribute("imgUrl");
                    return chatFactory.CreateReAssign(csId, csAlias, csAvatar);
                case "ihelper:notice:cer:online":
                    return chatFactory.CreateNoticeCSOnline();

                case "ihelper:notice:cer:offline":
                    return chatFactory.CreateNoticeCSOffline();
                case "ihelper:notice:draft:new":
                    return chatFactory.CreateNoticeNewOrder();
                default:
                    throw new Exception("未知的命名空间");
            }
        }
        /// <summary>
        /// 将聊天对象转换为具体通讯协议规定的格式.
        /// </summary>
        /// <param name="chat"></param>
        /// <returns></returns>
        public Message ChatToMessage( ReceptionChat chat, string server)
        {


            Message msg = new Message();

            msg.SetAttribute("type", "chat");
            msg.Id = chat.Id != Guid.Empty ? chat.Id.ToString() : Guid.NewGuid().ToString();
            msg.To = new agsXMPP.Jid(chat.ToId + "@" + server + "/" + chat.ToResource);//发送对象
            msg.Body = chat.MessageBody;

            var nodeActive = new agsXMPP.Xml.Dom.Element("active", string.Empty, "http://jabber.org/protocol/chatstates");
            msg.AddChild(nodeActive);

            var extNode = new agsXMPP.Xml.Dom.Element("ext");
            var extOrderID = new agsXMPP.Xml.Dom.Element("orderID", chat.SessionId);
            extNode.AddChild(extOrderID);
            msg.AddChild(extNode);

            switch (chat.GetType().Name)
            {
                case "ReceptionChat": //1
                    extNode.Namespace = "ihelper:chat:text";
                    break;
                case "ReceptionChatMedia"://2
                    extNode.Namespace = "ihelper:chat:media";

                    var mediaUrl = ((ReceptionChatMedia)chat).MedialUrl;
                    var mediaType = ((ReceptionChatMedia)chat).MediaType;
                    var extMedia = new agsXMPP.Xml.Dom.Element("msgObj");
                    extMedia.SetAttribute("url", mediaUrl);
                    extMedia.SetAttribute("type", mediaType);
                    extNode.AddChild(extMedia);
                    break;
                case "ReceptionChatNoticeSys"://3
                    msg.SetAttribute("type", "headline");
                    extNode.Namespace = "ihelper:notice:system";
                    break;
                case "ReceptionChatNoticeOrder"://4
                    msg.SetAttribute("type", "headline");
                    extNode.Namespace = "ihelper:notice:order";
                    string orderTitle = ((ReceptionChatNoticeOrder)chat).OrderTitle;
                    string orderStatus = ((ReceptionChatNoticeOrder)chat).CurrentStatus.ToString();
                    string orderType = ((ReceptionChatNoticeOrder)chat).OrderType;
                    var extOrderObj = new agsXMPP.Xml.Dom.Element("orderObj");
                    extOrderObj.SetAttribute("title", orderTitle);
                    extOrderObj.SetAttribute("status", orderStatus);
                    extOrderObj.SetAttribute("type", orderType);
                    extNode.AddChild(extOrderObj);
                    break;
                case "ReceptionChatNoticePromote"://5
                    msg.SetAttribute("type", "headline");
                    extNode.Namespace = "ihelper:notice:promote";
                    var promoteUrl = ((ReceptionChatNoticePromote)chat).PromotionUrl;
                    var exPromote = new agsXMPP.Xml.Dom.Element("url");
                    exPromote.Value = promoteUrl;
                    extNode.AddChild(exPromote);
                    break;
                case "ReceptionChatReAssign"://6
                    msg.SetAttribute("type", "headline");
                    extNode.Namespace = "ihelper:notice:cer:change";
                    var cerObj = new agsXMPP.Xml.Dom.Element("cerObj");
                    cerObj.SetAttribute("userID", ((ReceptionChatReAssign)chat).ReAssignedCustomerServiceId);
                    cerObj.SetAttribute("alias", ((ReceptionChatReAssign)chat).CSAlias);
                    cerObj.SetAttribute("imgUrl", ((ReceptionChatReAssign)chat).CSAvatar);
                    extNode.AddChild(cerObj);

                    break;
                case "ReceptionChatNoticeCustomerServiceOffline"://7
                    msg.SetAttribute("type", "headline");
                    extNode.Namespace = "ihelper:notice:cer:offline";
                    break;
                case "ReceptionChatNoticeCustomerServiceOnline"://8
                    msg.SetAttribute("type", "headline");
                    extNode.Namespace = "ihelper:notice:cer:online";
                    break;
                case "ReceptionChatNoticeNewOrder"://9
                    msg.SetAttribute("type", "headline");
                    extNode.Namespace = "ihelper:notice:draft:new";

                    break;

                case "ReceptionChatPushService"://10
                    extNode.Namespace = "ihelper:chat:orderobj";

                    var svcObj = new agsXMPP.Xml.Dom.Element("svcObj");
                    ReceptionChatPushService chatPushService = (ReceptionChatPushService)chat;
                    foreach (var serviceInfo in chatPushService.ServiceInfos)
                    {
                        svcObj.SetAttribute("svcID", serviceInfo.ServiceId);
                        svcObj.SetAttribute("name", serviceInfo.ServiceName);
                        svcObj.SetAttribute("type", serviceInfo.ServiceType);
                        svcObj.SetAttribute("startTime", serviceInfo.ServiceStartTime);
                        svcObj.SetAttribute("endTime", serviceInfo.ServiceEndTime);
                        extNode.AddChild(svcObj);


                        var storeObj = new agsXMPP.Xml.Dom.Element("storeObj");
                        storeObj.SetAttribute("userID", serviceInfo.StoreUserId);
                        storeObj.SetAttribute("alias", serviceInfo.StoreAlias);
                        storeObj.SetAttribute("imgUrl", serviceInfo.StoreAvatar);
                         extNode.AddChild(storeObj);
                    }
                    break;
                case "ReceptionChatDidichuxing":
                    extNode.Namespace = "ihelper:hyper:didichuxing";

                    ReceptionChatDidichuxing chatDidichuxing = (ReceptionChatDidichuxing)chat;
                    var extHrefDidichuxing = new agsXMPP.Xml.Dom.Element("href", "/hypersmdias/"+ chatDidichuxing.Id);
                    extNode.AddChild(extHrefDidichuxing);
                    break;
                default:
                    log.Error("不需要处理的命名空间:" + chat.GetType().Name);
                    break;
            }
            return msg;
        }


        /// <summary>
        /// 将原始的xml文件转换成agsxmpp的 message对象
        /// </summary>
        /// <param name="rawXml"></param>
        /// <returns></returns>
        public Message RawXmlToMessage(string rawXml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(rawXml);

            agsXMPP.Xml.Dom.Document agsxDoc = new Document();
            

            XmlElement eleMessage = (XmlElement)doc.FirstChild;
            string fromJid = eleMessage.GetAttribute("from");
            string toJid = eleMessage.GetAttribute("to");
            string messageid = eleMessage.GetAttribute("id");
            //string type= eleMessage.GetAttribute("type");
            string xpathMessage = "/*[local-name()='message']";
            string xpathBody = xpathMessage + "/*[local-name()='body']";
            string xpathExt = xpathMessage + "/*[local-name()='ext']";
            string xpathOrderId = xpathExt + "/*[local-name()='orderID']";
            string xpathMsgObj = xpathExt + "/*[local-name()='msgObj']";
            string xpathSvcObj = xpathExt + "/*[local-name()='svcObj']";
            string xpathStoreObj = xpathExt + "/*[local-name()='storeObj']";
            string xpathCustomerObj = xpathExt + "/*[local-name()='customerObj']";
            string xpathOrderObj = xpathExt + "/*[local-name()='orderObj']";

            var extNode = doc.SelectSingleNode(xpathExt);
            string body = doc.SelectSingleNode(xpathBody).InnerText;
            string orderId = doc.SelectSingleNode(xpathOrderId).InnerText;

            MessageBuilder builder = new MessageBuilder( );
            builder = builder.BuildBase(messageid, toJid, fromJid, body, orderId);
            string extNameSpace = extNode.NamespaceURI;
            switch (extNameSpace.ToLower())
            {
                case "ihelper:chat:text": break;
                case "ihelper:chat:media":
                    var msgObj = doc.SelectSingleNode(xpathMsgObj);
                    string mediaType = msgObj.Attributes["type"].Value;
                    string mediaUrl = msgObj.Attributes["url"].Value;
                    builder = builder.BuildMedia(mediaType, mediaUrl);
                    break;
                case "ihelper:chat:userstatus":

                    break;
                case "ihelper:notice:cer:change":

                    break;
                case "ihelper:notice:order":
                    /* <orderID>SB-DKJI-OFNS-SDLK</orderID>  
    <orderObj title="集思优科的外卖" status="Created" type=""></orderObj> */
                    var orderObj = doc.SelectSingleNode(xpathOrderObj);
                    string orderObjStatus = orderObj.Attributes["status"].Value;
                    string orderObjType = orderObj.Attributes["type"].Value;
                    string orderObjTitle = orderObj.Attributes["title"].Value;
                    builder = builder.BuildNoticeOrder(orderObjTitle, orderObjStatus, orderObjType);
                    break;
                case "ihelper:chat:orderobj":
                    var svcObj = doc.SelectSingleNode(xpathSvcObj);
                    string svcID = svcObj.Attributes["svcID"].Value;
                    string svcName = svcObj.Attributes["name"].Value;
                    string svcType = svcObj.Attributes["type"].Value;
                    string startTime = svcObj.Attributes["startTime"].Value;
                    string endTime = svcObj.Attributes["endTime"].Value;
                    var storeObj = doc.SelectSingleNode(xpathStoreObj);
                    string userId = storeObj.Attributes["userID"].Value;
                    string alias = storeObj.Attributes["alias"].Value;
                    string imgUrl = storeObj.Attributes["imgUrl"].Value;

                    builder = builder.BuildPushedService(svcID, svcName, svcType, startTime, endTime,userId, alias, imgUrl);
                    break;
                default:
                    log.Error("不需要处理的命名空间:" + extNameSpace); break;
            }

            return builder.Message;
        }

        public Message RawXmlToMessage2(string rawXml)
        {
            agsXMPP.Xml.Dom.Document agsxDoc = new Document();
           // agsxDoc.LoadXml(rawXml) as Message;
            var message=agsxDoc.RootElement as agsXMPP.protocol.client.Message;
            return message;
        }
        public ReceptionChat RawXmlToChat(string rawXml)
        {

            return MessageToChat(RawXmlToMessage(rawXml));
        }
        /*
<message xmlns="jabber:client" 
        type="chat" 
        id="cf062bec-509d-4b90-84a9-a65a011b3d6e"
        to="4d63d740-5561-11e6-b7f0-001a7dda7106@localhost/YDBan_DemoClient">
    <body>2</body>
    <active xmlns="http://jabber.org/protocol/chatstates"/>
    <ext xmlns="ihelper:chat:text">
    <orderID>f7f0fdc2-3856-4e25-9b75-a65901436cab</orderID>
    </ext>
</message>



        <message to="4e2676e1-5561-11e6-b7f0-001a7dda7106@localhost/YDBan_CustomerService" from="4d63d740-5561-11e6-b7f0-001a7dda7106@localhost/YDBan_DemoClient" type="chat" id="d0ae54ba-8be4-4881-b3d7-a3b3d643f4bf"><body>ffffddd</body><active xmlns="http://jabber.org/protocol/chatstates"/><ext xmlns="ihelper:chat:text"><orderID>f7f0fdc2-3856-4e25-9b75-a65901436cab</orderID></ext></message>

 *   */
    }
}
