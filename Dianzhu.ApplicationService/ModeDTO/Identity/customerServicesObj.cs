using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class customerServicesObj
    {
        string _id = null;
        /// <summary>
        /// 客服ID
        /// </summary>
        /// <type>string</type>
        public string id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        string _alias = "";
        /// <summary>
        /// 昵称
        /// </summary>
        /// <type>string</type>
        public string alias
        {
            get
            {
                return _alias;
            }
            set
            {
                _alias = value;
            }
        }

        string _imgUrl = "";
        /// <summary>
        /// 头像
        /// </summary>
        /// <type>string</type>
        public string imgUrl
        {
            get
            {
                return _imgUrl;
            }
            set
            {
                _imgUrl = value;
            }
        }

        /// <summary>
        /// 草稿订单 ID
        /// </summary>
        /// <type>string</type>
        //public string draftOrderID { get; set; }

    }
}
