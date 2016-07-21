using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class snapshortsObj
    {
        string _date = "";
        /// <summary>
        /// 快照的日期（yyyyMMdd）
        /// </summary>
        /// <type>string</type>
        public string date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
            }
        }

        string _maxCountOrder = "";
        /// <summary>
        /// 最大订单数
        /// </summary>
        /// <type>string</type>
        public string maxCountOrder
        {
            get
            {
                return _maxCountOrder;
            }
            set
            {
                _maxCountOrder = value;
            }
        }

        string _havaCountOrder = "";
        /// <summary>
        /// 已生成服务的订单数量
        /// </summary>
        /// <type>string</type>
        public string havaCountOrder
        {
            get
            {
                return _havaCountOrder;
            }
            set
            {
                _havaCountOrder = value;
            }
        }

        IList<workTimeObj> _workTimeArray = new List<workTimeObj>();
        /// <summary>
        /// 该快照下包含的工作时间数组
        /// </summary>
        /// <type>array[workTimeObj]</type>
        public IList<workTimeObj> workTimeArray
        {
            get
            {
                return _workTimeArray;
            }
            set
            {
                _workTimeArray = value;
            }
        }
    }
}
