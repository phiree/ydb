using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class common_Trait_Headers
    {
        /// <summary>
        /// 请求报文时间戳，为1970年0时0分0秒0毫秒至今的毫秒数
        /// </summary>
        /// <type>string</type>
        public string stamp_TIMES { get; set; }

        /// <summary>
        /// 1、前3位为固定编号009
        /// 2、流水号是每次接口请求的唯一标识ID
        /// 3、用于确定请求报文与响应报文的对应关系
        /// 4、流水号由请求发起方生成，并保证不重复
        /// 5、一个流水号只能使用一次（含请求/响应），重复请求无效
        /// 6、接收通讯请求的一方（接收方）应检查流水号是否被重复使用
        /// </summary>
        /// <type>string</type>
        public string serial_NUMBER { get; set; }

        /// <summary>
        /// 平台名称(IOS,Android,Web,system)
        /// </summary>
        /// <type>string</type>
        public string appName { get; set; }
    }
}
