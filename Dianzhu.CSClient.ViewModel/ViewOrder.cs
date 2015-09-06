using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.CSClient.ViewModel
{
    /// <summary>
    /// 订单
    /// </summary>
    public class ViewOrder
    {
        public string ServiceName { get; set; }
        public string ServiceBusinessName { get; set; }
        public string ServiceDescription { get; set; }
        public string ServiceUnitPrice { get; set; }
        public string ServiceUrl { get; set; }

        public string ServiceTime { get; set; }
        public string ServiceTargetAddress { get; set; }
        public string ServiceUnitAmount { get; set; }
        public string ServiceTotalPrice { get; set; }
    }
}
