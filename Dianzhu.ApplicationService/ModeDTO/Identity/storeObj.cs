using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Dianzhu.ApplicationService
{
    public class storeObj
    {
        string _id = null;
        /// <summary>
        /// 店铺ID
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

        string _name = "";
        /// <summary>
        /// 店铺名称
        /// </summary>
        /// <type>string</type>
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        string _introduction = "";
        /// <summary>
        ///简介
        /// </summary>
        /// <type>string</type>
        public string introduction
        {
            get
            {
                return _introduction;
            }
            set
            {
                _introduction = value;
            }
        }

        string _storePhone = "";
        /// <summary>
        ///店铺电话
        /// </summary>
        /// <type>string</type>
        public string storePhone
        {
            get
            {
                return _storePhone;
            }
            set
            {
                _storePhone = value;
            }
        }

        string _imgUrl = "";
        /// <summary>
        ///店铺的头像
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

        string _linkMan = "";
        /// <summary>
        ///联系人
        /// </summary>
        /// <type>string</type>
        public string linkMan
        {
            get
            {
                return _linkMan;
            }
            set
            {
                _linkMan = value;
            }
        }

        string _linkIdentity = "";
        /// <summary>
        ///联系人的身份证
        /// </summary>
        /// <type>string</type>
        public string linkIdentity
        {
            get
            {
                return _linkIdentity;
            }
            set
            {
                _linkIdentity = value;
            }
        }

        string _linkPhone = "";
        /// <summary>
        ///联系人的手机号
        /// </summary>
        /// <type>string</type>
        [StringLength(11, ErrorMessage = "手机号码为11位.", MinimumLength = 11)]
        [RegularExpression(@"^1[3578]\d{9}$", ErrorMessage = "手机号码不合法！")]
        public string linkPhone
        {
            get
            {
                return _linkPhone;
            }
            set
            {
                _linkPhone = value;
            }
        }

        IList<String> _certificateImgUrls = new List<String>();
        /// <summary>
        ///认证资质的图片地址（如身份证正反面）
        /// </summary>
        /// <type>array[string]</type>
        public IList<String> certificateImgUrls
        {
            get
            {
                return _certificateImgUrls;
            }
            set
            {
                _certificateImgUrls = value;
            }
        }

        IList<String> _showImgUrls = new List<String>();
        /// <summary>
        ///店铺的展示图片
        /// </summary>
        /// <type>array[string]</type>
        public IList<String> showImgUrls
        {
            get
            {
                return _showImgUrls;
            }
            set
            {
                _showImgUrls = value;
            }
        }

        string _url = "";
        /// <summary>
        ///店铺的网页
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

        string _vintage = "";
        /// <summary>
        ///店铺的年份
        /// </summary>
        /// <type>string</type>
        public string vintage
        {
            get
            {
                return _vintage;
            }
            set
            {
                _vintage = value;
            }
        }

        string _headCount = "";
        /// <summary>
        ///店铺总人数
        /// </summary>
        /// <type>string</type>
        public string headCount
        {
            get
            {
                return _headCount;
            }
            set
            {
                _headCount = value;
            }
        }

        string _appraise = "";
        /// <summary>
        ///店铺的平均评价
        /// </summary>
        /// <type>string</type>
        public string appraise
        {
            get
            {
                return _appraise;
            }
            set
            {
                _appraise = value;
            }
        }

        string _address = "";
        /// <summary>
        ///店铺的中文地址
        /// </summary>
        /// <type>string</type>
        public string address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }

        locationObj _location = new locationObj();
        /// <summary>
        ///定位信息模型
        /// </summary>
        /// <type>locationObj</type>
        public locationObj location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }


    }
}
