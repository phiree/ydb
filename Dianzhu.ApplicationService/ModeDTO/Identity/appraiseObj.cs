using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class appraiseObj
    {

        /// <summary>
        /// 评价的目标
        /// </summary>
        /// <type>string</type>
        public string target { get; set; }

        /// <summary>
        /// 评分值（0~5的整数）
        /// </summary>
        /// <type>string</type>
        public string value { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        /// <type>string</type>
        public string content { get; set; }
    }
}
