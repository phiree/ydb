using Dianzhu.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.Model
{

    /// <summary>
    /// 接待中的聊天记录.
    /// </summary>
    public class ReceptionChatFactory
    {

        Guid id;string sessionId;DateTime savedTime;double savedTimeStamp;DateTime sendTime;DateTime receivedTime ;string fromId;string toId;
        string messageBody; enum_XmppResource fromResource;enum_XmppResource toResource;
      
        public ReceptionChatFactory(Guid id, string from, string to, string messageBody, string sessionId,
           
            enum_XmppResource resourceFrom, enum_XmppResource resourceTo)
        {
            this.id = id;
            this.savedTime = DateTime.Now;
            this.savedTimeStamp = (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
            //构造传入.
            this.fromId = from;
            this.toId = to;
            this.messageBody = messageBody;
            this.fromResource = resourceFrom;
            this.toResource = resourceTo;
            this.sessionId = sessionId;
           
        }
        public ReceptionChat CreateChatText( )
        {
            return new ReceptionChat(id, fromId, toId, messageBody, sessionId, enum_ChatType.Chat, fromResource, toResource);

        }
        public ReceptionChat CreateChatMedia(string mediaUrl,string mediaType)
        {
            return new ReceptionChatMedia(mediaUrl, mediaType,id, fromId, toId, messageBody, sessionId, fromResource, toResource);

        }
        public ReceptionChat CreateNoticeSys( )
        {
            return new ReceptionChatNoticeSys(id, fromId, toId, messageBody, sessionId, fromResource, toResource);

        }
        public ReceptionChat CreateNoticeOrder(string orderTilte, enum_OrderStatus orderStatus, string orderType)
        {
            return new ReceptionChatNoticeOrder( orderTilte,  orderStatus,  orderType,id, fromId, toId, messageBody, sessionId, fromResource, toResource);

        }
        public ReceptionChat CreateReAssign(string reAssignedCustomerServiceId, string csAlias, string csAvatar)
        {
            return new ReceptionChatReAssign(  reAssignedCustomerServiceId,   csAlias,   csAvatar,
              id, fromId, toId, messageBody, sessionId, fromResource, toResource);

        }
        public ReceptionChat CreateNoticePromote(string promotionUrl)
        {
            return new ReceptionChatNoticePromote(promotionUrl,id, fromId, toId, messageBody, sessionId, fromResource, toResource);

        }
        public ReceptionChat CreateNoticeCSOffline(   )
        {
            return new ReceptionChatNoticeCustomerServiceOffline(   id, fromId, toId, messageBody, sessionId, fromResource, toResource);

        }
        public ReceptionChat CreateNoticeCSOnline( )
        {
            return new ReceptionChatNoticeCustomerServiceOnline(id, fromId, toId, messageBody, sessionId, fromResource, toResource);

        }
        public ReceptionChat CreateNoticeUserStatus(string userId, enum_UserStatus userStatus)
        {
            return new ReceptionChatUserStatus(  userId,   userStatus,id, fromId, toId, messageBody, sessionId, fromResource, toResource);

        }
        public ReceptionChat CreateChatPushService(IList<PushedServiceInfo> serviceInfos)
        {
            return new ReceptionChatPushService(serviceInfos,id, fromId, toId, messageBody, sessionId, fromResource, toResource);

        }
        public ReceptionChat CreateNoticeNewOrder( )
        {
            return new ReceptionChatNoticeNewOrder( id, fromId, toId, messageBody, sessionId, fromResource, toResource);

        }
        

    }
     
}