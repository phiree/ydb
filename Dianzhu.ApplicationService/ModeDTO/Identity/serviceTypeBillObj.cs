using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class serviceTypeBillObj
    {
        string _id = "";
        /// <summary>
        /// 服务类型ID
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
    }
}
