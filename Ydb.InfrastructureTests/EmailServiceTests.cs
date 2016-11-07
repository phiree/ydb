using NUnit.Framework;
using Ydb.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Ydb.Infrastructure.Tests
{
    [TestFixture()]
    public class EmailServiceTests
    {

        [Test()]
        public void SendEmailTest()
        {
            EmailService emailService = new EmailService();
            string emails = string.Empty;
            string[] emailList = new string[] {"aasdf@dsaf.com" };

            Assert.Throws(typeof(ArgumentNullException), () => emailService.SendEmail(emailList[0], "test", "testbody", emailList));

        }
    }
}