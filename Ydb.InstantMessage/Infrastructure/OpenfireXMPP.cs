using System;
using System.Collections.Generic;
using System.Timers;
using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPP.Xml.Dom;
using log4net;
using Ydb.InstantMessage.Application;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.InstantMessage.DomainModel.Enums;
using Ydb.InstantMessage.DomainModel.Reception;

namespace Ydb.InstantMessage.Infrastructure
{
    /// <summary>
    ///     通讯接口的 XMPP实现. 使用 agsxmpp 类库.
    /// </summary>
    public class OpenfireXMPP : IInstantMessage
    {
        private static XmppClientConnection XmppClientConnection;
        private readonly ILog log = LogManager.GetLogger("Ydb.InstantMessage.Infrastructure.OpenfireXMPP");

        private readonly IMessageAdapter messageAdapter;
        private readonly IRepositoryChat repositoryChat;
        private IReceptionAssigner receptionAssigner;

        public OpenfireXMPP(string server, string domain, IMessageAdapter messageAdapter,
            IReceptionAssigner receptionAssigner, IRepositoryChat repositoryChat, string resourceName)
            : this(server, domain, messageAdapter, receptionAssigner, repositoryChat)
        {
            XmppClientConnection.Resource = resourceName;
        }

        public OpenfireXMPP(string server, string domain, IMessageAdapter messageAdapter,
            IReceptionAssigner receptionAssigner, IRepositoryChat repositoryChat)
        {
            Server = server;
            Domain = domain;
            this.messageAdapter = messageAdapter;
            this.receptionAssigner = receptionAssigner;
            this.repositoryChat = repositoryChat;

            if (XmppClientConnection == null)
            {
                XmppClientConnection = new XmppClientConnection();
                XmppClientConnection.Server = domain;
                XmppClientConnection.ConnectServer = server;
                XmppClientConnection.AutoResolveConnectServer = false;
                XmppClientConnection.OnLogin += Connection_OnLogin;
                XmppClientConnection.OnPresence += Connection_OnPresence;
                XmppClientConnection.OnMessage += XmppClientConnection_OnMessage;
                XmppClientConnection.OnAuthError += XmppClientConnection_OnAuthError;
                XmppClientConnection.OnError += XmppClientConnection_OnError;
                XmppClientConnection.OnSocketError += XmppClientConnection_OnSocketError;
                XmppClientConnection.OnClose += XmppClientConnection_OnClose;
                XmppClientConnection.OnIq += XmppClientConnection_OnIq;
                XmppClientConnection.OnStreamError += XmppClientConnection_OnStreamError;
            }
        }

        public event IMClosed IMClosed;

        public event IMLogined IMLogined;

        public event IMAuthError IMAuthError;

        public event IMPresent IMPresent;

        public event IMReceivedMessage IMReceivedMessage;

        public event IMError IMError;

        public event IMConnectionError IMConnectionError;

        public event IMIQ IMIQ;

        public event IMStreamError IMStreamError;

        public string Server { get; } = string.Empty;

        public string Domain { get; } = string.Empty;

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
            log.Debug("xmpp open connection:" + userName);
            XmppClientConnection.Open(userName, password);
        }

        public void SendMessageMedia(Guid messageId, string mediaUrl, string mediaType, string to, string sessionId,
            string toResource)
        {
            XmppResource resourceTo;
            if (!Enum.TryParse(toResource, out resourceTo))
                throw new Exception("传入的toResource有误");
            var receptionChatFactory = new ReceptionChatFactory(messageId, string.Empty, to, string.Empty, sessionId,
                XmppResource.Unknow, resourceTo);
            var chat = receptionChatFactory.CreateChatMedia(mediaUrl, mediaType);
            SendMessage(chat);
        }

        public void SendMessageText(Guid messageId, string messageBogy, string to, string toResource, string sessionId)
        {
            XmppResource resourceTo;
            if (!Enum.TryParse(toResource, out resourceTo))
            {
                //  throw new Exception("传入的toResource有误");
                resourceTo = XmppResource.Unknow;
            }
            var receptionChatFactory = new ReceptionChatFactory(messageId, string.Empty, to, messageBogy, sessionId,
                XmppResource.Unknow, resourceTo);
            var chat = receptionChatFactory.CreateChatText();
            SendMessage(chat);
        }

        public void OpenConnection(string userName, string password, string resource)
        {
            log.Debug("xmpp open connection:" + userName);
            XmppClientConnection.Open(userName, password, resource);
        }

        public void SendMessagePushService(Guid messageId, IList<PushedServiceInfo> serviceInfos, string messageBody,
            string to, string toResource, string sessionId)
        {
            XmppResource resourceTo;
            if (!Enum.TryParse(toResource, out resourceTo))
                throw new Exception("传入的toResource有误");
            var receptionChatFactory = new ReceptionChatFactory(messageId, string.Empty, to, messageBody, sessionId,
                XmppResource.Unknow, resourceTo);
            var chat = receptionChatFactory.CreateChatPushService(serviceInfos);
            SendMessage(chat);
        }

        public void SendCSLoginMessage(Guid messageId, string messageBody, string to, string toResource,
            string sessionId)
        {
            XmppResource resourceTo;
            if (!Enum.TryParse(toResource, out resourceTo))
                throw new Exception("传入的toResource有误");
            var receptionChatFactoryDD = new ReceptionChatFactory(messageId, string.Empty, to, messageBody, sessionId,
                XmppResource.Unknow, resourceTo);
            var chatDD = receptionChatFactoryDD.CreateNoticeCSOnline();
            SendMessage(chatDD);
        }

        public void SendCSLogoffMessage(Guid messageId, string messageBody, string to, string toResource,
            string sessionId)
        {
            XmppResource resourceTo;
            if (!Enum.TryParse(toResource, out resourceTo))
                throw new Exception("传入的toResource有误");
            var receptionChatFactoryDD = new ReceptionChatFactory(messageId, string.Empty, to, messageBody, sessionId,
                XmppResource.Unknow, resourceTo);
            var chatDD = receptionChatFactoryDD.CreateNoticeCSOffline();
            SendMessage(chatDD);
        }

        public void SendNoticeOrderChangeStatus(string orderTilte, string orderStatus, string orderType,
            Guid messageId, string messageBody, string to, string toResource, string sessionId)
        {
            XmppResource resourceTo;
            if (!Enum.TryParse(toResource, out resourceTo))
                throw new Exception("传入的toResource有误");
            var receptionChatFactory = new ReceptionChatFactory(messageId, string.Empty, to, messageBody, sessionId,
                XmppResource.Unknow, resourceTo);
            var chat = receptionChatFactory.CreateNoticeOrder(orderTilte, orderStatus, orderType);
            SendMessage(chat);
        }

        public void SendNoticeNewOrder(Guid messageId, string to, string toResource, string sessionId)
        {
            XmppResource resourceTo;
            if (!Enum.TryParse(toResource, out resourceTo))
                throw new Exception("传入的toResource有误");
            var receptionChatFactory = new ReceptionChatFactory(messageId, string.Empty, to, string.Empty, sessionId,
                XmppResource.Unknow, resourceTo);
            var chat = receptionChatFactory.CreateNoticeNewOrder();
            SendMessage(chat);
        }

        public void SendReAssignToCustomer(string reAssignedCustomerServiceId, string csAlias, string csAvatar,
            Guid messageId, string to, string toResource, string sessionId)
        {
            XmppResource resourceTo;
            if (!Enum.TryParse(toResource, out resourceTo))
                throw new Exception("传入的toResource有误");
            var receptionChatFactory = new ReceptionChatFactory(messageId, string.Empty, to, string.Empty, sessionId,
                XmppResource.Unknow, resourceTo);
            var chat = receptionChatFactory.CreateReAssign(reAssignedCustomerServiceId, csAlias, csAvatar);
            SendMessage(chat);
        }

        public void SendDidichuxing(string fromlat, string fromlng, string fromaddr, string fromname, string tolat,
            string tolng, string toaddr, string toname, string phone, Guid messageId, string to, string toResource,
            string sessionId)
        {
            XmppResource resourceTo;
            if (!Enum.TryParse(toResource, out resourceTo))
                throw new Exception("传入的toResource有误");
            var receptionChatFactory = new ReceptionChatFactory(messageId, string.Empty, to, string.Empty, sessionId,
                XmppResource.Unknow, resourceTo);
            var chat = receptionChatFactory.CreateDidichuxing(fromlat, fromlng, fromaddr, fromname, tolat, tolng, toaddr,
                toname, phone);
            repositoryChat.Add(chat);
            SendMessage(chat);
        }

        private void XmppClientConnection_OnStreamError(object sender, Element e)
        {
            log.Debug("Receive  StreamError:" + e);
            if (IMStreamError != null)
                IMStreamError();
        }

        private void XmppClientConnection_OnIq(object sender, IQ iq)
        {
            log.Debug("receive_iq:" + iq);
            if (iq.Type == IqType.get)
            {
                var pong = new IQ(IqType.result);
                pong.To = iq.From;
                SendMessage(pong.ToString());
                log.Debug("send_iq:" + pong);
            }
        }

        private void XmppClientConnection_OnSocketError(object sender, Exception ex)
        {
            log.Debug("Receive  SocketError:" + ex.Message);
            if (IMConnectionError == null) return;
            IMConnectionError(ex.Message);
        }

        private void XmppClientConnection_OnError(object sender, Exception ex)
        {
            log.Debug("Receive  Error:" + ex.Message);
            if (IMError == null) return;
            IMError(ex.Message);
        }

        private void XmppClientConnection_OnAuthError(object sender, Element e)
        {
            log.Debug("Receive AuthError:" + e);
            if (IMAuthError == null) return;
            IMAuthError();
        }

        private void XmppClientConnection_OnMessage(object sender, Message msg)
        {
            //Action a = () => {
            //接受消息,由presenter构建chat
            //message-->chat
            //1 转换为chat对象
            log.Debug("receive_msg:" + msg);

            if (IMReceivedMessage != null)
                try
                {
                    var chat = messageAdapter.MessageToChat(msg);
                    // new Model.ReceptionChat();// MessageAdapter.MessageToChat(msg);
                    var dtoChat = new ReceptionChatDto();
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

            //};
            //NHibernateUnitOfWork.With.Transaction(a);
        }

        private void Connection_OnPresence(object sender, Presence pres)
        {
            log.Debug("Receive Presence:" + pres);

            if (IMPresent != null)
                IMPresent(pres.From.User, (int)pres.Type);
        }

        private void Connection_OnLogin(object sender)
        {
            if (IMLogined == null) return;
            IMLogined(XmppClientConnection.Username);

            //每隔一段时间给服务发送一个ping,防止连接超时.
            var tmHeartBeat = new Timer();
            tmHeartBeat.Elapsed += TmHeartBeat_Elapsed;
            tmHeartBeat.Interval = 5 * 60 * 1000;
            tmHeartBeat.Start();
        }

        private void TmHeartBeat_Elapsed(object sender, ElapsedEventArgs e)
        {
            var iqHeartBeat = new IQ(IqType.get, XmppClientConnection.MyJID, Server);
            var pingNode = new Element("ping");
            pingNode.Namespace = "urn:xmpp:ping";
            iqHeartBeat.AddChild(pingNode);

            XmppClientConnection.Send(iqHeartBeat);
            log.Debug("HeartBeat" + iqHeartBeat);
        }

        public void SendPresent()
        {
            var p = new Presence(ShowType.chat, "Online");
            p.Type = PresenceType.available;
            log.Debug("send present:" + p);
            XmppClientConnection.Send(p);
        }

        public void SendMessage(ReceptionChat chat)
        {
            //判断用户对应的tokoen
            //chat-->message
            var msg = messageAdapter.ChatToMessage(chat, Domain);
            log.Debug("send chat message" + msg);
            XmppClientConnection.Send(msg);
        }

        public void SendNotice(string title, string content)
        {
        } //todo:发送消息 应该分离出来, 用来处理 xmpp发送和 push.

        public void XmppClientConnection_OnClose(object sender)
        {
            log.Debug("Connection closed");
            if (IMClosed == null) return;
            IMClosed();
        }
    }
}