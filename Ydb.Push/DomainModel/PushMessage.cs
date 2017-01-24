using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ydb.Push
{
    public class PushMessage
    {
        public string DisplayContent { get; set; }
        public string OrderId { get; set; }
        public string OrderSerialNo { get; set; }
        public override string ToString()
        {
            return OrderSerialNo + "," + OrderId;
        }
    }
}
