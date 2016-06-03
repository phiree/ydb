using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.BLL;
using Dianzhu.CSClient;
using Dianzhu.Model;
using NUnit.Framework;
using Rhino.Mocks;
using FizzWare.NBuilder;
using Dianzhu.CSClient.IView;
using Dianzhu.CSClient.Presenter;
using Dianzhu.CSClient.IInstantMessage;
namespace Dianzhu.Test
{
    [TestFixture]
    public class test_mainform
    {
        
       
       
        
        public void Init()
        {
           
       
        }
        [Test]
        public void sdfsdfsd()
        {
            DAL.DALMembership dalmember = Builder<DAL.DALMembership>.CreateNew().Build();
            DAL.DALReception DALReception = Builder<DAL.DALReception>.CreateNew().Build();
            DAL.DALDZService DALDZService = Builder<DAL.DALDZService>.CreateNew().Build();
            DZMembershipProvider bllMember = Builder<DZMembershipProvider>.CreateNew().Build();

            BLLReception bllReception = Builder<BLLReception>.CreateNew()
             .With(x => x.DALReception = DALReception).Build();

            BLLDZService bllDZService = Builder<BLLDZService>.CreateNew().
                With(x => x.DALDZService = DALDZService).Build();
           // IMainFormView view = MockRepository.GenerateStub<IMainFormView>();
            InstantMessage xmpp = MockRepository.GenerateStub<InstantMessage>();

            Dianzhu.CSClient.IMessageAdapter.IAdapter adapter = MockRepository.GenerateStub<Dianzhu.CSClient.IMessageAdapter.IAdapter>();

            //MainPresenter formController = new MainPresenter(
            //    view, 
            //    xmpp,
            //    adapter,
            //    bllMember
            //     , bllReception, 
            //     bllDZService,
            //     Builder<BLLServiceOrder>.CreateNew().Build()
            //     ,MockRepository.GenerateMock<BLLReceptionStatus>()
            //     );
           // formController.ReceiveMessage("a@a.a", "hello", "/pic.png",string.Empty);
           // string result = view.ToString();
           // Assert.AreEqual("hello", result);
        }
    }
}
