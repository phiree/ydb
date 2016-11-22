﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
namespace Dianzhu.DAL.Mapping
{
    public class AreaMap:ClassMap<Area>
    {
        public AreaMap() { 
            Id(x=>x.Id);
            Map(x=>x.Name);
            Map(x => x.Code);
            Map(x => x.SeoName);
            Map(x => x.AreaOrder);
            Map(x => x.MetaDescription);
            Map(x => x.BaiduName);
            Map(x => x.BaiduCode);
        }
    }
}
