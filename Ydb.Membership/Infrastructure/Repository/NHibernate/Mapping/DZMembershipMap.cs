using FluentNHibernate.Mapping;
using Ydb.Membership.DomainModel;
using Ydb.Membership.DomainModel.Enums;

namespace Ydb.Membership.Infrastructure.Repository.NHibernate.Mapping
{
    public class DZMembershipMap : ClassMap<DZMembership>
    {
        public DZMembershipMap()
        {
            Id(x => x.Id).GeneratedBy.Custom<IdGeneratedOrAssigned>();
            Map(x => x.UserName).Unique();
            Map(x => x.Password);
            Map(x => x.PlainPassword);
            Map(x => x.TimeCreated);
            Map(x => x.LastLoginTime);
            Map(x => x.LoginTimes);
            Map(x => x.NickName);
            Map(x => x.Address);
            Map(x => x.QQNumber);

            Map(x => x.WXNumber);
            Map(x => x.WBNumber);
            Map(x => x.LockTime);
            Map(x => x.IsLocked);
            Map(x => x.LockReason);

            Map(x => x.Email); //.Unique();
            Map(x => x.Phone); //.Unique();
            Map(x => x.RealName);
            Map(x => x.Sex);
            Map(x => x.Birthday);
            Map(x => x.PersonalID);
            Map(x => x.AreaId);
            //References<Area>(x => x.Area).ForeignKey("none");
            Map(x => x.IsRegisterValidated);
            Map(x => x.RegisterValidateCode);
            Map(x => x.UserNameForOpenFire);
            Map(x => x.RecoveryCode);
            Map(x => x.AvatarUrl);
            Map(x => x.UserType).CustomType<UserType>();
            Map(x => x.LoginType).CustomType<LoginType>();
            Map(x => x.UserCity);
            Map(x => x.Longitude);
            Map(x => x.Latitude);
        }
    }
}