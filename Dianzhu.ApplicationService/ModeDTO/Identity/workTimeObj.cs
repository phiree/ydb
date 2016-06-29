using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class workTimeObj
    {
        /// <summary>
        /// 工作时间的ID
        /// </summary>
        /// <type>string</type>
        public string id { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        /// <type>string</type>
        public string tag { get; set; }

        /// <summary>
        /// 开始时间（HH:mm）
        /// </summary>
        /// <type>string</type>
        public string startTime { get; set; }

        /// <summary>
        /// 结束时间（HH:mm）
        /// </summary>
        /// <type>string</type>
        public string endTime { get; set; }

        /// <summary>
        /// 星期数（1~7 周一~周日）
        /// </summary>
        /// <type>string</type>
        public string week { get; set; }
        //public DayOfWeek week1 { get; set; }

        /// <summary>
        /// 是否开启（true Or false）
        /// </summary>
        /// <type>boolean</type>
        public bool bOpen { get; set; }

        /// <summary>
        /// 改时间段的最大接单量
        /// </summary>
        /// <type>string</type>
        public string maxCountOrder { get; set; }
    }
}
