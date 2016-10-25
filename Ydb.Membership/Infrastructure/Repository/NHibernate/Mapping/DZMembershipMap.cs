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
    public class DZMembershipMap : ClassMap<DZMembership>
    {
        public DZMembershipMap()
        {
            Id(x => x.Id);
            Map(x => x.UserName).Unique();
            Map(x => x.Password);
            Map(x => x.PlainPassword);
            Map(x => x.TimeCreated);
            Map(x => x.LastLoginTime);
            Map(x => x.LoginTimes);
            Map(x => x.NickName);
            Map(x => x.Address);
            Map(x => x.Email);//.Unique();
            Map(x => x.Phone);//.Unique();
            Map(x => x.IsRegisterValidated);
            Map(x => x.RegisterValidateCode);
            Map(x => x.UserNameForOpenFire);
            Map(x => x.RecoveryCode);
            Map(x => x.AvatarUrl);
            Map(x => x.UserType).CustomType<UserType>();
            Map(x => x.LoginType).CustomType<LoginType>();
            Map(x => x.UserCity);


        }

    }
}
