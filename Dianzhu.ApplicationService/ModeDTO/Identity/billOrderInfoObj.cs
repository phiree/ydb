using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class billOrderInfoObj
    {
        string _orderId = "";
        /// <summary>
        /// 订单Id
        /// </summary>
        /// <type>string</type>
        public string orderId
        {
            get
            {
                return _orderId;
            }
            set
            {
                _orderId = value;
            }
        }

        string _orderAmount = "";
        /// <summary>
        /// 订单金额
        /// </summary>
        /// <type>string</type>
        public string orderAmount
        {
            get
            {
                return _orderAmount;
            }
            set
            {
                _orderAmount = value;
            }
        }

        string _customerName = "";
        /// <summary>
        /// 客户名称
        /// </summary>
        /// <type>string</type>
        public string customerName
        {
            get
            {
                return _customerName;
            }
            set
            {
                _customerName = value;
            }
        }

        string _customerImgUrl = "";
        /// <summary>
        /// 客户头像Url
        /// </summary>
        /// <type>string</type>
        public string customerImgUrl
        {
            get
            {
                return _customerImgUrl;
            }
            set
            {
                _customerImgUrl = value;
            }
        }

        string _serviceType = "";
        /// <summary>
        /// 服务类型
        /// </summary>
        /// <type>string</type>
        public string serviceType
        {
            get
            {
                return _serviceType;
            }
            set
            {
                _serviceType = value;
            }
        }
    }
}
