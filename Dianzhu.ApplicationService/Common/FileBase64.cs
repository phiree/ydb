using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class FileBase64
    {
        /// <summary>
        /// Base64字符串
        /// </summary>
        /// <type>string</type>
        public string data { get; set; }

        /// <summary>
        /// 原文件明
        /// </summary>
        public string originalName { get; set; }
    }
}
