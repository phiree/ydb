using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class common_Trait_WorkTimeFiltering
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        /// <type>string</type>
        public string startTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        /// <type>string</type>
        public string endTime { get; set; }

        /// <summary>
        /// 星期
        /// </summary>
        /// <type>string</type>
        public string week { get; set; }
    }
}
