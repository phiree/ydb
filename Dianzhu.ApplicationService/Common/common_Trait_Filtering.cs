using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class common_Trait_Filtering
    {
        /// <summary>
        /// 每页的长度（>0）
        /// </summary>
        /// <type>string</type>
        public string pageSize { get; set; }

        /// <summary>
        /// 页数（>=1）
        /// </summary>
        /// <type>string</type>
        public string pageNum { get; set; }

        /// <summary>
        /// 距离第一项的偏移值
        /// </summary>
        /// <type>string</type>
        public string offset { get; set; }

        /// <summary>
        /// 是否升序，默认jiangxu（true Or false）
        /// </summary>
        /// <type>boolean</type>
        public bool ascending { get; set; }

        /// <summary>
        /// 排序的字段依据
        /// </summary>
        /// <type>string</type>
        public string sortby { get; set; }

        /// <summary>
        /// 设置起始的ID（包含）
        /// </summary>
        /// <type>string</type>
        public string baseID { get; set; }

        /// <summary>
        /// 临时用于查询的用户名
        /// </summary>
        /// <type>string</type>
        public string userID { get; set; }
    }
}
