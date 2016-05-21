using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model.Finance;
using Dianzhu.BLL.Finance;
using Dianzhu.Model;
using Dianzhu.DAL.Finance;
using Rhino.Mocks;
using NUnit.Framework;
using FizzWare.NBuilder;
namespace Dianzhu.Test.BLLTest.Finance
{
   public class BLLSharePointTest
    {
        /// <summary>
        /// 获取分成比
        /// </summary>
        [Test]
        public void GetSharePoint()
        {
            var dalSharePoint = MockRepository.GenerateMock<DALSharePoint>("");
            var dalDefaultSharePoint = MockRepository.GenerateMock<DALDefaultSharePoint>("");
            BLLSharePoint bllSharepoint = new BLLSharePoint(dalSharePoint, dalDefaultSharePoint);
            DZMembership business = Builder<DZMembership>.CreateNew()
                .With(x => x.UserType == Model.Enums.enum_UserType.business)
                .Build();

            DZMembership cs = Builder<DZMembership>.CreateNew()
               .With(x => x.UserType == Model.Enums.enum_UserType.customerservice)
               .Build();

            bllSharepoint.GetSharePoint(cs);
        }
    }
}
