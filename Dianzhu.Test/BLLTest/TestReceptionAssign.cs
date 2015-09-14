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


            var dalMock = MockRepository.GenerateStub<DAL.DALReceptionStatus>(string.Empty);
            BLLReceptionStatus bll = new BLLReceptionStatus(dalMock);
            IList<DZMembership> csList = Builder<DZMembership>.CreateListOfSize(2)
                .TheFirst(1) .With(x=>x.UserName="b")
                .TheNext(1).With(x=>x.UserName="a")
                .Build();
            dalMock.Stub(x => x.GetAll<ReceptionStatus>()).Return(Builder<ReceptionStatus>.CreateListOfSize(2)
                .TheFirst(1).With(x=>x.CustomerService=csList[0])
                .TheNext(1).With(x=>x.CustomerService=csList[1])
                .Build());

            DZMembership customer = Builder<DZMembership>.CreateNew().Build();
            ReceptionAssigner ass = new ReceptionAssigner(new AssignStratageRandom());
            ass.dalRS = dalMock;
            DZMembership customerService = bll.Assign(customer,null);
            Console.WriteLine(customerService.UserName);
            Assert.Contains(customerService.UserName,new string[]{"a","b"});
        }
        public void assign_when_cs_login()
        { 
            //客服登录.增加一条上线信息, 等待分配.
            //new BLLReceptionAssign().add(csname, null);
        }
        
    }
}
