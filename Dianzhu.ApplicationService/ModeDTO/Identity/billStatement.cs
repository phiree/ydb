using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class billStatement
    {

        string _date = "";
        /// <summary>
        /// 统计时间(日期/月份)
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

        string _Income = "";
        /// <summary>
        /// 总收入
        /// </summary>
        /// <type>string</type>
        public string Income
        {
            get
            {
                return _Income;
            }
            set
            {
                _Income = value;
            }
        }

        string _Expenditure = "";
        /// <summary>
        /// 总支出
        /// </summary>
        /// <type>string</type>
        public string Expenditure
        {
            get
            {
                return _Expenditure;
            }
            set
            {
                _Expenditure = value;
            }
        }

        IList<serviceTypeBillObj> _ServiceTypeBillObj = new List<serviceTypeBillObj>();
        /// <summary>
        /// 类型明细统计
        /// </summary>
        /// <type>array[string]</type>
        public IList<serviceTypeBillObj> ServiceTypeBillObj
        {
            get
            {
                return _ServiceTypeBillObj;
            }
            set
            {
                _ServiceTypeBillObj = value;
            }
        }
    }
}
