using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class common_Trait_ComplainFiltering
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        /// <type>string</type>
        public string orderID { get; set; }

        /// <summary>
        /// 店铺ID
        /// </summary>
        /// <type>string</type>
        public string storeID { get; set; }

        /// <summary>
        /// 客服ID
        /// </summary>
        /// <type>string</type>
        public string customerServiceID { get; set; }
    }
}
