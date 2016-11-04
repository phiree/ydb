using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Finance.DomainModel;
using FluentNHibernate.Mapping;

namespace Ydb.Finance.Infrastructure.Repository.NHibernate.Mapping
{
   public class ServiceTypePointMap : ClassMap<ServiceTypePoint>
    {
        public ServiceTypePointMap()
        {
            Id(x => x.Id);//.GeneratedBy.Assigned();加上这个表示必须先生成id,再传给数据库
            Map(x => x.ServiceTypeId);
            Map(x => x.Point);
        }
    }
}
