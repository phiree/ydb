using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ydb.Common
{
   public class StringHelper
    {
        public static string ReplaceSpace(string input)
        {
            string patern = @"\s*";
            return Regex.Replace(input, patern, string.Empty);
        }
        public static bool IsSameDomain(string namespace1, string namespace2)
        {

            return GetFirstTwoSecion(namespace1) == GetFirstTwoSecion(namespace2);
       }
        private static string GetFirstTwoSecion(string nameSpace)
        {
            string[] sections = nameSpace.Split('.');
            return sections[0] + sections[1];
        }
        /// <summary>
        /// 通过普通的Query hql语句 获取 count* 语句
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string BuildCountQuery(string query)
        {
            ////"select s from supplier from supplier d where 1=1   "


            Regex reg = new Regex(@"(?<=select\s+(distinct)?).*?(?=from)");

            Match m = reg.Match(query);

            string result = reg.Replace(query, " count(*) ", 1);
            return result;
        }

        public static DateTime ParseToDate(string strDate,bool b)
        {
            try
            {
                DateTime dt = DateTime.Parse(strDate);
                if (b)
                {
                    dt.AddDays(1);
                }
                return dt;
            }
            catch
            {
                throw new Exception("日期格式不正确");
            }
            
        }
    }
}
