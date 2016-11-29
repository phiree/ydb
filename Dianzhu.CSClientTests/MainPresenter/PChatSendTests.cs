using NUnit.Framework;
using Dianzhu.CSClient.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.Presenter.Tests
{
    [TestFixture()]
    public class PChatSendTests
    {
        [Test()]
        public void ViewChatSend_SendTextClickTest()
        {
            PChatSend pChat = new PChatSend(null, null, null, null, "111");

            pChat.ViewChatSend_SendTextClick();
        }
    }
}