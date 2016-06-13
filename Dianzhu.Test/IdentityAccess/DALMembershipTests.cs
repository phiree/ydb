using NUnit.Framework;
using Dianzhu.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.DAL;
namespace Dianzhu.DAL.Tests
{
    [TestFixture()]
    public class DALMembershipTests
    {
        [Test()]
        public void GetMemberByWechatOpenIdTest()
        {
            DAL.DALMembership dal = new DALMembership();
            dal.GetMemberByWechatOpenId("aa");
        }
    }
}