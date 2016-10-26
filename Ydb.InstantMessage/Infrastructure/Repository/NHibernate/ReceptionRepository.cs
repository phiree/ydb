using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ydb.InstantMessage.DomainModel.Reception;
using NHibernate;
using Ydb.Common.Repository;
namespace Ydb.InstantMessage.Infrastructure.Repository.NHibernate
{
    public class RepositoryReception :NHRepositoryBase<ReceptionStatus,Guid>,  IRepositoryReception
    {

        
        public IList<ReceptionStatus> FindByCustomerId(string customerServiceId)
        {
            return  Find(x => x.CustomerId == customerServiceId);
        }
        public IList<ReceptionStatus> FindByCustomerServiceId(string csId)
        {
            return  Find(x => x.CustomerServiceId == csId);
        }
        public IList<ReceptionStatus> FindByDiandian(string diandianId,int amount)
        {
            return  Find(x => x.CustomerServiceId == diandianId).Take(amount).ToList();
        }
 
    }
}
