using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class refundStatusObj
    {
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

        string _amount = "";
        /// <summary>
        /// 金额
        /// </summary>
        /// <type>string</type>
        public string amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
            }
        }

        IList<string> _resourcesUrls = new List<string>();
        /// <summary>
        /// 图片资源Url数组
        /// </summary>
        /// <type>array[string]</type>
        public IList<string> resourcesUrls
        {
            get
            {
                return _resourcesUrls;
            }
            set
            {
                _resourcesUrls = value;
            }
        }

        string _createTime = "";
        /// <summary>
        /// 生成的时间（yyyyMMddHHmmss）
        /// </summary>
        /// <type>string</type>
        public string createTime
        {
            get
            {
                return _createTime;
            }
            set
            {
                _createTime = value;
            }
        }

        string _orderStatus = "";
        /// <summary>
        /// 对应的订单状态
        /// </summary>
        /// <type>string</type>
        public string orderStatus
        {
            get
            {
                return _orderStatus;
            }
            set
            {
                _orderStatus = value;
            }
        }

        string _target = "";
        /// <summary>
        /// 提交本次理赔的目标（"customerService":客服，"store":店铺，"user":用户，"system":系统）
        /// </summary>
        /// <type>string</type>
        public string target
        {
            get
            {
                return _target;
            }
            set
            {
                _target = value;
            }
        }
    }
}
