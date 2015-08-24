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
            DZMembershipProvider bllMember = Builder<DZMembershipProvider>.CreateNew()
                     .Build();

            BLLReception bllReception = Builder<BLLReception>.CreateNew().Build();
            BLLDZService bllDZService = Builder<BLLDZService>.CreateNew().Build();
            MainFormView view = Builder<MainFormView>.CreateNew().Build();
            FormController formController = new FormController(view, bllMember
                 , bllReception, bllDZService);
            formController.ReceiveMessage("a@a.a", "hello", "/pic.png");
            string result = view.ToString();
            Assert.AreEqual("hello", result);
        }
    }
}
