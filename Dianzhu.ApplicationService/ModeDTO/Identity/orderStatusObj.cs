using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class orderStatusObj
    {
        string _status = "";
        /// <summary>
        /// 状态字符串
        /// </summary>
        /// <type>string</type>
        public string status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        string _createTime = "";
        /// <summary>
        /// 生成时间
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

        string _lastStatus = "";
        /// <summary>
        /// 对应的上一状态
        /// </summary>
        /// <type>string</type>
        public string lastStatus
        {
            get
            {
                return _lastStatus;
            }
            set
            {
                _lastStatus = value;
            }
        }

        string _title = "";
        /// <summary>
        /// 标题
        /// </summary>
        /// <type>string</type>
        public string title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        string _content = "";
        /// <summary>
        /// 详细描述
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
    }
}
