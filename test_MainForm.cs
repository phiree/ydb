using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Dianzhu.CSClient;
using Dianzhu.BLL;
using FizzWare.NBuilder;
namespace Dianzhu.Test
{
    [TestFixture]
    public class test_MainForm
    {
        FormController formController;
        IView view = Builder<IView>.CreateNew().Build();
        DZMembershipProvider bllMember = Builder<DZMembershipProvider>.CreateNew()
                   .Build();

        BLLReception bllReception = Builder<BLLReception>.CreateNew().Build();
        BLLDZService bllDZService = Builder<BLLDZService>.CreateNew().Build();
          
        [SetUp]
        public void Init()
        {
             FormController formController = new FormController(view, bllMember
                , bllReception, bllDZService
            );
        }
        /// <summary>
        /// 接受消息
        /// ui 新增未读按钮 或将 对应按钮设置为未读
        /// 聊天界面如果是当前用户,则增加一条消息
        /// </summary>
        [Test]
        public void test_ReceiveMessage()
        {
            formController.ReceiveMessage("a@a.a", "hello", "/pic.png");
            Assert.AreEqual("hello",view.ChatHistory[0].MessageBody);
        }
    }
}
