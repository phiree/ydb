using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Finance.DomainModel
{
    public class SpecExitedSalePoint : Helpers.Specification.SpecificationBase<SalePoint>
    {
        readonly Guid serviceTypeId;
        public SpecExitedSalePoint(Guid serviceTypeId)
        {
            this.serviceTypeId = serviceTypeId;
        }
        public override Expression<Func<SalePoint, bool>> SpecExpression
        {
            get
            {
                return salePoint => this.serviceTypeId == salePoint.TypeId;
            }
        }
    }
}
