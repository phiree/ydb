using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Chat.Enums;
using Ydb.InstantMessage.DomainModel.Enums;

namespace Ydb.InstantMessage.DomainModel.Chat
{
    /// <summary>
    /// IM,信息,多媒体
    /// </summary>
    public class ReceptionChatMedia : ReceptionChat
    {
        public ReceptionChatMedia() { }
        public ReceptionChatMedia(string mediaurl, string mediatype,
         Guid id, string from, string to, string messageBody, string sessionId, XmppResource resourceFrom, XmppResource resourceTo)
            : base(id, from, to, messageBody, sessionId, ChatType.Chat, resourceFrom, resourceTo)
        {
            this.MedialUrl = mediaurl;
            this.MediaType = mediatype;
        }

        public virtual string MedialUrl { get; protected internal set; }
        public virtual string MediaType { get; protected internal set; }

        public virtual new ReceptionChatMediaDto ToDto()
        {
            ReceptionChatMediaDto dto = new ReceptionChatMediaDto();

            dto.Id = Id;
            dto.SessionId = SessionId;
            dto.SavedTime = SavedTime;
            dto.SavedTimestamp = SavedTimestamp;
            dto.SendTime = SendTime;
            dto.ReceiveTime = ReceiveTime;
            dto.ChatType = ChatType.ToString();
            dto.FromId = FromId;
            dto.ToId = ToId;
            dto.MessageBody = MedialUrl != null ? Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + MedialUrl : "";
            dto.ChatTarget = ChatTarget.ToString();
            dto.FromResource = FromResource.ToString();
            dto.FromResourceName = FromResourceName;
            dto.ToResource = ToResource.ToString();
            dto.IsReaded = IsReaded;
            dto.IsfromCustomerService = IsfromCustomerService;

            dto.MedialUrl = MedialUrl;
            dto.MediaType = MediaType;

            return dto;
        }
    }
}
