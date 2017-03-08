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

        /// <summary>
        /// 判断DateTime
        /// </summary>
        /// <param name="strDateTime">DateTime</param>
        /// <param name="strError">错误信息</param>
        /// <returns></returns>
        public static DateTime CheckDateTime(string strDateTime, string strType, string strError,bool b)
        {
            if (string.IsNullOrEmpty(strDateTime))
                return DateTime.MinValue;
            DateTime dt;
            try
            {
                dt = DateTime.ParseExact(strDateTime, strType, null);
                //dt = DateTime.Parse(stime); .ToString("yyyy-MM-dd HH:mm:ss fff")
                if (b)
                {
                    dt=dt.AddDays(1);
                }
                return dt;
            }
            catch (Exception e)
            {
                throw new FormatException(strError + "格式有误");
            }
        }

        /// <summary>
        /// 判断Guid
        /// </summary>
        /// <param name="strGuidID">GuidID</param>
        /// <param name="strError">错误信息</param>
        /// <returns></returns>
        public static Guid CheckGuidID(string strGuidID, string strError)
        {
            if (string.IsNullOrEmpty(strGuidID))
                return Guid.Empty;
            Guid GuidID;
            bool guididisGuid = Guid.TryParse(strGuidID, out GuidID);
            if (!guididisGuid)
            {
                throw new FormatException(strError + "格式有误");
            }
            return GuidID;
        }
    }
}
