using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PHSuit
{
    public class PHCore
    {
        public static DateTime GetNextDay(DateTime date)
        {
            DateTime nextDay =Convert.ToDateTime(date.AddDays(1).ToShortDateString());
            return nextDay;
        }
        private static readonly Random random = new Random();
        /// <summary>
        /// 随机生成由数字组成的字符串
        /// </summary>
        /// <param name="size">长度</param>
        /// <returns></returns>
        public static string GetRandom(int size)
        { 
            string max_str=string.Empty;
            for(int i=0;i<size;i++)
            {
                int c = random.Next(9);
                max_str+=""+c;
            }

            return max_str;
            
        }
    }
}
