using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.BusinessResource.DomainModel.Service
{
    public class ServiceTypeAdapter : IServiceTypeAdapter
    {
        public IList<ServiceType> AdaptFromDataTable(DataTable dataTableFromExcel)
        {
            throw new NotImplementedException();
        }
    }
}
