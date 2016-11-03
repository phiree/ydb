using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Membership.DomainModel;
using Ydb.Membership.DomainModel.Enums;
namespace Ydb.Membership.Infrastructure.Repository.NHibernate.Mapping
{
    public class DZMembershipQQMap : SubclassMap<DZMembershipQQ>
    {
        public DZMembershipQQMap()
        {
            Map(x => x.ClientId);
            Map(x => x.OpenId);
            Map(x => x.Ret);
            Map(x => x.Msg);
            Map(x => x.IsLost);
            Map(x => x.NickName);
            Map(x => x.Gender);
            Map(x => x.Province);
            Map(x => x.City);
            Map(x => x.Year);
            Map(x => x.Figureurl);
            Map(x => x.Figureurl1);
            Map(x => x.Figureurl2);
            Map(x => x.FigureurlQq1);
            Map(x => x.FigureurlQq2);
            Map(x => x.IsYellowVip);
            Map(x => x.Vip);
            Map(x => x.YellowVipLevel);
            Map(x => x.Level);
            Map(x => x.IsYellowYearVip);
        }
    }

}
