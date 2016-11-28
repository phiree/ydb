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
    }
}
