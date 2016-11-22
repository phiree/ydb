using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.BusinessResource.DomainModel
{
    public interface IPeriodValidator
    {
        IList<TimePeriod> Periods { set; }
        bool IsConflict(TimePeriod newPeriod);
        bool CanModify(TimePeriod oldPeriod, TimePeriod newPeriod,out string errMsg);
    }
}
