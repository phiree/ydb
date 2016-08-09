using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class assignObj
    {
        string _staffID = "";
        /// <summary>
        /// 指派员工的ID
        /// </summary>
        /// <type>string</type>
        public string staffID
        {
            get
            {
                return _staffID;
            }
            set
            {
                _staffID = value;
            }
        }

        string _orderID = "";
        /// <summary>
        /// 指派的订单ID
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

        string _createTime = "";
        /// <summary>
        /// 生成时间（yyyyMMddHHmmss）
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
    }
}
