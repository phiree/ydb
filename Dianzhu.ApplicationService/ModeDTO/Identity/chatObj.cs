using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class chatObj
    {
        string _id = "";
        /// <summary>
        /// 消息ID
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

        string _to = "";
        /// <summary>
        /// 接收者ID
        /// </summary>
        /// <type>string</type>
        public string to
        {
            get
            {
                return _to;
            }
            set
            {
                _to = value;
            }
        }

        string _from = "";
        /// <summary>
        /// 发送者ID
        /// </summary>
        /// <type>string</type>
        public string from
        {
            get
            {
                return _from;
            }
            set
            {
                _from = value;
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

        string _body = "";
        /// <summary>
        /// 消息文本
        /// </summary>
        /// <type>string</type>
        public string body
        {
            get
            {
                return _body;
            }
            set
            {
                _body = value;
            }
        }

        string _type = "";
        /// <summary>
        /// 类型("chat":[文本]，"voice":[语音]，"image":[图片]，"video":[视频]，"url":[网址]，"pushOrder":[推送的订单])
        /// </summary>
        /// <type>string</type>
        public string type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        string _sendTime = "";
        /// <summary>
        /// 发送时间(yyyyMMddHHmmss)
        /// </summary>
        /// <type>string</type>
        public string sendTime
        {
            get
            {
                return _sendTime;
            }
            set
            {
                _sendTime = value;
            }
        }

    }
}
