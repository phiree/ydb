using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class common_Trait_LocationFiltering
    {
        /// <summary>
        /// 经度
        /// </summary>
        /// <type>string</type>
        public string longitude { get; set; }

        /// <summary>
        /// 维度
        /// </summary>
        /// <type>string</type>
        public string latitude { get; set; }

        /// <summary>
        /// 城市代码
        /// </summary>
        /// <type>string</type>
        public string code { get; set; }
    }
}
