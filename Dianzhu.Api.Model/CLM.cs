using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dianzhu.Config;
namespace Dianzhu.Api.Model
{
    public class ReqDataCLM001001
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string orderID { get; set; }
        public string target { get; set; }
        public string context { get; set; }
        public string resourcesUrl { get; set; }
    }
}
