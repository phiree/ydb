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
    public class DZMembershipWeChatMap : SubclassMap<DZMembershipWeChat>
    {
        public DZMembershipWeChatMap()
        {
            Map(x => x.AccessToken);
            Map(x => x.ExpiresIn);
            Map(x => x.RefreshToken);
            Map(x => x.WeChatOpenId);
            Map(x => x.Scope);
            Map(x => x.Unionid);
            Map(x => x.NickName);
            Map(x => x.Sex);
            Map(x => x.Province);
            Map(x => x.City);
            Map(x => x.Country);
            Map(x => x.Headimgurl);
        }
    }

}
