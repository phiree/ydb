using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
namespace Dianzhu.DAL.Mapping
{
    public class IMUserStatusArchieveMap : ClassMap<IMUserStatusArchieve>
    {
        public IMUserStatusArchieveMap() { 
            Id(x=>x.Id);
            Map(x => x.UserIdRaw);
            Map(x => x.UserID);
            Map(x => x.Status).CustomType<Model.Enums.enum_UserStatus>();
            Map(x => x.ArchieveTime);
            Map(x => x.IpAddress);
            Map(x => x.OFIpAddress);
            Map(x => x.ClientName);
        }
    }
}
