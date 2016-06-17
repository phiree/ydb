using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class common_Trait_ServiceFiltering
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        /// <type>string</type>
        public string name { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        /// <type>string</type>
        public string type { get; set; }

        /// <summary>
        /// 服务简介
        /// </summary>
        /// <type>string</type>
        public string introduce { get; set; }

        /// <summary>
        ///起步价
        /// </summary>
        /// <type>boolean</type>
        public string startAt { get; set; }
    }
}
