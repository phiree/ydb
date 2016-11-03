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
    public class DZMembershipSinaWeiboMap : SubclassMap<DZMembershipSinaWeibo>
    {
        public DZMembershipSinaWeiboMap()
        {
            Map(x => x.AccessToken);
            Map(x => x.ExpiresId);
            Map(x => x.RefreshToken);
            Map(x => x.Scope);
            Map(x => x.CreateAt);
            Map(x => x.UId);
            Map(x => x.UIdstr);
            Map(x => x.ScreenName);
            Map(x => x.Name);
            Map(x => x.Province);
            Map(x => x.City);
            Map(x => x.Location);
            Map(x => x.Description);
            Map(x => x.Url);
            Map(x => x.ProfileImageUrl);
            Map(x => x.ProfileUrl);
            Map(x => x.Domain);
            Map(x => x.WeiHao);
            Map(x => x.Gender);
            Map(x => x.FollowersCount);
            Map(x => x.FriendsCount);
            Map(x => x.StatusesCount);
            Map(x => x.FavouritesCount);
            Map(x => x.CreatedAt);
            Map(x => x.Following);
            Map(x => x.AllowAllActMsg);
            Map(x => x.GeoEnabled);
            Map(x => x.Verified);
            Map(x => x.VerifiedType);
            Map(x => x.Remark);
            Map(x => x.AllowAllComment);
            Map(x => x.AvatarLarge);
            Map(x => x.AvatarHd);
            Map(x => x.VerifiedReason);
            Map(x => x.FollowMe);
            Map(x => x.OnlineStatus);
            Map(x => x.BiFollowersCount);
            Map(x => x.Lang);
        }
    }

}
