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
        /// <summary>
        /// 员工ID
        /// </summary>
        /// <type>string</type>
        public string id { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        /// <type>string</type>
        public string number { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        /// <type>string</type>
        public string alias { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        /// <type>string</type>
        public string email { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        /// <type>string</type>
        [StringLength(11, ErrorMessage = "手机号码为11位.", MinimumLength = 11)]
        [RegularExpression(@"^1[3578]\d{9}$", ErrorMessage = "手机号码不合法！")]
        public string phone { get; set; }

        /// <summary>
        ///头像
        /// </summary>
        /// <type>string</type>
        public string imgUrl { get; set; }

        /// <summary>
        ///性别（女：true,男：false）
        /// </summary>
        /// <type>boolean</type>
        public bool sex { get; set; }

        /// <summary>
        ///特长描述
        /// </summary>
        /// <type>string</type>
        public string specialty { get; set; }

        /// <summary>
        ///真实姓名
        /// </summary>
        /// <type>string</type>
        public string realName { get; set; }


        /// <summary>
        ///身份证号
        /// </summary>
        /// <type>string</type>
        public string identity { get; set; }

        /// <summary>
        /// 与店铺相关数据
        /// </summary>
        /// <type>string</type>
        public storeDataObj storeData { get; set; }
    }
}
