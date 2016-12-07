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
            PushMessage msg = new PushMessage {DisplayContent = "this is a test for user" + DateTime.Now};
            Dianzhu.Push.XMPush.XMPush xmpush = new XMPush(PushType.PushToUser, msg);
           string result=  xmpush.Push(  "18608956891", 0);
            Console.WriteLine(result);

           
        }
        [Test]
        public void PushTesToBusiness()
        {
            PushMessage msg = new PushMessage { DisplayContent = "this is a test for business" + DateTime.Now };

            Dianzhu.Push.XMPush.XMPush xmpushbusiness = new XMPush(PushType.PushToBusiness, msg);
            string result2 = xmpushbusiness.Push(  "290842915@qq.com", 0);
            Console.WriteLine(result2);
        }
    }
}