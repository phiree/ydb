using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dianzhu.ApplicationService
{
    public class UserChangeBody
    {
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
        ///地址
        /// </summary>
        /// <type>string</type>
        public string address { get; set; }

        /// <summary>
        ///性别（女：true,男：false）
        /// </summary>
        /// <type>string</type>
        public string sex { get; set; }

        /// <summary>
        ///新密码
        /// </summary>
        /// <type>string</type>
        public string newPassWord { get; set; }

        /// <summary>
        ///原始密码（必填）
        /// </summary>
        /// <type>string</type>
        [Required]
        public string oldPassWord { get; set; }
    }
}
