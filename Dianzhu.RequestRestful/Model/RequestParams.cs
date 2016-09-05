using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dianzhu.RequestRestful
{
    public class RequestParams
    {
        /// <summary>
        /// appName客户端标识,header参数
        /// </summary>
        [Required]
        public string appName { get; set; }

        ///// <summary>
        ///// appName客户端标识,header参数
        ///// </summary>
        //[Required]
        //public string appKey { get; set; }

        /// <summary>
        /// stamp_TIMES请求时间戳,header参数
        /// </summary>
        [Required]
        public string stamp_TIMES { get; set; }

        /// <summary>
        /// token用户验证token,header参数
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// sign请求签名,header参数
        /// </summary>
        [Required]
        public string sign { get; set; }

        /// <summary>
        /// url请求路径
        /// </summary>
        [Required]
        public string url { get; set; }

        /// <summary>
        /// method请求方式
        /// </summary>
        [Required]
        public string method { get; set; }

        /// <summary>
        /// content请求body
        /// </summary>
        public string content { get; set; }
    }
}
