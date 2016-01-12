using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.DAL;
using NUnit.Framework;
using FizzWare.NBuilder;

namespace Dianzhu.Test.BLLTest
{
    [TestFixture]
    public class BLLReceptionChatTest
    {
        [Test]
        public void FindChatByOrder()
        {

            var dal = new DALReceptionChatDD();

            ServiceOrder order = Builder<ServiceOrder>.CreateNew().Build();
            order.Id = new Guid("4dd3a792-7ba4-4871-ab59-a56700c59f17");
            IList<ReceptionChatDD> chat = dal.GetChatDDListByOrder(order);

            Assert.AreNotEqual(chat.Count, 0);

        }
        [Test]
        public void GetListTest()
        {
            DAL.DALReceptionChatDD dal = new DALReceptionChatDD();
            dal.GetListByFrom(null);
        }
    }
}
