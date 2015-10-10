using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
 
namespace Dianzhu.DAL.Mapping
{
    public class ReceptionMap : ClassMap<ReceptionBase>
    {
        public ReceptionMap()
        {
            Id(x => x.Id);
            HasMany(x => x.ChatHistory).Cascade.All();
             
            
            Map(x => x.IsComplete);
            References<DZMembership>(x => x.Receiver);

            Map(x => x.SatisfactionRate);
            References<DZMembership>(x => x.Sender);
            Map(x => x.TimeBegin);
            Map(x => x.TimeEnd);
            
        }
    }
    public class ReceptionCustomerMap : SubclassMap<ReceptionCustomer>
    {
        public ReceptionCustomerMap()
        {
            HasMany<ServiceOrder>(x => x.Orders);
            References<CustomerService>(x => x.TransferFrom);
            HasMany<DZService>(x => x.PushedServices);
            
        }
    }
    public class ReceptionChatMap : ClassMap<ReceptionChat>
    {
        public ReceptionChatMap()
        {
            Id(x => x.Id).UnsavedValue(Guid.Empty);
            Map(x => x.SavedTime);
            Map(x => x.MessageBody);
            Map(x => x.ReceiveTime);
            Map(x => x.SendTime);
            References<DZMembership>(x => x.To);
            References<DZMembership>(x => x.From);
            References<ServiceOrder>(x => x.ServiceOrder);
            Map(x => x.ChatType).CustomType<int>();
            Map(x => x.Version);
            References<ReceptionBase>(x => x.Reception).Cascade.All().Column("ReceptionBase_id");
        }
    }
   
 
   

    public class ReceptionChatMediaMap : SubclassMap<ReceptionChatMedia>
    {
        public ReceptionChatMediaMap()
        {
            Map(x => x.MedialUrl);
            Map(x => x.MediaType);
        }
    }
    public class ReceptionChatReAssignMap : SubclassMap<ReceptionChatReAssign>
    {
        public ReceptionChatReAssignMap()
        {
            References<DZMembership>(x => x.ReAssignedCustomerService);
        }
    }
    public class ReceptionChatNoticeMap : SubclassMap<ReceptionChatNotice>
    {
        public ReceptionChatNoticeMap()
        {
            References<DZMembership>(x => x.UserObj);
        }
    }






}
