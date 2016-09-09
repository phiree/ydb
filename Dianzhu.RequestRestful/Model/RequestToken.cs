using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.RequestRestful
{
   public class RequestToken
    {
        /// <summary>
        /// token用户验证token,header参数
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// userEndpoint用户 或 商户 或 客服 或 员工 的路由路径
        /// </summary>
        public string userEndpoint { get; set; }
        
    }
}
