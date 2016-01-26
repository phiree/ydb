using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Dianzhu.Push;
namespace Dianzhu.Test.DianzhuPushTest
{
    [TestFixture]
    public class TestPush
    {
        [Test]
        public void TestPushIOS()
        {
            //         Push.pushNotifications("8de76c196a605120db39ab58373edf159c1301b43659bd129fcf72b696e2a26c", "test_push", @"files\aps_development_Mark.p12", 1);

            IPush ipush = new PushIOS("ed26e81b669fd4a1755421255d3c4771d26f7f7e5994e8cf7728f35a0dc7a531", 1, string.Empty);
            ipush.Push("test_push" + DateTime.Now.ToString());
        }
        [Test]
        public void TestPushIOSFromWeb()
        {
            PHSuit.HttpHelper.CreateHttpRequest(
                string.Format("http://localhost:8040/push.ashx?client={0}&pushNum={1}&notificaitonSound={2}&message={3}&deviceToken={4}",
                "ios",1,string.Empty,"i am pusing from web api, "+DateTime.Now.ToString(), "9192b3cc5112899606a2dbc5968ad948213d76ee73bf6ab3b3fa7c13ce0a58dd")
                ,"get",null);
                
        }
    }
}
