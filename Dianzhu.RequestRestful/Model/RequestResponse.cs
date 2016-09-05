using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.RequestRestful
{
    public class RequestResponse
    {
        /// <summary>
        /// 是否请求成功
        /// </summary>
        public bool code { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 结果数据
        /// </summary>
        public string data { get; set; }
    }
}
