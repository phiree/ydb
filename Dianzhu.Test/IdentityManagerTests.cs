using NUnit.Framework;
using Dianzhu.CSClient.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.CSClient.Presenter;
using Dianzhu.Model;
using System.Threading;
namespace Dianzhu.CSClient.Presenter.Tests
{
    [TestFixture()]
    public class IdentityManagerTests
    {
        [SetUp]
        public void setup()
        {
            foreach (var i in IdentityManager.CurrentIdentityList)
            {
                IdentityManager.DeleteIdentity(i.Key);
            }
        }
        [Test()]
        public void UpdateIdentityListSingleThreadTest()
        {
            
            DZMembership customer1 = new DZMembership { Id=Guid.NewGuid(),UserName="customer1" };
            DZMembership customer2 = new DZMembership { Id = Guid.NewGuid(), UserName = "customer2" };
            
            for (int i = 0; i < 10; i++)
            {
                DZMembership c = new DZMembership { Id = Guid.NewGuid(), UserName = "customer"+i };
                ServiceOrder order = new ServiceOrder { CustomerId = c, Id = Guid.NewGuid() };
                IdentityTypeOfOrder itoo;
        
                IdentityManager.UpdateIdentityList(order, out itoo);
                IdentityManager.CurrentIdentity = order;
            }
            PrintResult();
        }

        [Test]
        public void UpdateIdentityListMultiThreadTest()
        {

            DZMembership customer1 = new DZMembership { Id = Guid.NewGuid(), UserName = "customer1" };
            DZMembership customer2 = new DZMembership { Id = Guid.NewGuid(), UserName = "customer2" };

            for (int i = 0; i < 1000; i++)
            {
                DZMembership c = new DZMembership { Id = Guid.NewGuid(), UserName = "customer" + i };
                ServiceOrder order = new ServiceOrder { CustomerId = c, Id = Guid.NewGuid() };
 
                System.Threading.Thread t = new System.Threading.Thread(() => AddIdentityAndSetCurrent(order,false));
                t.Start();
                
                
            }
            PrintResult();
        }
        private void PrintResult()
        {
            int trues = 0, falses = 0;
            foreach (var item in IdentityManager.CurrentIdentityList)
            {
                if (item.Value == true) trues += 1;
                else falses += 1;
            }
            Console.WriteLine("true:" + trues + " false:" + falses);
        }
        [Test]
        public void UpdateIdentityListMultiThreadWithLockTest()
        {

            DZMembership customer1 = new DZMembership { Id = Guid.NewGuid(), UserName = "customer1" };
            DZMembership customer2 = new DZMembership { Id = Guid.NewGuid(), UserName = "customer2" };

            for (int i = 0; i < 1000; i++)
            {
                DZMembership c = new DZMembership { Id = Guid.NewGuid(), UserName = "customer" + i };
                ServiceOrder order = new ServiceOrder { CustomerId = c, Id = Guid.NewGuid() };

                /*
                System.Threading.Thread t = new System.Threading.Thread(() => IdentityManager.UpdateIdentityList(order, out itoo));
                t.Start();

                 IdentityManager.UpdateIdentityList(order, out itoo);
                IdentityManager.CurrentIdentity = order;
                */
                System.Threading.Thread t = new System.Threading.Thread(() => AddIdentityAndSetCurrent(order,true));
                
                t.Start();
                 
                
            }
            PrintResult();
        }

        static object o = new object();
        private void AddIdentityAndSetCurrent(ServiceOrder order,bool useLook)
        {
            
            IdentityTypeOfOrder itoo;
            if (useLook)
            {
              
                lock (o)
                {
                    IdentityManager.UpdateIdentityList(order, out itoo);

                    IdentityManager.CurrentIdentity = order;
                }

            }
            else
            {
                IdentityManager.UpdateIdentityList(order, out itoo);

                IdentityManager.CurrentIdentity = order;
            }
        }

        
    }
}