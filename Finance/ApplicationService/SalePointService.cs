using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers.Repository;
namespace Finance.ApplicationService
{
    public class SalePointService
    {
        IRepository<DomainModel.SalePoint> repSalPoint;
        public SalePointService(IRepository<DomainModel.SalePoint> repSalPoint)
        {
            this.repSalPoint = repSalPoint;
        }
        public void SetPoint(Guid serviceTypeId, decimal point)
        {
            //查询已经存在的条目
            var salePoint = repSalPoint.FindOne(new DomainModel.SpecExitedSalePoint(serviceTypeId));
            if (salePoint == null)
            {
                salePoint = new DomainModel.SalePoint();

            }
            salePoint.SetPoint(serviceTypeId, point);
            repSalPoint.Add(salePoint);
        }
    }
}
