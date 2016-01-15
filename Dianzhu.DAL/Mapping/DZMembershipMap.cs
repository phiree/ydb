﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
namespace Dianzhu.DAL.Mapping
{
    
    public class DZMembershipMap:ClassMap<DZMembership>
    {
        public DZMembershipMap()
        {
            Id(x => x.Id);
            Map(x => x.UserName).Unique();
            Map(x => x.Password);
            Map(x => x.PlainPassword);
            Map(x => x.TimeCreated);
            Map(x => x.LastLoginTime);
            Map(x=>x.NickName);
            Map(x => x.Address);
            Map(x => x.Email).Unique();
            Map(x => x.Phone).Unique();
            Map(x => x.IsRegisterValidated);
            Map(x => x.RegisterValidateCode);
            Map(x => x.UserNameForOpenFire);
            Map(x => x.RecoveryCode);
            Map(x => x.AvatarUrl);
            Map(x => x.UserType);
            
        }
               
    }
    public class BusinessUserMap : SubclassMap<BusinessUser>
    {
        public BusinessUserMap()
        {
            References<Business>(x => x.BelongTo);
        }
    }
    public class CustomerServiceMap : SubclassMap<CustomerService>
    {
        public CustomerServiceMap()
        {
            Map(x => x.CSId);
         }
    }

    public class DZMembershipWeChatMap : SubclassMap<DZMembershipWeChat>
    {
        public DZMembershipWeChatMap()
        {
            Map(x => x.AccessToken);
            Map(x => x.ExpiresIn);
            Map(x => x.RefreshToken);
            Map(x => x.OpenId);
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
