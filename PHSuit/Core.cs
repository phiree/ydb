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
        public static string GetRandom(int size)
        { 
            string max_str=string.Empty;
            Random rm = new Random();
            
            for(int i=0;i<size;i++)
            {
                int c = rm.Next(9);
                max_str+=""+c;
            }

            return max_str;
            
        }
    }
}
