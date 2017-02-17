using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Ydb.Common.Infrastructure
{
    public class JsonHelper
    {
        static log4net.ILog log = log4net.LogManager.GetLogger("PHSuit.JsonHelper");

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
        /// <summary>
        /// xml格式字符串转换为json格式.  cdata的内容直接取出.
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="removeRoot"></param>
        /// <returns></returns>
        public static string Xml2Json(string xml, bool removeRoot)
        {
            Regex r1 = new Regex(@"<!\[cdata\[.*?\]\]\>", RegexOptions.IgnoreCase);
            Regex r2 = new Regex(@"(?<=<!\[cdata\[).*?(?=\]\])", RegexOptions.IgnoreCase);
            Regex r3 = new Regex(@"\<\?.+\?\>", RegexOptions.IgnoreCase);
            MatchCollection m1 = r1.Matches(xml);
            MatchCollection m2 = r2.Matches(xml);

            xml = r3.Replace(xml, string.Empty);

            for (int i = 0; i < m1.Count; i++)
            {
                xml = xml.Replace(m1[i].Value, m2[i].Value);
            }
            if (m1.Count != m2.Count)
            {
                log.Error("xml解析有误: Cdata解析有误.");
                throw new Exception("xml解析有误:Cdata解析有误.");
            }

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            if (m1.Count > 0)
            {
                XmlNode n = xmlDoc.CreateElement("HasCDATA");
                n.InnerText = "True";
                xmlDoc.DocumentElement.AppendChild(n);
            }

            string json = Newtonsoft.Json.JsonConvert.SerializeXmlNode(xmlDoc, Newtonsoft.Json.Formatting.Indented, removeRoot);
            return json;
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
