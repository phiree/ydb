using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class assignObj
    {
        /// <summary>
        /// 指派员工的ID
        /// </summary>
        /// <type>string</type>
        public string staffID { get; set; }

        /// <summary>
        /// 指派的订单ID
        /// </summary>
        /// <type>string</type>
        public string orderID { get; set; }

        /// <summary>
        /// 生成时间（yyyyMMddHHmmss）
        /// </summary>
        /// <type>string</type>
        public string createTime { get; set; }
    }
}
