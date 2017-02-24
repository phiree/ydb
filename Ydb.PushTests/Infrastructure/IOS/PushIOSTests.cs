using NUnit.Framework;

namespace Ydb.Push.Tests
{
    [TestFixture]
    public class PushIOSTests
    {
        [SetUp]
        public void Setup()
        {
           // Ydb.Common.LoggingConfiguration.Config("PushTests");
        }

        [Test]
        public void PushTest()
        {
            var pushIos = new PushIOS();
            pushIos.Push(PushTargetClient.PushToUser,
                new PushMessage
                {
                    DisplayContent = "message",
                    OrderId = "12492362-2f92-476f-b956-a71500a04e23",

                    OrderSerialNo = "FW11111"
                }
                , "4eb8954fda72c9bb8d0c0f5f85d99ac0b324c84836806c53d0fe43352f047aa1", 1
                );
        }
    }
}