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
            Component<AgentMemberInfo>(x => x.MemberInfo,m=> { });
           
        }
    }
}
