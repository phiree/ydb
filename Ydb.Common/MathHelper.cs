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


        /// <summary>
        /// 计算两个时间的时间差
        /// </summary>
        /// <param name="DateTime1"></param>
        /// <param name="DateTime2"></param>
        /// <returns></returns>
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
            return dateDiff;
        }

        /// <summary>
        /// 计算两个时间的时间差返回总好毫秒数
        /// </summary>
        /// <param name="DateTime1"></param>
        /// <param name="DateTime2"></param>
        /// <returns></returns>
        public static long DateDiffTicks(DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts.Ticks;
        }

        /// <summary>
        ///  将时间差的总毫秒数转换成详细的时间段
        /// </summary>
        /// <param name="totalSecond"></param>
        /// <returns></returns>
        public static string ChangeDateDiff(long ticks)
        {
            string dateDiff = "";
            TimeSpan ts = new TimeSpan(ticks);
            if (ts.Days > 0)
            {
                dateDiff = dateDiff + ts.Days.ToString() + "天";
            }
            if (ts.Hours > 0 || dateDiff != "")
            {
                dateDiff = dateDiff + ts.Hours.ToString() + "小时";
            }
            if (ts.Minutes > 0 || dateDiff != "")
            {
                dateDiff = dateDiff + ts.Minutes.ToString() + "分钟";
            }
            if (ts.Seconds > 0 || dateDiff == "")
            {
                dateDiff = dateDiff + ts.Seconds.ToString() + "秒";
            }
            return dateDiff;
        }


    }
}
