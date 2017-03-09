using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class appraiseObj
    {
        string _target = "";
        /// <summary>
        /// 评价的目标
        /// </summary>
        /// <type>string</type>
        public string target
        {
            get
            {
                return _target;
            }
            set
            {
                if (value.ToLower() == "customerservice")
                {
                    _target = "customerService";
                }
                else if (value.ToLower() == "store")
                {
                    _target = "store";
                }
                else
                {
                    _target = value;
                }
            }
        }

        string _value = "";
        /// <summary>
        /// 评分值（0~5的整数）
        /// </summary>
        /// <type>string</type>
        public string value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        string _content = "";
        /// <summary>
        /// 描述
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
