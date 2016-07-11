using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class customerServicesObj
    {

        /// <summary>
        /// 客服ID
        /// </summary>
        /// <type>string</type>
        public string id { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        /// <type>string</type>
        public string alias { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        /// <type>string</type>
        public string imgUrl { get; set; }

        /// <summary>
        /// 草稿订单 ID
        /// </summary>
        /// <type>string</type>
        //public string draftOrderID { get; set; }

    }
}
