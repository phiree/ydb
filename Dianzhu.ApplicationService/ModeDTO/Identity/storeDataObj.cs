using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class storeDataObj
    {
        string _storeID = "";
        /// <summary>
        /// 店铺ID
        /// </summary>
        /// <type>string</type>
        public string storeID
        {
            get
            {
                return _storeID;
            }
            set
            {
                _storeID = value;
            }
        }

        int _handleCount = 0;
        /// <summary>
        /// 正在处理的订单数
        /// </summary>
        /// <type>int</type>
        public int handleCount
        {
            get
            {
                return _handleCount;
            }
            set
            {
                _handleCount = value;
            }
        }

        int _finishCount = 0;
        /// <summary>
        /// 完成的订单数
        /// </summary>
        /// <type>string</type>
        public int finishCount
        {
            get
            {
                return _finishCount;
            }
            set
            {
                _finishCount = value;
            }
        }

        int _allCount = 0;
        /// <summary>
        /// 总单数
        /// </summary>
        /// <type>string</type>
        public int allCount
        {
            get
            {
                return _allCount;
            }
            set
            {
                _allCount = value;
            }
        }

        IList<string> _assignOrderIDs = new List<string>();
        /// <summary>
        /// 参与的订单ID
        /// </summary>
        public IList<string> assignOrderIDs
        {
            get
            {
                return _assignOrderIDs;
            }
            set
            {
                _assignOrderIDs = value;
            }
        }

    }
}
