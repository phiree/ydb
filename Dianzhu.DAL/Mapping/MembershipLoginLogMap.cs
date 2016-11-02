using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
namespace Dianzhu.DAL.Mapping
{
    public class MembershipLoginLogMap:ClassMap<MembershipLoginLog>
    {
        public MembershipLoginLogMap() { 
            Id(x=>x.Id);
            Map(x=>x.LogTime);
            Map(x => x.LogType).CustomType<enumLoginLogType>();
            Map(x => x.Memo);
            Map(x => x.MemberId);
           
        }
    }
}
