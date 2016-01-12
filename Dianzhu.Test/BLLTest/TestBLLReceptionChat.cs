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
   public  class BLLReceptionChatTest
    {
       [Test]
        public void FindChatByOrder()
        {

            var dal = new DALReceptionChatDD();

            ServiceOrder order = Builder<ServiceOrder>.CreateNew().Build();
            order.Id = new Guid("81d6c8d5-0562-4075-96a9-a56700a86aaf");
            //IList<ReceptionChatDD> chat = dal.GetChatDDListByOrder(order);
            IList<ReceptionChatDD> chat = null;

            Assert.AreNotEqual(chat.Count, 0);

        }
    }
}
