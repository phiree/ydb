using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ydb.InstantMessage.DomainModel.Reception;
using NHibernate;
using Ydb.InstantMessage.Infrastructure.Repository;
using Ydb.Common.Repository;
namespace Ydb.InstantMessage.Infrastructure.Repository.NHibernate
{
    public class RepositoryReception :NHRepositoryBase<ReceptionStatus,Guid>,  IRepositoryReception
    {

        
        public IList<ReceptionStatus> FindByCustomerId(string customerId)
        {
            return  Find(x => x.CustomerId == customerId);
        }
        public IList<ReceptionStatus> FindByCustomerServiceId(string csId)
        {
            return  Find(x => x.CustomerServiceId == csId);
        }
        public IList<ReceptionStatus> FindByDiandian(string diandianId,int amount)
        {
            return  Find(x => x.CustomerServiceId == diandianId).Take(amount).ToList();
        }
        public IList<ReceptionStatus> UpdateCustomerAreaId(string customerId, string areaId)
        {
           var list=  Find(x => x.CustomerId == customerId);
            foreach (var i in list)
            {
                i.AreaId = areaId;
            }
            return list;
        }
 
    }
}
