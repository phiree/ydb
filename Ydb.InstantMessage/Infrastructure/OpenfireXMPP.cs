using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
using agsXMPP.protocol.client;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.InstantMessage.Application;
using Ydb.InstantMessage.DomainModel.Enums;
using Ydb.InstantMessage.DomainModel.Reception;

namespace Ydb.InstantMessage.Infrastructure
{
    /// <summary>
    /// 通讯接口的 XMPP实现. 使用 agsxmpp 类库.
    /// </summary>
    public class OpenfireXMPP : IInstantMessage
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Ydb.InstantMessage.Infrastructure.OpenfireXMPP");
 
        static agsXMPP.XmppClientConnection XmppClientConnection;


        public event IMClosed IMClosed;
        public event IMLogined IMLogined;
        public event IMAuthError IMAuthError;
        public event IMPresent IMPresent;
        public event IMReceivedMessage IMReceivedMessage;
        public event IMError IMError;
        public event IMConnectionError IMConnectionError;
        public event IMIQ IMIQ;
        public event IMStreamError IMStreamError;
        IMessageAdapter messageAdapter;
        IReceptionAssigner receptionAssigner;
        private string server = string.Empty;
        private string domain = string.Empty;
        public string Server
        {
            get { return server; }
        }
        public string Domain
        {
            get { return domain; }
        }
        public OpenfireXMPP(string server,string domain, IMessageAdapter messageAdapter, IReceptionAssigner receptionAssigner, string resourceName)
            : this(server,domain, messageAdapter, receptionAssigner)
        {
            XmppClientConnection.Resource = resourceName;
        }
        public OpenfireXMPP(string server,string domain, IMessageAdapter messageAdapter, IReceptionAssigner receptionAssigner)
        {
            this.server = server;
            this.domain = domain;
            this.messageAdapter = messageAdapter;
            this.receptionAssigner = receptionAssigner;

            if (XmppClientConnection == null)
            {
                XmppClientConnection = new agsXMPP.XmppClientConnection();
                XmppClientConnection.Server =domain;
                XmppClientConnection.ConnectServer = server;
                XmppClientConnection.AutoResolveConnectServer = false;
                XmppClientConnection.OnLogin += new agsXMPP.ObjectHandler(Connection_OnLogin);
                XmppClientConnection.OnPresence += new PresenceHandler(Connection_OnPresence);
                XmppClientConnection.OnMessage += new MessageHandler(XmppClientConnection_OnMessage);
                XmppClientConnection.OnAuthError += new XmppElementHandler(XmppClientConnection_OnAuthError);
                XmppClientConnection.OnError += new ErrorHandler(XmppClientConnection_OnError);
                XmppClientConnection.OnSocketError += new ErrorHandler(XmppClientConnection_OnSocketError);
                XmppClientConnection.OnClose += new ObjectHandler(XmppClientConnection_OnClose);
                XmppClientConnection.OnIq += XmppClientConnection_OnIq;
                XmppClientConnection.OnStreamError += XmppClientConnection_OnStreamError;

            }
        }

        private void XmppClientConnection_OnStreamError(object sender, agsXMPP.Xml.Dom.Element e)
        {
            log.Debug("Receive  StreamError:" + e.ToString());
            if (IMStreamError != null)
            {
                IMStreamError();
            }            
        }

        private void XmppClientConnection_OnIq(object sender, IQ iq)
        {
           
            log.Debug("receive_iq:" + iq.ToString());
            if (iq.Type == IqType.get)
            {
                var pong = new IQ(IqType.result);
                pong.To = iq.From;
                SendMessage(pong.ToString());
                log.Debug("send_iq:" + pong);
            }
        }
        void XmppClientConnection_OnSocketError(object sender, Exception ex)
        {
            log.Debug("Receive  SocketError:" + ex.Message);
            if (IMConnectionError == null) return;
            IMConnectionError(ex.Message);
        }
        void XmppClientConnection_OnError(object sender, Exception ex)
        {
            log.Debug("Receive  Error:" + ex.Message);
            if (IMError == null) return;
            IMError(ex.Message);
        }
        void XmppClientConnection_OnAuthError(object sender, agsXMPP.Xml.Dom.Element e)
        {
            log.Debug("Receive AuthError:" + e.ToString());
            if (IMAuthError == null) return;
            IMAuthError();
        }
        void XmppClientConnection_OnMessage(object sender, Message msg)
        {

            //Action a = () => { 
            //接受消息,由presenter构建chat
            //message-->chat
            //1 转换为chat对象
            log.Debug("receive_msg:" + msg.ToString());

            if (IMReceivedMessage != null)
            {
                try
                {
                    ReceptionChat chat = messageAdapter.MessageToChat(msg);// new Model.ReceptionChat();// MessageAdapter.MessageToChat(msg);
                    ReceptionChatDto dtoChat = new ReceptionChatDto();
                    switch (chat.GetType().Name)
                    {
                        case "ReceptionChat":
                            dtoChat = chat.ToDto();
                            break;
                        case "ReceptionChatMedia":
                            dtoChat = ((ReceptionChatMedia)chat).ToDto();
                            break;
                        case "ReceptionChatPushService":
                            dtoChat = ((ReceptionChatPushService)chat).ToDto();
                            break;
                        case "ReceptionChatNoticeCustomerServiceOnline":
                        case "ReceptionChatNoticeCustomerServiceOffline":
                        case "ReceptionChatNoticeOrder":
                            throw new Exception("暂时不处理该类型聊天消息");
                        default:
                            throw new Exception("未知chat类型");
                    }

                    IMReceivedMessage(dtoChat);
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                  //  PHSuit.ExceptionLoger.ExceptionLog(log, ex);
                }
            }

            //};
            //NHibernateUnitOfWork.With.Transaction(a);

        }
        void Connection_OnPresence(object sender, Presence pres)
        {
            log.Debug("Receive Presence:" + pres);
            
            if (IMPresent != null)
            {
                
                IMPresent(pres.From.User, (int)pres.Type);
            }
        }
        void Connection_OnLogin(object sender)
        {
            if (IMLogined == null) return;
            IMLogined(XmppClientConnection.Username);

            //每隔一段时间给服务发送一个ping,防止连接超时.
            System.Timers.Timer tmHeartBeat = new System.Timers.Timer();
            tmHeartBeat.Elapsed += TmHeartBeat_Elapsed;
            tmHeartBeat.Interval = 5 * 60 * 1000;
            tmHeartBeat.Start();
        }
        private void TmHeartBeat_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            IQ iqHeartBeat = new IQ(IqType.get, XmppClientConnection.MyJID, server);
            var pingNode = new agsXMPP.Xml.Dom.Element("ping");
            pingNode.Namespace = "urn:xmpp:ping";
            iqHeartBeat.AddChild(pingNode);

            XmppClientConnection.Send(iqHeartBeat);
            log.Debug("HeartBeat"+iqHeartBeat.ToString());
        }
        public void SendPresent()
        {
          
            Presence p = new Presence(ShowType.chat, "Online");
            p.Type = PresenceType.available;
            log.Debug("send present:"+p.ToString());
            XmppClientConnection.Send(p);
        }
        public void SendMessage(ReceptionChat chat)
        {
            //判断用户对应的tokoen
            //chat-->message
            Message msg = messageAdapter.ChatToMessage(chat, domain);
            log.Debug("send chat message" + msg.ToString());
            XmppClientConnection.Send(msg);

        }
        //todo:发送消息 应该分离出来, 用来处理 xmpp发送和 push.
        
        public void SendMessage(string xml)
        {
            log.Debug("send xml message" + xml);
            XmppClientConnection.Send(xml);
        }
        public void Close()
        {
            log.Debug("xmppconenction close ");
            XmppClientConnection.Close();
        }
        public void OpenConnection(string userName, string password)
        {
            log.Debug("xmpp open connection:"+userName);
            XmppClientConnection.Open(userName, password);
        }
        public void XmppClientConnection_OnClose(object sender)
        {
            log.Debug("Connection closed");
            if (IMClosed == null) return;
            IMClosed();
        }

        public void SendMessageMedia(Guid messageId, string mediaUrl, string mediaType, string to, string sessionId, string toResource)
        {
            XmppResource resourceTo;
            if(!Enum.TryParse(toResource,out resourceTo))
            {
                throw new Exception("传入的toResource有误");
            }
            ReceptionChatFactory receptionChatFactory = new ReceptionChatFactory(messageId, string.Empty, to, string.Empty, sessionId, XmppResource.Unknow, resourceTo);
            ReceptionChat chat = receptionChatFactory.CreateChatMedia(mediaUrl, mediaType);
            SendMessage(chat);
        }

        public void SendMessageText(Guid messageId, string messageBogy, string to, string toResource, string sessionId)
        {
            XmppResource resourceTo;
            if (!Enum.TryParse(toResource, out resourceTo))
            {
                throw new Exception("传入的toResource有误");
            }
            ReceptionChatFactory receptionChatFactory = new ReceptionChatFactory(messageId, string.Empty, to, messageBogy, sessionId, XmppResource.Unknow, resourceTo);
            ReceptionChat chat = receptionChatFactory.CreateChatText();
            SendMessage(chat);
        }

        public void OpenConnection(string userName, string password, string resource)
        {
            log.Debug("xmpp open connection:" + userName);
            XmppClientConnection.Open(userName, password, resource);
        }

        public void SendMessagePushService(Guid messageId, IList<PushedServiceInfo> serviceInfos, string messageBody, string to, string toResource, string sessionId)
        {
            XmppResource resourceTo;
            if (!Enum.TryParse(toResource, out resourceTo))
            {
                throw new Exception("传入的toResource有误");
            }
            ReceptionChatFactory receptionChatFactory = new ReceptionChatFactory(messageId, string.Empty, to, messageBody, sessionId, XmppResource.Unknow, resourceTo);
            ReceptionChat chat = receptionChatFactory.CreateChatPushService(serviceInfos);
            SendMessage(chat);
        }

        public void SendCSLoginMessage(Guid messageId, string messageBody, string to, string toResource, string sessionId)
        {
            XmppResource resourceTo;
            if (!Enum.TryParse(toResource, out resourceTo))
            {
                throw new Exception("传入的toResource有误");
            }
            ReceptionChatFactory receptionChatFactoryDD = new ReceptionChatFactory(messageId, string.Empty, to, messageBody, sessionId, XmppResource.Unknow, resourceTo);
            ReceptionChat chatDD = receptionChatFactoryDD.CreateNoticeCSOnline();
            SendMessage(chatDD);
        }

        public void SendCSLogoffMessage(Guid messageId, string messageBody, string to, string toResource, string sessionId)
        {
            XmppResource resourceTo;
            if (!Enum.TryParse(toResource, out resourceTo))
            {
                throw new Exception("传入的toResource有误");
            }
            ReceptionChatFactory receptionChatFactoryDD = new ReceptionChatFactory(messageId, string.Empty, to, messageBody, sessionId, XmppResource.Unknow, resourceTo);
            ReceptionChat chatDD = receptionChatFactoryDD.CreateNoticeCSOffline();
            SendMessage(chatDD);
        }

        public void SendNoticeOrderChangeStatus(string orderTilte, string orderStatus, string orderType,
            Guid messageId, string messageBody, string to, string toResource, string sessionId)
        {
            XmppResource resourceTo;
            if (!Enum.TryParse(toResource, out resourceTo))
            {
                throw new Exception("传入的toResource有误");
            }
            ReceptionChatFactory receptionChatFactory = new ReceptionChatFactory(messageId, string.Empty, to, messageBody, sessionId, XmppResource.Unknow, resourceTo);
            ReceptionChat chat = receptionChatFactory.CreateNoticeOrder(orderTilte,orderStatus,orderType);
            SendMessage(chat);
        }

        public void SendNoticeNewOrder(Guid messageId, string to, string toResource, string sessionId)
        {
            XmppResource resourceTo;
            if (!Enum.TryParse(toResource, out resourceTo))
            {
                throw new Exception("传入的toResource有误");
            }
            ReceptionChatFactory receptionChatFactory = new ReceptionChatFactory(messageId, string.Empty, to, string.Empty, sessionId, XmppResource.Unknow, resourceTo);
            ReceptionChat chat = receptionChatFactory.CreateNoticeNewOrder();
            SendMessage(chat);
        }

        public void SendReAssignToCustomer(string reAssignedCustomerServiceId, string csAlias, string csAvatar,
            Guid messageId, string to, string toResource, string sessionId)
        {
            XmppResource resourceTo;
            if (!Enum.TryParse(toResource, out resourceTo))
            {
                throw new Exception("传入的toResource有误");
            }
            ReceptionChatFactory receptionChatFactory = new ReceptionChatFactory(messageId, string.Empty, to, string.Empty, sessionId, XmppResource.Unknow, resourceTo);
            ReceptionChat chat = receptionChatFactory.CreateReAssign(reAssignedCustomerServiceId, csAlias, csAvatar);
            SendMessage(chat);
        }
    }
}
