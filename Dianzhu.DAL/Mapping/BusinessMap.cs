﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
 
namespace Dianzhu.DAL.Mapping
{
    public class Business_AbsMap : ClassMap<Business_Abs>
    {
        public Business_AbsMap()
        {
            Id(x => x.Id).CustomType<Guid>();
            Map(x => x.Name);
            Map(x => x.Contact);
            Map(x => x.Description);
            Map(x => x.Email);
            Map(x => x.Phone);
            Map(x => x.WebSite);
            Map(x => x.OwnerId);
            Map(x => x.Enabled);
            Map(x => x.CreatedTime);
        }
    }
    public class BusinessMap : SubclassMap<Business>
    {
        public BusinessMap()
        {
            Map(x => x.Address);
            Map(x => x.WorkingYears);
            Map(x=>x.Description);
            Map(x => x.Latitude);
            Map(x => x.Longitude);
            Map(x => x.IsApplyApproved);
            Map(x => x.ApplyRejectMessage);
            References<Area>(x => x.AreaBelongTo).ForeignKey("none");
           
            Map(x => x.DateApply);
            Map(x => x.DateApproved);
           
            HasMany<BusinessImage>(x => x.BusinessImages).Cascade.AllDeleteOrphan();
            Map(x => x.ChargePersonIdCardNo);
            Map(x => x.StaffAmount);
            Map(x => x.ChargePersonIdCardType).CustomType<Model.Enums.enum_IDCardType>();
            Map(x => x.PromoteScope);
            
            Map(x => x.RawAddressFromMapAPI);
        }
    }
    public class BusinessImageMap : ClassMap<BusinessImage>
    {
        public BusinessImageMap()
        {
            Id(x => x.Id);
            Map(x => x.Description);
            Map(x => x.ImageName);
            Map(x => x.ImageType).CustomType<Dianzhu.Model.Enums.enum_ImageType>();
            Map(x => x.OrderNumber);
            Map(x => x.Size);
            Map(x => x.UploadTime);
            Map(x => x.IsCurrent);
            
        }
    }

}
