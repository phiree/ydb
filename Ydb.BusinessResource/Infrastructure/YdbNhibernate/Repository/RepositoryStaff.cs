using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ydb.Common.Specification;

using Ydb.Common.Repository;
using Ydb.BusinessResource.DomainModel;

namespace Ydb.BusinessResource.Infrastructure.YdbNHibernate.Repository
{
    public class RepositoryStaff : NHRepositoryBase<Staff, Guid>, IRepositoryStaff
    {
       
        
        public int GetEnableSum(Business business)
        {
            return (int) GetRowCount(x => x.Belongto.Id == business.Id && x.Enable);
            
        }
        

        public IList<Staff> GetAllListByBusiness(Business business)
        {
            return Find(x => x.Belongto == business);
        }
        
    }
}
