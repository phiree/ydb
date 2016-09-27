using NUnit.Framework;
using Dianzhu.Push.XMPush;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.Push.XMPush.Tests
{
    [TestFixture()]
    public class XMPushTests
    {
        [Test()]
        public void PushTesttoUser()
        {
            Dianzhu.Push.XMPush.XMPush xmpush = new XMPush(PushType.PushToUser,string.Empty);
           string result=  xmpush.Push("this is a test for user"+DateTime.Now, "18608956891", 0);
            Console.WriteLine(result);

           
        }
        [Test]
        public void PushTesToBusiness()
        {
            

            Dianzhu.Push.XMPush.XMPush xmpushbusiness = new XMPush(PushType.PushToBusiness, Guid.NewGuid().ToString());
            string result2 = xmpushbusiness.Push("this is a test for business" + DateTime.Now, "290842915@qq.com", 0);
            Console.WriteLine(result2);
        }
    }
}