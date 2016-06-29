using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class common_Trait_OrderFiltering
    {
        /// <summary>
        /// 订单类别("done":[完成的订单]，"pending":[未完成的订单]，"all":[所有的订单])
        /// </summary>
        /// <type>string</type>
        public Model.Enums.enum_OrderSearchType statusSort { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        /// <type>string</type>
        public string status { get; set; }
    }
}
