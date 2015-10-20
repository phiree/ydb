using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
namespace PHSuit
{
    public class StringHelper
    {
        //
        public static string InsertToId(string guidWithNo)
        {
            return guidWithNo;
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

        /// <summary>
        /// 更新url
        /// </summary>
        /// <param name="original">Request.QueryString.Tostring()</param>
        /// <param name="paramName">休要更新的参数名</param>
        /// <param name="value">新值</param>
        /// <returns></returns>
        public static string BuildUrlWithParameters(HttpRequest request, string paramName, string value)
        {
            var nameValues = HttpUtility.ParseQueryString(request.QueryString.ToString());
            nameValues.Set(paramName, value);
            string url = request.Url.AbsolutePath;
            string updatedQueryString = "?" + nameValues.ToString();
           return (url + updatedQueryString);
        }

        public static bool IsEamil(string str)
        {
            return true;
        }
        public static string EnsureNormalUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return userName;
            string normalUserName = userName;
            if (Regex.IsMatch(userName, @"^[^\.@]+||[^\.@]+\.[^\.@]+$"))
            {
                normalUserName = userName.Replace("||", "@");
            }
            return normalUserName;
        }
        /// <summary>
        ///openfire 用户转换成普通用户.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static string EnsureOpenfireUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return userName;
            string openfireName = userName;
            if (Regex.IsMatch(userName, @"^[^\.@]+@[^\.@]+\.[^\.@]+$"))
            {
                openfireName = userName.Replace("@", "||");
            }
            return openfireName;
        }
        public static string ParseUrlParameter(string url,string param)
        {
            string result = string.Empty;
            Uri uri = new Uri(url);
        var vv=   HttpUtility.ParseQueryString(uri.Query);
            if (string.IsNullOrEmpty(param) )
            {

                result = vv.Get(0);
            }
            else
            {
                
                result = vv.Get(param);
            }
            return result;
        }
        public static IList<string> ParseUrl(string input,out bool isMatch)
        {
            List<string> result = new List<string>();
            string regp = @"((http://)|(www)).+?(\s|$)?";
            Regex r = new Regex(regp);
            isMatch= r.IsMatch(input);
            if (!isMatch)
            {
                return result;
            }
            MatchCollection ms = new Regex(regp).Matches(input);
            foreach (Match m in ms)
            {
                result.Add(m.Value);
            }

            return result;
        }

       
    }

   public class JsonHelper
    {
        private const string INDENT_STRING = "    ";
        public static string FormatJson(string str)
        {
            var indent = 0;
            var quoted = false;
            var sb = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                switch (ch)
                {
                    case '{':
                    case '[':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, ++indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case '}':
                    case ']':
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, --indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        sb.Append(ch);
                        break;
                    case '"':
                        sb.Append(ch);
                        bool escaped = false;
                        var index = i;
                        while (index > 0 && str[--index] == '\\')
                            escaped = !escaped;
                        if (!escaped)
                            quoted = !quoted;
                        break;
                    case ',':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case ':':
                        sb.Append(ch);
                        if (!quoted)
                            sb.Append(" ");
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }
            return sb.ToString();
        }
    }

    static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action)
        {
            foreach (var i in ie)
            {
                action(i);
            }
        }
    }
}