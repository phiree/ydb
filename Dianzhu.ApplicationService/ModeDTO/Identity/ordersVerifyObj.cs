using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService
{
    public class ordersVerifyObj
    {
        string _verifyMD5 = "";
        /// <summary>
        /// MD5加密结果
        /// </summary>
        /// <type>string</type>
        public string verifyMD5
        {
            get
            {
                return _verifyMD5;
            }
            set
            {
                _verifyMD5 = value;
            }
        }
    }
}
