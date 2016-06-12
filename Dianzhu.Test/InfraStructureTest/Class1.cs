using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSYK.Infrastructure;
using NUnit.Framework;
namespace Dianzhu.Test.InfraStructureTest
{
    [TestFixture]
    public class EncryptServiceTest
    {
        [Test]
        public void TestGetMd5()
        {
            EncryptService ens = new EncryptService();
            string md5=    ens.GetMD5Hash("123456");
            Console.WriteLine(md5);
        }
    }
}
