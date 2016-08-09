using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class refundObj
    {
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

        IList<string> _resourcesUrls =new List<string>();
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

        string _action = "";
        /// <summary>
        /// 理赔请求的动作（"submit":提交理赔请求，"refund":店铺同意理赔要求，"reject":店铺拒绝理赔，"askPqy":店铺要求支付赔偿金，"agree":用户同意商户处理，"cancel":用户放弃理赔，"intervention":用户要求官方介入）
        /// </summary>
        /// <type>boolean</type>
        public string action
        {
            get
            {
                return _action;
            }
            set
            {
                _action = value;
            }
        }



    }
}
