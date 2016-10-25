using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Finance.DomainModel;
using NHibernate;
using Ydb.Common.Repository;

namespace Ydb.Finance.Infrastructure.Repository
{
   internal class RepositoryServiceTypePoint : NHRepositoryBase<ServiceTypePoint, Guid>, IRepositoryServiceTypePoint
    {
        public RepositoryServiceTypePoint(ISession session) : base(session)
        {
        }

        public ServiceTypePoint GetOneByServiceType(string serviceTypeId)
        {
            var result = FindOne(x => x.ServiceTypeId == serviceTypeId);
            return result;
        }

        public IList<ServiceTypePoint> GetAll()
        {
            return Find(x => true);
        }


        public void SaveList(IList<ServiceTypePoint> list)
        {
            foreach (ServiceTypePoint item in list)
            {
                SaveOrUpdate(item);
            }

        }
    }
}
