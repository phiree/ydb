using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class common_Trait_Simple_Headers
    {
        /// <summary>
        /// 请求报文时间戳，为1970年0时0分0秒0毫秒至今的毫秒数 unix时间戳
        /// </summary>
        /// <type>string</type>
        public string stamp_TIMES { get; set; }

        /// <summary>
        /// 平台名称
        /// </summary>
        /// <type>string</type>
        public string appName { get; set; }
    }
}
