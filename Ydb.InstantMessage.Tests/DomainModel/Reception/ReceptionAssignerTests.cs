using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ydb.InstantMessage.DomainModel.Reception;
using Ydb.InstantMessage;
using FizzWare.NBuilder;
using Rhino.Mocks;

namespace Ydb.InstantMessage.DomainModel.Reception.Tests
{
    [TestFixture()]
    public class ReceptionAssignTest
    {
        [SetUp]
        public void SetUp()
        {

        }
        #region AssignCustomerServiceToCustomer
        /*匹配列表中有一个:{在线,离线}
         匹配列表中有多个;
         没有匹配:{只有点点:还有其他客服}

                     */
        [Test]
        public void AssignCustomerServiceToCustomer_OneExistedReception()
        {
            string customerId = Guid.NewGuid().ToString();
            string customerServiceId = Guid.NewGuid().ToString();
            ReceptionStatus rs = Builder<ReceptionStatus>.CreateNew()
                .With(x => x.CustomerId = customerId)
                .With(x => x.CustomerServiceId = customerServiceId)
                .Build();
            IList<ReceptionStatus> existedReception = new List<ReceptionStatus> { rs };
            IReceptionSession iSession = MockRepository.Mock<IReceptionSession>();
            iSession.Stub(x => x.IsUserOnline(customerServiceId)).Return(true);

            ReceptionAssigner Assigner = new ReceptionAssigner(iSession, new AssignStratageRandom());
            string assignedCustomerServiceId = Assigner.AssignCustomerServiceToCustomer(existedReception, customerId);
            Console.WriteLine(assignedCustomerServiceId);
            Assert.AreEqual(customerServiceId, assignedCustomerServiceId);
        }
        [Test]
        public void AssignCustomerServiceToCustomer_NotExistedReception()
        {
            string customerId = Guid.NewGuid().ToString();
            ReceptionStatus rs = Builder<ReceptionStatus>.CreateNew().With(x => x.CustomerId = customerId).Build();
            IList<ReceptionStatus> existedReception = Builder<ReceptionStatus>.CreateListOfSize(4).Build();
            existedReception.Add(rs);

            IReceptionSession iSession = MockRepository.Mock<IReceptionSession>();
            iSession.Stub(x => x.IsUserOnline(customerId)).Return(true);

            ReceptionAssigner Assigner = new ReceptionAssigner(iSession, new AssignStratageRandom());
            string assignedCustomerServiceId = Assigner.AssignCustomerServiceToCustomer(existedReception, customerId);
            Console.WriteLine(assignedCustomerServiceId);
        }
        #endregion
    }
}