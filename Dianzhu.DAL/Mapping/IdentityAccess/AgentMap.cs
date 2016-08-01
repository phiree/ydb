using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dianzhu.Model;
namespace Dianzhu.DAL.Mapping
{
    public class AgentMap:SubclassMap<Agent>
    {
        public AgentMap() { 
            
            References<Area>(x=>x.Area);
            Map(x => x.SharePoint);
            //Component<AgentMemberInfo>(x => x.MemberInfo);
            References<AgentMemberInfo>(x => x.MemberInfo);
        }
    }

    public class AgentMemberInfoMap : ClassMap<AgentMemberInfo>
    {
        public AgentMemberInfoMap()
        {
            Id(x => x.Id).CustomType<Guid>();
            Map(x => x.RealName);
            Map(x => x.Sex);
            Map(x => x.PersonalID);
            Map(x => x.Phone);
            Map(x => x.AvatarUrl);
        }
    }
}
