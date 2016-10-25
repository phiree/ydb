using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Finance.Application
{
    public interface IServiceTypePointService
    {
        void Add(string serviceType, decimal point);
        decimal GetPoint(string serviceTypeId);
        IList<ServiceTypePointDto> GetAll();
    }
}
