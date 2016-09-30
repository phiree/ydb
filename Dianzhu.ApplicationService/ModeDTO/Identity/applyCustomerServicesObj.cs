using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class applyCustomerServicesObj
    {
        customerServicesObj _customerServicesObj = new customerServicesObj();
        /// <summary>
        /// 客服模型
        /// </summary>
        /// <type>customerServicesObj</type>
        public customerServicesObj customerServicesObj
        {
            get
            {
                return _customerServicesObj;
            }
            set
            {
                _customerServicesObj = value;
            }
        }

        string _draftOrderID = "";
        /// <summary>
        /// 草稿订单 ID
        /// </summary>
        /// <type>string</type>
        public string draftOrderID
        {
            get
            {
                return _draftOrderID;
            }
            set
            {
                _draftOrderID = value;
            }
        }

    }
}
