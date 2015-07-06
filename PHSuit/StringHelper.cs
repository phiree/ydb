using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace PHSuit
{
    public class StringHelper
    {
        //
        public static string InsertToId(string guidWithNo)
        {
            /*45bc9230   8
             * -9e4f   4
             * -4fec    4
             * -ab65    4
             * -a4c700a45c99 12
             */
            if (guidWithNo.Length != 32)
            {
                throw new ArgumentException("传入字符串长度不是36");
            }
            string result = guidWithNo.Substring(0, 8)
                + "-" + guidWithNo.Substring(8, 4)
                + "-" + guidWithNo.Substring(12, 4)
                + "-" + guidWithNo.Substring(16, 4)
                + "-" + guidWithNo.Substring(20, 12);
       
        return result;
        }
        public static string ReplaceSpace(string input)
        {
            string patern = @"\s*";
            return Regex.Replace(input, patern, string.Empty);
        }
        public static string ReplaceInvalidChaInFileName(string input, string replacement)
        {
            input = input.Trim();
            string partern = @"[\\\/\:\'\?\*\<\>\|\n|\s]";
            return Regex.Replace(input, partern, replacement);
        }
        public static string ReplaceInvalidChaInFileName(string input)
        {
            return ReplaceInvalidChaInFileName(input, string.Empty);
        }
        public static bool ReplaceSpaceAndCompare(string s1, string s2)
        {
            s1 = ReplaceSpace(s1);
            s2 = ReplaceSpace(s2);
            return s1 == s2;
        }
        /// <summary>
        /// 保证字符串的字符数量
        /// </summary>
        /// <param name="input"></param>
        /// <param name="exceptLenth">希望的长度</param>
        /// <param name="fillChar"> 填充字符 </param>
        /// <returns></returns>
        public static string EnsureStringLength(string input, int exceptLenth, char fillChar)
        {
            throw new NotImplementedException();
        }

        public static string[] Split(string original, char s)
        {
            return original.Split(s);
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
        #region 全角半角转换

        /// <summary>

        /// 转全角的函数(SBC case)

        /// </summary>

        /// <param name="input">任意字符串</param>

        /// <returns>全角字符串</returns>

        ///<remarks>
        ///</remarks>

        public static string ToSBC(string input)
        {

            //半角转全角：

            char[] c = input.ToCharArray();

            for (int i = 0; i < c.Length; i++)
            {

                if (c[i] == 32)
                {

                    c[i] = (char)12288;

                    continue;

                }

                if (c[i] < 127)

                    c[i] = (char)(c[i] + 65248);

            }

            return new string(c);

        }

        /// <summary> 转半角的函数(DBC case) </summary>

        /// <param name="input">任意字符串</param>

        /// <returns>半角字符串</returns>

        ///<remarks>

        ///全角空格为12288，半角空格为32

        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248

        ///</remarks>

        public static string ToDBC(string input)
        {

            char[] c = input.ToCharArray();

            for (int i = 0; i < c.Length; i++)
            {

                if (c[i] == 12288)
                {

                    c[i] = (char)32;

                    continue;

                }

                if (c[i] > 65280 && c[i] < 65375)

                    c[i] = (char)(c[i] - 65248);

            }

            return new string(c);

        }

        #endregion

        /// <summary>
        /// 用正则判断字符串属于哪种语言.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string LanguageTypeDetermine(string input)
        {
            if (Regex.IsMatch(input, "[\u4e00-\u9fa5]"))
            {
                return "zh";
            }
            return "en";
        }
        /// <summary>
        /// 补全字符串
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static string FullFillWidth(string original, string fillString, int width, bool prepend)
        {
            string s = fillString + original;
            if (!prepend)
                s = original + fillString;
            return s.Substring(s.Length - width);
        }


    }
}