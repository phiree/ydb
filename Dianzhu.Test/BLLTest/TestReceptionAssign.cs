using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FizzWare.NBuilder;
using Rhino.Mocks;
using Dianzhu.BLL;
using Dianzhu.Model;
namespace Dianzhu.Test.BLLTest
{
    /// <summary>
    /// 为客户分配客服
    /// </summary>
    [TestFixture]
    public class TestReceptionAssign
    {
        private static List<ReceptionStatus> db = new List<ReceptionStatus>();

        private class DbMocker 
        {
            
        }

        [Test]
        public void assign_customer_login()
        {

            //单元测试太复杂 代表代码构建有问题. 需要优化.
            var dalMock = MockRepository.GenerateStub<DAL.DALReceptionStatus>(string.Empty);
            var dalMemberMock = MockRepository.GenerateStub<DAL.DALMembership>(string.Empty);

            BLLReceptionStatus bll = new BLLReceptionStatus(dalMock);
            IList<DZMembership> csList = Builder<DZMembership>.CreateListOfSize(2)
                .TheFirst(1).With(x => x.UserName = "b")
                .TheNext(1).With(x => x.UserName = "a")
                .Build();
            dalMock.Expect(x=>x.DeleteAllCustomerAssign(new DZMembership()));

            dalMock.Expect(x=>x.SaveOrUpdate(new ReceptionStatus()));
            dalMock.Expect(x =>x.Delete(new ReceptionStatus()));
            dalMock.Expect(x =>x.Save(new ReceptionStatus()));
            dalMock.Stub(x => x.GetListByCustomerService(new DZMembership())).Return(
                Builder<ReceptionStatus>.CreateListOfSize(1).Build()
                );
            IIMSession sessionMock = MockRepository.GenerateStrictMock<IIMSession>();
            IList<OnlineUserSession> onlineUsers = Builder<OnlineUserSession>.CreateListOfSize(2)
                .All().With(x=>x.ressource="ydb_cstool")
                .TheFirst(1).With(x=>x.username=Guid.NewGuid().ToString())
                .TheNext(1).With(x => x.username =Guid.NewGuid().ToString())
                .Build();
            dalMemberMock.Stub(x => x.GetOne(new Guid(onlineUsers[0].username))).Return(
                Builder<DZMembership>.CreateNew().With(x=>x.UserName="b"). Build()
                );
            dalMemberMock.Stub(x => x.GetOne(new Guid(onlineUsers[1].username))).Return(
                Builder<DZMembership>.CreateNew().With(z=>z.UserName="a"). Build()
                );
            sessionMock.Stub(x => x.GetOnlineSessionUser()).Return(onlineUsers);
            

            DZMembership customer = Builder<DZMembership>.CreateNew().Build();
            ReceptionAssigner ass = new ReceptionAssigner(
                new AssignStratageRandom(),
                sessionMock,
                dalMock,dalMemberMock);
             
            int forA = 0, forB = 0;
            for (int i = 0; i < 100; i++)
            {  
            Dictionary<DZMembership,DZMembership> assigned = ass.AssignCustomerLogin(customer);
                if (assigned[customer].UserName == "a") forA++;
                if (assigned[customer].UserName == "b") forB++;
        }
            Console.Write("assign to A:" + forA + ",to B:" + forB);
            decimal result = (decimal)forA / (decimal)forB;
            Assert.IsTrue(0.69m < result && 1.44m > result);
           
        }
        public void assign_when_cs_login()
        { 
            //客服登录.增加一条上线信息, 等待分配.
            //new BLLReceptionAssign().add(csname, null);
        }

        
    }
}
