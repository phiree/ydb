using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.Config;
namespace Dianzhu.Api.Model
{
    public class ReqDataASN001007
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public bool assign { get; set; }
        public string staffId { get; set; }
        public IList<string> arrayOrderId { get; set; }
    }

    public class ReqDataASN002007
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public bool assign { get; set; }
        public string orderId { get; set; }
        public IList<string> arrayStaffId { get; set; }
    }
}
