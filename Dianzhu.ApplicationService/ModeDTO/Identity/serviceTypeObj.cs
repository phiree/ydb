using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class serviceTypeObj
    {
        string _id = null;
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

        string _fullDescription = "";
        /// <summary>
        /// 服务类型的完整描述,">"区分,由顶级开始
        /// </summary>
        /// <type>string</type>
        public string fullDescription
        {
            get
            {
                return _fullDescription;
            }
            set
            {
                _fullDescription = value;
            }
        }

        string _superID = null;
        /// <summary>
        /// 上级服务类型的 ID
        /// </summary>
        /// <type>string</type>
        public string superID
        {
            get
            {
                return _superID;
            }
            set
            {
                _superID = value;
            }
        }
    }
}
