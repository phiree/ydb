using NUnit.Framework;
using Dianzhu.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Mocks;
using Dianzhu.Model;
using FizzWare.NBuilder;
using System.Threading;
using Ydb.Membership.DomainModel;
using Ydb.Membership.DomainModel.Enums;

namespace Dianzhu.DAL.Tests
{
    [TestFixture]
    public class DALServiceOrderTests
    {
        BLL.IBLLServiceOrder bllServiceOrder;
        IList<DZMembership> members;
        ServiceOrder order;
        [SetUp]
        public void Init()
        {
            Bootstrap.Boot();
            //NHibernateUnitOfWork.UnitOfWork.Start();


            PHSuit.Logging.Config("Dianzhu.DAL.Tests.DALServiceOrderTests");

            //bllServiceOrder = Bootstrap.Container.Resolve<BLL.IBLLServiceOrder>();
            

            members = Builder<DZMembership>.CreateListOfSize(3)
                 .TheFirst(1)
                 //.With(x => x.Id = new Guid("f197a81d-c984-4894-b21c-a5f00106e08b"))
                             .With(x => x.UserName = "user1")
                             .With(x => x.UserType = UserType.customer)
                  .TheNext(1)
                  //.With(x => x.Id = new Guid("a8a2fe97-33cc-4602-85ed-a5f001197c72"))
                             .With(x => x.UserName = "user2")
                             .With(x => x.UserType = UserType.customer)
                 .TheLast(1)
                 //.With(x => x.Id = new Guid("6ba73c46-83ea-450d-90b2-a5f00101da01"))
                             .With(x => x.UserName = "cs001")
                             .With(x => x.UserType = UserType.customerservice)
                 .Build();

            //order = bllServiceOrder.GetOne(new Guid("16ae7044-6522-49a8-abf2-a68100f578a6"));
        }
        [Test]
        public void GetOrderListOfServiceByDateRangeTest()
        {
            DALServiceOrder dal = new DALServiceOrder();
            Action ac = () => {
                 var list=  dal.GetOrderListOfServiceByDateRange(new Guid("9ab2f087-2f15-4c45-86a4-a6510094dfda"), new DateTime(2016, 7, 30), new DateTime(2016, 7, 30));
                //dal.GetOne(Guid.NewGuid());
            };
            NHibernateUnitOfWork.With.Transaction(ac);
        }

        [Test]
        public void ChangeStatusTest()
        {
            //IList<DZMembership> members = Builder<DZMembership>.CreateListOfSize(3)
            //     .TheFirst(1).With(x => x.Id = new Guid("f197a81d-c984-4894-b21c-a5f00106e08b"))
            //                 .With(x => x.UserName = "user1")
            //                 .With(x => x.UserType = Model.Enums.enum_UserType.customer)
            //      .TheNext(1).With(x => x.Id = new Guid("a8a2fe97-33cc-4602-85ed-a5f001197c72"))
            //                 .With(x => x.UserName = "user2")
            //                 .With(x => x.UserType = Model.Enums.enum_UserType.customer)
            //     .TheLast(1).With(x => x.Id = new Guid("6ba73c46-83ea-450d-90b2-a5f00101da01"))
            //                 .With(x => x.UserName = "cs001")
            //                 .With(x => x.UserType = Model.Enums.enum_UserType.customerservice)
            //     .Build();

            //ServiceOrder order= Builder<ServiceOrder>.CreateNew()
            //    .With(x => x.Customer = members[0])
            //    .With(x => x.CustomerService = members[2])
            //    .With(x => x.OrderStatus =  Model.Enums.enum_OrderStatus.Created)
            //    .With(x => x.Id = new Guid("d9f216b5-92e4-4f7a-87a0-a5f00107b6bc"))
            //    .Build();


            ThreadStart threadStart1 = new ThreadStart(Calculate2);
            Thread thread1 = new Thread(threadStart1);
            thread1.Start();
            //Console.WriteLine("OrderFlow_PayDepositAndWaiting:" + order.OrderStatus);
            //Assert.AreEqual(order.OrderStatus, Model.Enums.enum_OrderStatus.checkPayWithDeposit);


            ThreadStart threadStart2 = new ThreadStart(Calculate1);
            Thread thread2 = new Thread(threadStart2);
            thread2.Start();
            //Console.WriteLine("OrderFlow_ConfirmDeposit:" + order.OrderStatus);
            //Assert.AreEqual(order.OrderStatus, Model.Enums.enum_OrderStatus.Payed);


            //bllServiceOrder.OrderFlow_PayDepositAndWaiting(order);


            //bllServiceOrder.OrderFlow_ConfirmDeposit(order);
        }

        [Test]
        public void Calculate1()
        {
            NHibernateUnitOfWork.UnitOfWork.Start();
            BLL.IBLLServiceOrder bllServiceOrder = Bootstrap.Container.Resolve<BLL.IBLLServiceOrder>();
            ServiceOrder order = bllServiceOrder.GetOne(new Guid("16ae7044-6522-49a8-abf2-a68100f578a6"));
            bllServiceOrder.OrderFlow_PayDepositAndWaiting(order);
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
        }
        [Test]
        public void Calculate2()
        {
            NHibernateUnitOfWork.UnitOfWork.Start();
            BLL.IBLLServiceOrder bllServiceOrder = Bootstrap.Container.Resolve<BLL.IBLLServiceOrder>();
            ServiceOrder order = bllServiceOrder.GetOne(new Guid("16ae7044-6522-49a8-abf2-a68100f578a6"));
            bllServiceOrder.OrderFlow_ConfirmDeposit(order);
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
        }

        [TearDown]
        public void End()
        {
            //NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            //NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
        }
    }
}