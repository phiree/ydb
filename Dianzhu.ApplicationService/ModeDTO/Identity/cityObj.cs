using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class cityObj
    {
        /// <summary>
        /// 城市名称
        /// </summary>
        /// <type>string</type>
        public string name { get; set; }

        /// <summary>
        /// 城市关键字母（如“A”）
        /// </summary>
        /// <type>string</type>
        public string key { get; set; }

        /// <summary>
        /// 城市代码
        /// </summary>
        /// <type>string</type>
        public string code { get; set; }
    }
}
