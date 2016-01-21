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

namespace Dianzhu.Test.DALTest
{
    [TestFixture]
   public  class DALReceptionChatDDTest
    {
       [Test]
        public void GetChatDDListByOrder()
        {

            var dal = new DALReceptionChatDD();

            IList<DZMembership> logoffCList = new List<DZMembership>();

            int num = 2;

            IList<ServiceOrder> orderCList = dal.GetCustomListDistinctFrom(num);
            if (orderCList.Count > 0)
            {
                foreach (ServiceOrder order in orderCList)
                {
                    if (!logoffCList.Contains(order.Customer))
                    {
                        logoffCList.Add(order.Customer);
                    }
                }
            }

            IList<ReceptionChatDD> chatList = dal.GetChatDDListByOrder(logoffCList);

            Assert.AreEqual(chatList.Count, 6);

        }

        [Test]
        public void GetCustomListDistinctFrom()
        {

            var dal = new DALReceptionChatDD();

            int num = 6;

            IList<ServiceOrder> chatList = dal.GetCustomListDistinctFrom(num);

            Assert.AreEqual(chatList.Count, 5);

        }
    }
}
