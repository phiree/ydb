using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Text.RegularExpressions;
namespace Dianzhu.Test
{
    [TestFixture]
    public class jsonconvert_test
    {
        [Test]
        public void aa()
        {
            string regp = @"_(\d+)[x|X](\d+)";
            Regex r = new Regex(regp);
            Match m = r.Match("asdfaewr.aspx?_40Xd13");
            Console.WriteLine(m.Groups[1]+","+m.Groups[2]);
          //  Console.WriteLine(m[1]);
        }
    }

}
