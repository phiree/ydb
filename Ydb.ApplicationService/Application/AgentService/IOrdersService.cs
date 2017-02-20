using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.ApplicationService.Application.AgentService
{
   public interface IOrdersService
    {
        long GetOrdersCountByArea(IList<string> areaIdList, bool isSharea);
    }
}
