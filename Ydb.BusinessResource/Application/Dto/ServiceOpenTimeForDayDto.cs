using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.BusinessResource.Application
{
    public class ServiceOpenTimeForDayDto
    {
        /// <summary>
        /// 标签
        /// </summary>
        public  string Tag { get; set; }

        /// <summary>
        /// 该事件段内的最大接单数量
        /// </summary>
        public  int MaxOrderForOpenTime { get; set; }
        public  bool Enabled { get; set; }

        public string timeStart;
        public string timeEnd;
    }
}
