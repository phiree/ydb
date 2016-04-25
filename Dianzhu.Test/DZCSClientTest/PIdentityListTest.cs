using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using Dianzhu.CSClient.IInstantMessage;
using Dianzhu.CSClient.IView;
using Dianzhu.CSClient.Presenter;
using FizzWare.NBuilder;
using Dianzhu.Model;
namespace Dianzhu.Test.DZCSClientTest
{
    public class PIdentityListTest
    {
        [Test]
        public void IdentityClick_Test()
        {
            var viewCustomerList = MockRepository.GenerateStub<IViewIdentityList>();
            var iIM = MockRepository.GenerateStub<InstantMessage>();
            var viewChatList =  MockRepository.GenerateStub<IViewChatList>();
            viewChatList.ChatList = Builder<ReceptionChat>.CreateListOfSize(1).Build();
            var viewOrder = MockRepository.GenerateStub<IViewOrder>();

            PChatList pChatList = new PChatList(viewChatList, viewCustomerList, new DAL.DALReception(), iIM);

            IList<DZMembership> members = Builder<DZMembership>.CreateListOfSize(3)
                 .TheFirst(1).With(x => x.Id = new Guid("f197a81d-c984-4894-b21c-a5f00106e08b"))
                             .With(x => x.UserName = "user1")
                             .With(x => x.UserType = Model.Enums.enum_UserType.customer)
                  .TheNext(1).With(x => x.Id = new Guid("a8a2fe97-33cc-4602-85ed-a5f001197c72"))
                             .With(x => x.UserName = "user2")
                             .With(x => x.UserType = Model.Enums.enum_UserType.customer)
                 .TheLast(1).With(x => x.Id = new Guid("6ba73c46-83ea-450d-90b2-a5f00101da01"))
                             .With(x => x.UserName = "cs001")
                             .With(x => x.UserType = Model.Enums.enum_UserType.customerservice)
                 .Build();

            ServiceOrder order_user1 = Builder<ServiceOrder>.CreateNew()
                .With(x => x.Customer = members[0])
                .With(x => x.CustomerService = members[2])
                .With(x=>x.Id=new Guid("d9f216b5-92e4-4f7a-87a0-a5f00107b6bc"))
                .Build();

            pChatList.ViewIdentityList_IdentityClick(order_user1);

            ServiceOrder order_user2 = Builder<ServiceOrder>.CreateNew()
               .With(x => x.Customer = members[1])
               .With(x => x.CustomerService = members[2])
               .With(x => x.Id = new Guid("39a6f7ed-f9d8-4782-b75f-a5f001198475"))
               .Build();
            pChatList.ViewIdentityList_IdentityClick(order_user2);
        }

    }
}
