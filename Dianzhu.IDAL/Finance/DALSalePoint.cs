using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.IDAL.Finance
{
    public interface IDALServiceTypePoint
    {
         Model.Finance.ServiceTypePoint GetOne(Model.ServiceType serviceType);
        
    }
}
