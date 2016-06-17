using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class common_Trait_AssignFiltering
    {
        /// <summary>
        /// 指派的员工ID
        /// </summary>
        /// <type>string</type>
        public string staffID { get; set; }

        /// <summary>
        /// 指派的订单ID
        /// </summary>
        /// <type>string</type>
        public string orderID { get; set; }

        /// <summary>
        /// 店铺ID
        /// </summary>
        /// <type>string</type>
        public string storeID { get; set; }
    }
}
