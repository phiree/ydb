using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Common
{
   public class StringHelper
    {
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
