using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class workTimeObj
    {
        string _id = null;
        /// <summary>
        /// 工作时间的ID
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

        string _tag = "";
        /// <summary>
        /// 标签
        /// </summary>
        /// <type>string</type>
        public string tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }

        string _startTime = "";
        /// <summary>
        /// 开始时间（HH:mm）
        /// </summary>
        /// <type>string</type>
        public string startTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                _startTime = value;
            }
        }

        string _endTime = "";
        /// <summary>
        /// 结束时间（HH:mm）
        /// </summary>
        /// <type>string</type>
        public string endTime
        {
            get
            {
                return _endTime;
            }
            set
            {
                _endTime = value;
            }
        }

        string _week = "";
        /// <summary>
        /// 星期数（1~7 周一~周日）
        /// </summary>
        /// <type>string</type>
        public string week
        {
            get
            {
                return _week;
            }
            set
            {
                _week = value;
            }
        }
        //public DayOfWeek week1 { get; set; }

        bool _bOpen = true;
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

        string _maxCountOrder = "";
        /// <summary>
        /// 改时间段的最大接单量
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
    }
}
