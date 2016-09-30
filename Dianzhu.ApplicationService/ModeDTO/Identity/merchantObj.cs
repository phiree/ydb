using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dianzhu.ApplicationService
{
    public class merchantObj
    {
        string _id = null;
        /// <summary>
        /// 商户ID
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

        string _alias = "";
        /// <summary>
        /// 昵称
        /// </summary>
        /// <type>string</type>
        public string alias
        {
            get
            {
                return _alias;
            }
            set
            {
                _alias = value;
            }
        }

        string _email = "";
        /// <summary>
        /// 邮箱
        /// </summary>
        /// <type>string</type>
        public string email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        string _phone = "";
        /// <summary>
        /// 手机
        /// </summary>
        /// <type>string</type>
        [StringLength(11, ErrorMessage = "手机号码为11位.", MinimumLength = 11)]
        [RegularExpression(@"^1[3578]\d{9}$", ErrorMessage = "手机号码不合法！")]
        public string phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
            }
        }

        string _imgUrl = "";
        /// <summary>
        ///头像
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

        string _pWord = "";
        /// <summary>
        /// 密码
        /// </summary>
        /// <type>string</type>
        public string pWord
        {
            get
            {
                return _pWord;
            }
            set
            {
                _pWord = value;
            }
        }


    }
}
