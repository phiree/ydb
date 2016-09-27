using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class billModelObj
    {
        string _id = "";
        /// <summary>
        /// 账单id
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

        string _createTime = "";
        /// <summary>
        /// 账单id
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

        string _serialNo = "";
        /// <summary>
        /// 账单id
        /// </summary>
        /// <type>string</type>
        public string serialNo
        {
            get
            {
                return _serialNo;
            }
            set
            {
                _serialNo = value;
            }
        }

        string _type = "";
        /// <summary>
        /// 账单id
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

        string _amount = "";
        /// <summary>
        /// 账单id
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

        string _discount = "";
        /// <summary>
        /// 账单id
        /// </summary>
        /// <type>string</type>
        public string discount
        {
            get
            {
                return _discount;
            }
            set
            {
                _discount = value;
            }
        }

        billOrderInfoObj _billOrderInfo =new billOrderInfoObj();
        /// <summary>
        /// 账单id
        /// </summary>
        /// <type>string</type>
        public billOrderInfoObj billOrderInfo
        {
            get
            {
                return _billOrderInfo;
            }
            set
            {
                _billOrderInfo = value;
            }
        }
    }
}
