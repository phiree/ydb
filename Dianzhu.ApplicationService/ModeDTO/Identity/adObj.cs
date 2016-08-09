using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class adObj
    {
        string _imgUrl = "";
        /// <summary>
        /// 图片Url
        /// </summary>
        /// <type>string</type>
        public string imgUrl
        {
            get
            {
                return _imgUrl;
            }
            set
            {
                _imgUrl = value;
            }
        }

        string _url = "";
        /// <summary>
        /// 广告的链接
        /// </summary>
        /// <type>string</type>
        public string url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
            }
        }

        string _num = "";
        /// <summary>
        /// 序号
        /// </summary>
        /// <type>string</type>
        public string num
        {
            get
            {
                return _num;
            }
            set
            {
                _num = value;
            }
        }

        string _updateTime = "";
        /// <summary>
        /// 更新该广告的时间（yyyyMMddHHmmss）
        /// </summary>
        /// <type>string</type>
        public string updateTime
        {
            get
            {
                return _updateTime;
            }
            set
            {
                _updateTime = value;
            }
        }
    }
}
