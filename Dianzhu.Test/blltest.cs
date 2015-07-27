using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.BLL;
using Dianzhu.Model;
using NUnit.Framework;
namespace Dianzhu.Test
{
    [TestFixture]
    public class blltest
    {
        [Test]
        public void test_send_email_validate()
        {
            DZMembershipProvider dp = new DZMembershipProvider();
            dp.DALMembership = null;
            dp.SendValidationMail();
        }
    }
}
