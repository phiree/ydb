using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ydb.Infrastructure
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
    }
}
