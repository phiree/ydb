using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.IDAL.Finance;
using Dianzhu.Model;
using Dianzhu.Model.Finance;

namespace Dianzhu.DAL.Finance
{
    public class DALServiceTypePoint :NHRepositoryBase<ServiceTypePoint,Guid>, IDALServiceTypePoint// DALBase<Model.Finance.ServiceTypePoint>
    {
        public ServiceTypePoint GetOneByServiceType(ServiceType serviceType)
        {
            var result = FindOne(x => x.ServiceType.Id == serviceType.Id);
            return result;
        }
        public void SaveList(IList<ServiceTypePoint> list)
        {
            foreach (ServiceTypePoint p in list)
            {
                Add(p);
            }
        }
        public IList<Dianzhu.Model.Finance.ServiceTypePoint> GetAll()
        {
            return Find(x => true);
        }
    }
}
