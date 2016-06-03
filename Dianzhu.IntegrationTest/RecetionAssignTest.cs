using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Dianzhu.CSClient.Presenter;
using Dianzhu.CSClient.IView;
namespace Dianzhu.IntegrationTest
{
    [TestFixture]
    public class RecetionAssignTest
    {
        [SetUp]
        public void Setup()
        {
            Bootstraper.Bootstrap.Boot();
            PHSuit.Logging.Config("abcdefa");
        }
        public void BatchCSClientLogin()
        { }
        [Test]
        public void OpenfireLogin()
        {
            LoginPresenter loginPresenter = Bootstraper.Bootstrap.Container.Resolve<LoginPresenter>();

            loginPresenter.Login("aa@aa.aa","123456");

            loginPresenter.Login("f@f.f", "123456");

             
        }
    }
}
