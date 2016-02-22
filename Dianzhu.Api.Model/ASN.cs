using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.Config;
namespace Dianzhu.Api.Model
{
    public class ReqDataASN001005
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string orderId { get; set; }
        public string staffId { get; set; }
    }

    public class RespDataASN001005
    {
        public string assigned { get; set; }
    }

    public class ReqDataASN001006
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string businessId { get; set; }
        public string staffId { get; set; }
        public string pageNum { get; set; }
        public string pageSize { get; set; }
    }

    public class RespDataASN001006
    {
        public IList<RespDataASN_arrayData> arrayData { get; set; }
    }

    public class RespDataASN_arrayData
    {
        public string orderId { get; set; }
        public string assigned { get; set; }
    }

    public class ReqDataASN001007
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public bool assign { get; set; }
        public string staffId { get; set; }
        public IList<string> arrayOrderId { get; set; }
    }

    public class ReqDataASN001008
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public bool assign { get; set; }
        public string orderId { get; set; }
        public IList<string> arrayStaffId { get; set; }
    }
}
