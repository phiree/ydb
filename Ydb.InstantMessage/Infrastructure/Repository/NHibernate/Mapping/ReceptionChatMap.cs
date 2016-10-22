using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Chat;
using FluentNHibernate.Mapping;
using Ydb.Common.Domain;
namespace Ydb.InstantMessage.Infrastructure.Repository.NHibernate.Mapping
{
    public class ReceptionChatMap:ClassMap<ReceptionChat>
    {
        public ReceptionChatMap()
        {
            Id(x => x.Id);
            Map(x => x.ChatTarget).CustomType<Ydb.InstantMessage.DomainModel.Chat.Enums.ChatTarget>();
            Map(x => x.ChatType).CustomType<Ydb.InstantMessage.DomainModel.Chat.Enums.ChatType>();
            Map(x => x.FromId);
            Map(x => x.FromResource).CustomType<Ydb.InstantMessage.DomainModel.Enums.XmppResource>();
         
            Map(x => x.IsReaded);
            Map(x => x.MessageBody);
            Map(x => x.ReceiveTime);
            Map(x => x.SavedTime);
            Map(x => x.SavedTimestamp);
            Map(x => x.SendTime);
            Map(x => x.SessionId);
            Map(x => x.ToId);
            Map(x => x.ToResource).CustomType<Ydb.InstantMessage.DomainModel.Enums.XmppResource>();
            

        }
    }
}
