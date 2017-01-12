using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ydb.Membership.Application;

namespace Ydb.Membership.Tests.Application
{
    [TestFixture()]
    public class UserTokenServiceTests
    {

        IUserTokenService userTokenService;
        string userID = "";
        string token = "";
        string appName = "";
        [SetUp]
        public void SetUp()
        {
            Bootstrap.Boot();
            userTokenService = Bootstrap.Container.Resolve<IUserTokenService>();
            userID = "0404c4f9-05bd-4a27-9203-a674010cfc36";
            token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJsb2dpbk5hbWUiOiIxODg4OTM4NzY3NyIsInBhc3N3b3JkIjoiOThQMGhKRml0OGU2eWUwcHF0cUdiVlZUUFFGODczeUEtOGNkUktvZHBIayIsIlVzZXJUeXBlIjoiY3VzdG9tZXIiLCJVc2VySUQiOiIwNDA0YzRmOS0wNWJkLTRhMjctOTIwMy1hNjc0MDEwY2ZjMzYifQ.3afFkLv6gmUucnq-K-smbtOGLaUwWUqHTyl2EHf8RjQ";
            appName = "UI3f4185e97b3E4a4496594eA3b904d60d";
        }
        [Test()]
        public void UserTokenService_addToken()
        {
            userTokenService.addToken(userID, token, appName);
        }

        [Test()]
        public void UserTokenService_CheckToken()
        {
            Console.WriteLine(userTokenService.CheckToken(token).ToString());
        }

        [Test()]
        public void UserTokenService_DeleteToken()
        {
            userTokenService.DeleteToken(userID);
        }

    }
}
