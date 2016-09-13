using NUnit.Framework;
using Dianzhu.CSClient.MessageAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.CSClient;
using Dianzhu.Model;
namespace Dianzhu.CSClient.MessageAdapter.Tests
{
    [TestFixture()]
    public class MessageAdapterTests
    {
        [SetUp]
        public void Setup()
        {
           
        }
        [Test()]
        public void RawXmlToChatTest_TextChat()
        {
            string rawXml = "<message xmlns=\"jabber:client\" to=\"4e2676e1-5561-11e6-b7f0-001a7dda7106@localhost/YDBan_CustomerService\" id=\"eb6ecbc6-6979-42b8-b449-a1b7c7ae5ce0\" from=\"272be8b3-100c-423c-83e2-a63d012dd455@localhost/YDBan_DemoClient\"><body>97</body><active xmlns=\"http://jabber.org/protocol/chatstates\" /><ext xmlns=\"ihelper:chat:text\"><orderID>bed76b1b-fe4a-4a90-abbd-a65b0129b1d9</orderID></ext></message>";
            MessageAdapter ma = new MessageAdapter();

            ReceptionChat chat = ma.RawXmlToChat(rawXml);
            Assert.AreEqual(chat.ToId, "4e2676e1-5561-11e6-b7f0-001a7dda7106");
        }
        [Test()]
        public void RawXmlToChatTest_Media()
        {
            /*
             <message xmlns=""jabber:client"" from=""05a8aefd-6a9b-11e6-b78a-001a7dda7106@localhost/YDBan_User"" to=""05ad9bf7-6a9b-11e6-b78a-001a7dda7106@localhost"" type=""chat"">
  <body />
  <active xmlns=""http://jabber.org/protocol/chatstates"" />
  <ext xmlns=""ihelper:chat:media"">
    <orderID>cfe31a94-83a3-45fc-a468-a595009e0e4a</orderID>
    <msgObj type=""image"" url=""http://localhost:8038/GetFile.ashx?fileName=_$_85f591ba-ebcc-41ab-b093-3ab31ca56609_$_ChatImage_$_image"" />
  </ext>
</message>
             */
            string rawXml = @"<message xmlns=""jabber:client"" from=""05a8aefd-6a9b-11e6-b78a-001a7dda7106@localhost/YDBan_User"" to=""05ad9bf7-6a9b-11e6-b78a-001a7dda7106@localhost"" type=""chat"">
  <body />
  <active xmlns=""http://jabber.org/protocol/chatstates"" />
  <ext xmlns=""ihelper:chat:media"">
    <orderID>cfe31a94-83a3-45fc-a468-a595009e0e4a</orderID>
    <msgObj type=""image"" url=""http://localhost:8038/GetFile.ashx?fileName=_$_85f591ba-ebcc-41ab-b093-3ab31ca56609_$_ChatImage_$_image"" />
  </ext>
</message>";
            MessageAdapter ma = new MessageAdapter();

            ReceptionChatMedia chat =(ReceptionChatMedia) ma.RawXmlToChat(rawXml);
            Assert.AreEqual(chat.ToId, "4e2676e1-5561-11e6-b7f0-001a7dda7106");
            Assert.AreEqual("http://localhost:8038/GetFile.ashx?fileName=_$_85f591ba-ebcc-41ab-b093-3ab31ca56609_$_ChatImage_$_image", chat.MedialUrl);
            Assert.AreEqual("image", chat.MediaType);
            Assert.AreEqual(string.Empty, chat.MessageBody);
             
        }
        [Test()]
        public void RawXmlToChatTest_PushService()
        {
            /*
             <message xmlns="jabber:client" type="chat" 
             id="e81d4056-4481-4742-8725-834629eef034"
             to="0e3e9327-7b82-407c-a92f-a64300fd03db@192.168.1.172/YDBan_DemoClient">
                <body />
                <active xmlns="http://jabber.org/protocol/chatstates" />
                <ext xmlns="ihelper:chat:orderobj">
                    <orderID>9194b79b-e2f3-4c61-9267-a667010b9843</orderID>
                    <svcObj svcID="6b9d84de-4dd4-48ce-b93b-a66700fa7ba4" name="弹弹00" type="弹棉花&gt;弹棉花" startTime="20160830090000" />
                    <storeObj userID="258efa6a-e8e3-4bc5-8b24-a66700fa3c5e" alias="测试店铺1" imgUrl="" />
                    </ext>
              </message>

            <message xmlns=""jabber:client"" type=""chat"" 
             id=""e81d4056-4481-4742-8725-834629eef034""
             to=""0e3e9327-7b82-407c-a92f-a64300fd03db@192.168.1.172/YDBan_DemoClient"">
                <body />
                <active xmlns=""http://jabber.org/protocol/chatstates"" />
                <ext xmlns=""ihelper:chat:orderobj"">
                    <orderID>9194b79b-e2f3-4c61-9267-a667010b9843</orderID>
                    <svcObj svcID=""6b9d84de-4dd4-48ce-b93b-a66700fa7ba4"" name=""弹弹00"" type=""弹棉花&gt;弹棉花"" startTime=""20160830090000"" />
                    <storeObj userID=""258efa6a-e8e3-4bc5-8b24-a66700fa3c5e"" alias=""测试店铺1"" imgUrl="""" />
                    </ext>
              </message>
             */
            string rawXml = @"<message xmlns=""jabber:client"" type=""chat"" 
                             id=""e81d4056-4481-4742-8725-834629eef034""
                             to=""4e2676e1-5561-11e6-b7f0-001a7dda7106@192.168.1.172/YDBan_DemoClient""
                             from=""272be8b3-100c-423c-83e2-a63d012dd455@192.168.1.172/YDBan_DemoClient"">
                                <body />
                                <active xmlns=""http://jabber.org/protocol/chatstates"" />
                                <ext xmlns=""ihelper:chat:orderobj"">
                                    <orderID>04f32cf5-6f2f-4186-bda3-a652010b2609</orderID>
                                    <svcObj svcID=""910808d8-e76a-4a51-812b-a64100978abb"" name=""弹弹00"" type=""弹棉花&gt;弹棉花"" startTime=""20160830090000"" />
                                    <storeObj userID=""258efa6a-e8e3-4bc5-8b24-a66700fa3c5e"" alias=""测试店铺1"" imgUrl="""" />
                                    </ext>
                              </message>";
            IMessageAdapter.IAdapter ma = Bootstrap.Container.Resolve<Dianzhu.CSClient.IMessageAdapter.IAdapter>();
            NHibernateUnitOfWork.UnitOfWork.Start();
            ReceptionChat chat = ma.RawXmlToChat(rawXml);
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
            NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
            Assert.AreEqual(typeof(ReceptionChatPushService), chat.GetType());


        }

        [Test()]
        public void RawXmlToChatTestNoticeOrder()
        {
            /*
             <message xmlns="jabber:client" to="4e2676e1-5561-11e6-b7f0-001a7dda7106@localhost" 
             id="fb2afd35-1627-425d-a761-4586e637b150" type="headline" from="fa7ef456-0978-4ccd-b664-a594014cbfe7@localhost/YDBan_IMServer">
                 <body>订单状态已变为:Created</body>
                 <active xmlns="http://jabber.org/protocol/chatstates" />
                <ext xmlns="ihelper:notice:order">
                    <orderID>231cdac7-0c1f-4e96-8553-a6600120a6bd</orderID>
                    <orderObj type="动漫设计" status="Created" title="畅畅设计;" />
                </ext>
                </message> 
             */
            string rawXml = @"<message xmlns=""jabber:client"" to=""4e2676e1-5561-11e6-b7f0-001a7dda7106@localhost"" 
             id=""fb2afd35-1627-425d-a761-4586e637b150"" type=""headline"" from=""fa7ef456-0978-4ccd-b664-a594014cbfe7@localhost/YDBan_IMServer"">
                 <body>订单状态已变为:Created</body>
                 <active xmlns=""http://jabber.org/protocol/chatstates"" />
                <ext xmlns=""ihelper:notice:order"">
                    <orderID>231cdac7-0c1f-4e96-8553-a6600120a6bd</orderID>
                    <orderObj type=""动漫设计"" status=""Created"" title=""畅畅设计;"" />
                </ext>
                </message>";
            IMessageAdapter.IAdapter ma = Bootstrap.Container.Resolve<Dianzhu.CSClient.IMessageAdapter.IAdapter>();
        
            ReceptionChat chat = ma.RawXmlToChat(rawXml);
       
            Assert.AreEqual(typeof(ReceptionChatNoticeOrder), chat.GetType());


        }

    
    }
}