using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class remindObj
    {
        string _id = "";
        /// <summary>
        /// 提醒ID
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

        string _title = "";
        /// <summary>
        /// 标题
        /// </summary>
        /// <type>string</type>
        public string title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        string _remindTime = "";
        /// <summary>
        /// 提醒时间（yyyyMMddHHmmss）
        /// </summary>
        /// <type>string</type>
        public string remindTime
        {
            get
            {
                return _remindTime;
            }
            set
            {
                _remindTime = value;
            }
        }

        string _content = "";
        /// <summary>
        /// 描述
        /// </summary>
        /// <type>string</type>
        public string content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }

        bool _bOpen =true;
        /// <summary>
        /// 是否开启（true Or false）
        /// </summary>
        /// <type>boolean</type>
        public bool bOpen
        {
            get
            {
                return _bOpen;
            }
            set
            {
                _bOpen = value;
            }
        }

        string _orderID = "";
        /// <summary>
        /// 关联的订单ID
        /// </summary>
        /// <type>string</type>
        public string orderID
        {
            get
            {
                return _orderID;
            }
            set
            {
                _orderID = value;
            }
        }
    }
}
