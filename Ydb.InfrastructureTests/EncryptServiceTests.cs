using NUnit.Framework;
using Ydb.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ydb.Infrastructure.Tests
{
    [TestFixture()]
    public class EncryptServiceTests
    {
        EncryptService encryptService = new EncryptService();
        [Test()]
        public void EncryptTestWithPrefixComment()
        {

            string rawString = "150___server:localhost;pwd=p;uid=3";
            string encrypted = encryptService.Encrypt(rawString, false);
            Assert.IsTrue(encrypted.StartsWith("150___"));

            string decrpyted = encryptService.Decrypt(encrypted, false);
            Assert.IsTrue(decrpyted.StartsWith("server"));
        }
        [Test()]
        public void EncryptTestWithoutPrefixComment()
        {
            var encryptService = new EncryptService();
            string rawString = "server:localhost;pwd=p;uid=3";
            string encrypted = encryptService.Encrypt(rawString, false);
            Assert.IsFalse(encrypted.StartsWith("150____"));

            string decrpyted = encryptService.Decrypt(encrypted, false);
            Assert.IsTrue(decrpyted.StartsWith("server"));
        }
        [Test()]
        public void EncryptTestWithEmptyPrefixComment()
        {
            var encryptService = new EncryptService();
            string rawString = "___server:localhost;pwd=p;uid=3";
            string encrypted = encryptService.Encrypt(rawString, false);
            Assert.IsFalse(encrypted.StartsWith("___"));

            string decrpyted = encryptService.Decrypt(encrypted, false);
            Assert.IsTrue(decrpyted.StartsWith("server"));
        }
      
        [Test]
        public void DbConfigBuilderTest()
        {
            Ydb.InfrastructureTests.DbConfigBuilder dbConfig = new InfrastructureTests.DbConfigBuilder();

            var l=   dbConfig.BuildForServer("localhost", "root", "root");
            Console.WriteLine(l[0]);
            Assert.AreEqual("localhost_dianzhu___data source=localhost;database=dianzhu;uid=root;pwd=root;port=3306;", l[0]);
            Assert.AreEqual("localhost_ydb_finance___data source=localhost;database=ydb_finance;uid=root;pwd=root;port=3306;", l[1]);
            var l2 = dbConfig.ReplaceDianzhuDb("dianzhu_publish_test").BuildForServer("192.168.1.172", "root", "root");

            Assert.AreEqual("192.168.1.172_dianzhu_publish_test___data source=192.168.1.172;database=dianzhu_publish_test;uid=root;pwd=root;port=3306;", l2[0]);

        }
        [Test]
        public void EncryptTest__()
        {
         
            PrintOneServer(new InfrastructureTests.DbConfigBuilder().BuildForServer("localhost", "root", "root"));
            PrintOneServer(new InfrastructureTests.DbConfigBuilder().ReplaceDianzhuDb("dianzhu_publish_test"). BuildForServer("192.168.1.172", "root", "root"));
            PrintOneServer(new InfrastructureTests.DbConfigBuilder().ReplaceDianzhuDb("dianzhu_test").BuildForServer("192.168.1.150", "root", "root"));
            PrintOneServer(new InfrastructureTests.DbConfigBuilder().BuildForServer("business.ydban.cn", "jsyk2016", "X*G7czoy6twAYIz7","4407"));
            PrintOneServer(new InfrastructureTests.DbConfigBuilder().BuildForServer("dev.ydban.cn", "jsyk2016", "IQDv$qefeqFWuq_L","4407"));
            PrintConfigSection(new InfrastructureTests.DbConfigBuilder().ReplaceDianzhuDb("dianzhu_publish"). BuildForServerConfig("dev.ydban.cn", "jsyk2016", "IQDv$qefeqFWuq_L", "4407"));
            
            PrintConfigSection(new InfrastructureTests.DbConfigBuilder().BuildForServerConfig("localhost", "root", "root","3306"));


            PrintConfigSection(new InfrastructureTests.DbConfigBuilder().ReplaceDianzhuDb("dianzhu_test").BuildForServerConfig("192.168.1.150", "root", "root", "3306"));
          
            /*
            Console.WriteLine("172_dianzhu_publish_test:" + encryptService.Encrypt("172_dianzhu___data source=192.168.1.172;uid=root;pwd=root;database=dianzhu_publish_test", false));
            Console.WriteLine("172_ydb_membership:" + encryptService.Encrypt("172_membership___data source=192.168.1.172;uid=root;pwd=root;database=ydb_membership", false));
            Console.WriteLine("172_ydb_instantmessage:" + encryptService.Encrypt("172_instantmessage___data source=192.168.1.172;uid=root;pwd=root;database=ydb_instantmessage", false));
            Console.WriteLine("172_ydb_finance:" + encryptService.Encrypt("172_finance___data source=192.168.1.172;uid=root;pwd=root;database=ydb_finance", false));
            Console.WriteLine("172_ydb_common:" + encryptService.Encrypt("172_common___data source=192.168.1.172;uid=root;pwd=root;database=ydb_common", false));
            Console.WriteLine("172_ydb_businessresource:" + encryptService.Encrypt("172_businessresource___data source=192.168.1.172;uid=root;pwd=root;database=ydb_businessresource", false));

            Console.WriteLine("localhost_Dianzhu" + ":" + encryptService.Encrypt("localhost_dianzhu___data source=localhost;uid=root;pwd=root;database=dianzhu", false));
            Console.WriteLine("localhost_ydb_membership" + ":" + encryptService.Encrypt("localhost_membership___data source=localhost;uid=root;pwd=root;database=ydb_membership", false));
            Console.WriteLine("localhost_ydb_instantmessage" + ":" + encryptService.Encrypt("localhost_instantmessage___data source=localhost;uid=root;pwd=root;database=ydb_instantmessage", false));
            Console.WriteLine("localhost_ydb_finance" + ":" + encryptService.Encrypt("localhost_finance___data source=localhost;uid=root;pwd=root;database=ydb_finance", false));
            Console.WriteLine("localhost_ydb_common" + ":" + encryptService.Encrypt("localhost_common___data source=localhost;uid=root;pwd=root;database=ydb_common", false));
            Console.WriteLine("localhost_ydb_businessresource" + ":" + encryptService.Encrypt("localhost_businessresource___data source=localhost;uid=root;pwd=root;database=ydb_businessresource", false));

            Console.WriteLine("150_dianzhu_test:" + encryptService.Encrypt("data source=192.168.1.150;uid=ydb;pwd=jsyk2016;database=dianzhu_test", false));
            Console.WriteLine("150_ydb_instantmessage:" + encryptService.Encrypt("data source=192.168.1.150;uid=ydb;pwd=jsyk2016;database=ydb_instantmessage", false));

            Console.WriteLine("Aliyun_Official_dianzhu_publish:" + encryptService.Encrypt("data source=business.ydban.cn;uid=jsyk2016;pwd=X*G7czoy6twAYIz7;port=4407;database=dianzhu", false));
            Console.WriteLine("Aliyun_Official_ydb_instantmessage:" + encryptService.Encrypt("data source=business.ydban.cn;uid=jsyk2016;pwd=X*G7czoy6twAYIz7;port=4407;database=ydb_instantmessage", false));
            Console.WriteLine("Aliyun_Official_ydb_membership:" + encryptService.Encrypt("data source=business.ydban.cn;uid=jsyk2016;pwd=X*G7czoy6twAYIz7;port=4407;database=ydb_membership", false));
            Console.WriteLine("Aliyun_Official_ydb_finance:" + encryptService.Encrypt("data source=business.ydban.cn;uid=jsyk2016;pwd=X*G7czoy6twAYIz7;port=4407;database=ydb_finance", false));
            Console.WriteLine("Aliyun_Official_ydb_common:" + encryptService.Encrypt("data source=business.ydban.cn;uid=jsyk2016;pwd=X*G7czoy6twAYIz7;port=4407;database=ydb_common", false));

            Console.WriteLine("Aliyun_Test_dianzhu_publish:" + encryptService.Encrypt("data source=dev.ydban.cn;uid=jsyk2016;pwd=IQDv$qefeqFWuq_L;port=4407;database=dianzhu_publish", false));
            Console.WriteLine("Aliyun_Test_ydb_instantmessage:" + encryptService.Encrypt("data source=dev.ydban.cn;uid=jsyk2016;pwd=IQDv$qefeqFWuq_L;port=4407;database=ydb_instantmessage", false));
            Console.WriteLine("Aliyun_Test_ydb_membership:" + encryptService.Encrypt("data source=dev.ydban.cn;uid=jsyk2016;pwd=IQDv$qefeqFWuq_L;port=4407;database=ydb_membership", false));
            Console.WriteLine("Aliyun_Test_ydb_finance:" + encryptService.Encrypt("data source=dev.ydban.cn;uid=jsyk2016;pwd=IQDv$qefeqFWuq_L;port=4407;database=ydb_finance", false));
            Console.WriteLine("Aliyun_Test_ydb_common:" + encryptService.Encrypt("data source=dev.ydban.cn;uid=jsyk2016;pwd=IQDv$qefeqFWuq_L;port=4407;database=ydb_common", false));
            */
        }


        private void PrintOneServer(IList<string> l)

        {
            foreach (string s in l)
            {
                Console.WriteLine( encryptService.Encrypt(s, false));
            }
        }
        private void PrintConfigSection(IList<string> l)
        {
            StringBuilder sbConfig = new StringBuilder();
            sbConfig.AppendLine("<connectionStrings>");

            //l.Select(x => sbConfig.AppendLine(x));
            foreach (string s in l)
            {
                sbConfig.AppendLine(s);
            }
            sbConfig.AppendLine("</connectionStrings>");
            Console.WriteLine(sbConfig.ToString());
        }

        
    }

    
}