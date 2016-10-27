using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FizzWare.NBuilder;
using Dianzhu.Model;
using Ydb.InstantMessage.Application;
using Ydb.InstantMessage.DomainModel.Chat;

namespace Dianzhu.Test.DianzhuNotifyCenterTest
{
    [TestFixture]
    public class OrderNotifyTest
    {
        
        static IInstantMessage imInstance = null;
        [SetUp]
        public void setup()
        {
            if (imInstance == null)
            {

                //IMessageAdapter adapter = new MessageAdapter();
                //imInstance = new XMPP("","",adapter);
                //imInstance.OpenConnection("4f088d5c-be94-43bc-9644-a4d1008be129", "123456");
                //imInstance.IMLogined += ImInstance_IMLogined;
            }
            else
            {
                logined = true;
            }

        }
       static bool logined = false;
        private void ImInstance_IMLogined(string jidUser)
        {
            logined = true;
        }

        [Test]
        public void SendOrderChangedTeset()
        {
            while(!logined)
            {
                System.Threading.Thread.Sleep(3000);

            }
            //Dianzhu.NotifyCenter.IMNotify on = Bootstrap.Container.Resolve<Dianzhu.NotifyCenter.IMNotify>();
            //ServiceOrder order = Builder<ServiceOrder>.CreateNew()
            //    .With(x=>x.Customer=Builder<DZMembership>.CreateNew().With(y=>y.Id=new Guid("1cd5ac25-fcc6-432d-bba0-a4f90129edcf")).Build())
            //    .With(x => x.CustomerService = Builder<DZMembership>.CreateNew().With(z => z.Id = new Guid("d53147d9-1a1e-4df8-b4d0-a4f90129ad25")).Build())
            // //  .With(x=>x.Id=new Guid("a5edb351-ae48-4f76-8b0c-a53200c1a7d7"))
            //    .Build();
          
            //on.SendOrderChangedNotify(order);

        }
    }
}
