using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model.Finance;
using Dianzhu.Model;
namespace Dianzhu.IDAL.Finance
{
    public interface IDALServiceTypePoint:IRepository<Dianzhu.Model.Finance.ServiceTypePoint,Guid>
    {

          ServiceTypePoint GetOneByServiceType(ServiceType serviceType);


          IList<Dianzhu.Model.Finance.ServiceTypePoint> GetAll();
    }
}
