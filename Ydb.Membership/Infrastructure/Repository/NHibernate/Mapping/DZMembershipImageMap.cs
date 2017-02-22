using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel;
using Ydb.Membership.DomainModel.Enums;
using FluentNHibernate.Mapping;

namespace Ydb.Membership.Infrastructure.Repository.NHibernate.Mapping
{
    public class DZMembershipImageMap : ClassMap<DZMembershipImage>
    {
        public DZMembershipImageMap()
        {
            Id(x => x.Id);
            Map(x => x.Description);
            Map(x => x.ImageName);
            Map(x => x.ImageType).CustomType<DZMembershipImageType>();
            Map(x => x.OrderNumber);
            Map(x => x.Size);
            Map(x => x.UploadTime);
            Map(x => x.IsCurrent);
        }
    }
}
