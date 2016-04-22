using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.DAL.Finance
{
    public class DALServiceTypePoint:DALBase<Model.Finance.ServiceTypePoint>
    {
        public DALServiceTypePoint() : base() { }
        public DALServiceTypePoint(string fortest) : base(fortest) { }

        public virtual Model.Finance.ServiceTypePoint GetOne(Model.ServiceType serviceType)
        {
            var query = Session.QueryOver<Model.Finance.ServiceTypePoint>().Where(x => x.ServiceType == serviceType);
           return GetOneByQuery(query);
        }
    }
}
