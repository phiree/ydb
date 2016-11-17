using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
using Ydb.Common;

namespace Dianzhu.DAL.Mapping
{
    public class IMUserStatusMap : ClassMap<IMUserStatus>
    {
        public IMUserStatusMap() { 
            Id(x=>x.Id);
            Map(x => x.UserIdRaw);
            Map(x => x.UserID);
            Map(x => x.Status).CustomType<enum_UserStatus>();
            Map(x => x.LastModifyTime);
            Map(x => x.IpAddress);
            Map(x => x.OFIpAddress);
            Map(x => x.ClientName);
        }
    }

    public class IMUserStatusArchieveMap : ClassMap<IMUserStatusArchieve>
    {
        public IMUserStatusArchieveMap()
        {
            Id(x => x.Id);
            Map(x => x.UserIdRaw);
            Map(x => x.UserID);
            Map(x => x.Status).CustomType<enum_UserStatus>();
            Map(x => x.ArchieveTime);
            Map(x => x.IpAddress);
            Map(x => x.OFIpAddress);
            Map(x => x.ClientName);
        }
    }
}
