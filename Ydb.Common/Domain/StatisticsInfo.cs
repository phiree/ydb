using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Common.Domain
{
    public class StatisticsInfo
    {
        public string XName { get; set; }
        public string YName { get; set; }

        public IDictionary<string,long> XYValue { get; set; }
    }
}
