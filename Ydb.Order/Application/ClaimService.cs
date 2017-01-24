using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ydb.Order.DomainModel;
using Ydb.Order.DomainModel.Repository;

namespace Ydb.Order.Application
{
    public class ClaimsService : IClaimsService
    {
        public IRepositoryClaims repoClaims;

        public ClaimsService(IRepositoryClaims repoClaims)
        {
            this.repoClaims = repoClaims;
        }

        public void Save(Claims c)
        {
            repoClaims.Add(c);
        }

        public void Update(Claims c)
        {
            c.LastUpdateTime = DateTime.Now;
            repoClaims.Update(c);
        }

        public Claims GetOneByOrder(ServiceOrder order)
        {
            return repoClaims.GetOneByOrder(order);
        }
    }
}
