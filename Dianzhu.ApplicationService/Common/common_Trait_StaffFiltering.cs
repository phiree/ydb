using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class common_Trait_StaffFiltering
    {
        /// <summary>
        /// 员工昵称
        /// </summary>
        /// <type>string</type>
        public string alias { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        /// <type>string</type>
        public string email { get; set; }

        /// <summary>
        /// 员工电话
        /// </summary>
        /// <type>string</type>
        public string phone { get; set; }

        /// <summary>
        ///性别（女：true,男：false）
        /// </summary>
        /// <type>boolean</type>
        public string sex { get; set; }

        /// <summary>
        ///特长
        /// </summary>
        /// <type>string</type>
        public string specialty { get; set; }

        /// <summary>
        ///真实姓名
        /// </summary>
        /// <type>string</type>
        public string realName { get; set; }
    }
}
