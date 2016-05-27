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
        }
        public void BatchCSClientLogin()
        { }
        [Test]
        public void OpenfireLogin()
        {
            LoginPresenter loginPresenter = Bootstraper.Bootstrap.Container.Resolve<LoginPresenter>();
            loginPresenter.loginView_ViewLogin();
        }
    }
}
