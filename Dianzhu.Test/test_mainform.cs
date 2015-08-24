using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.BLL;
using Dianzhu.CSClient;
using Dianzhu.Model;
using NUnit.Framework;
using FizzWare.NBuilder;
using Dianzhu.CSClient.IVew;
using Dianzhu.CSClient.Presenter;
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
            DZMembershipProvider bllMember = Builder<DZMembershipProvider>.CreateNew()
                .With(x=>x.DALMembership=dalmember)
                     .Build();

            BLLReception bllReception = Builder<BLLReception>.CreateNew()
             .With(x => x.DALReception = DALReception).Build();

            BLLDZService bllDZService = Builder<BLLDZService>.CreateNew().
                With(x => x.DALDZService = DALDZService).Build();
            MainFormView view = Builder<MainFormView>.CreateNew().Build();
            FormController formController = new FormController(
                view, 
                bllMember
                 , bllReception, 
                 bllDZService,
                 Builder<DZMembership>.CreateNew().Build());
            formController.ReceiveMessage("a@a.a", "hello", "/pic.png");
            string result = view.ToString();
            Assert.AreEqual("hello", result);
        }
    }
}
