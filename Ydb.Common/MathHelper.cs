using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Common
{
    public class MathHelper
    {
        /// <summary>
        /// 计算比率
        /// </summary>
        /// <param name="lMonth"></param>
        /// <param name="lBeforeMonth"></param>
        /// <returns></returns>
        public static string GetCalculatedRatio(decimal lMonth, decimal lBeforeMonth)
        {
            if (lBeforeMonth == 0)
            {
                return "0";
            }
            decimal ratio = lMonth / lBeforeMonth * 100;
            return string.Format("{0:f}", ratio) + "%";
        }
    }
}
