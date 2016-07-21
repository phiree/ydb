using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dianzhu.ApplicationService
{
    public class staffObj
    {
        string _id = null;
        /// <summary>
        /// 员工ID
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

        string _loginName = "";
        /// <summary>
        /// 员工登录App的用户名
        /// </summary>
        /// <type>string</type>
        public string loginName
        {
            get
            {
                return _loginName;
            }
            set
            {
                _loginName = value;
            }
        }

        string _userID = "";
        /// <summary>
        /// 员工用户ID
        /// </summary>
        /// <type>string</type>
        public string userID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
            }
        }

        string _pWord = "";
        /// <summary>
        /// 员工登录App的密码
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

        string _number = "";
        /// <summary>
        /// 员工编号
        /// </summary>
        /// <type>string</type>
        public string number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
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

        bool _sex = false;
        /// <summary>
        ///性别（女：true,男：false）
        /// </summary>
        /// <type>boolean</type>
        public bool sex
        {
            get
            {
                return _sex;
            }
            set
            {
                _sex = value;
            }
        }

        string _specialty = "";
        /// <summary>
        ///特长描述
        /// </summary>
        /// <type>string</type>
        public string specialty
        {
            get
            {
                return _specialty;
            }
            set
            {
                _specialty = value;
            }
        }

        string _realName = "";
        /// <summary>
        ///真实姓名
        /// </summary>
        /// <type>string</type>
        public string realName
        {
            get
            {
                return _realName;
            }
            set
            {
                _realName = value;
            }
        }

        string _identity = "";
        /// <summary>
        ///身份证号
        /// </summary>
        /// <type>string</type>
        public string identity
        {
            get
            {
                return _identity;
            }
            set
            {
                _identity = value;
            }
        }

        storeDataObj _storeData =new storeDataObj();
        /// <summary>
        /// 与店铺相关数据
        /// </summary>
        /// <type>string</type>
        public storeDataObj storeData
        {
            get
            {
                return _storeData;
            }
            set
            {
                _storeData = value;
            }
        }
    }
}
