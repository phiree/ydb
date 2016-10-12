using NUnit.Framework;
using Ydb.InstantMessage.DomainModel.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using agsXMPP.protocol.client;
using Ydb.InstantMessage.DomainModel.Chat;
namespace Ydb.InstantMessage.DomainModel.Chat.Tests
{
    [TestFixture()]
    public class MessageAdapterTests
    {
        /*
         
        <message to=""4e2676e1-5561-11e6-b7f0-001a7dda7106@localhost/YDBan_CustomerService"" from=""4d63d740-5561-11e6-b7f0-001a7dda7106@localhost/YDBan_DemoClient"" type=""chat"" id=""d0ae54ba-8be4-4881-b3d7-a3b3d643f4bf""><body>ffffddd</body><active xmlns=""http://jabber.org/protocol/chatstates""/><ext xmlns=""ihelper:chat:text""><orderID>f7f0fdc2-3856-4e25-9b75-a65901436cab</orderID></ext></message>
            string rawXml ="<message to=\"4e2676e1-5561-11e6-b7f0-001a7dda7106@localhost/YDBan_CustomerService\" from=\"4d63d740-5561-11e6-b7f0-001a7dda7106@localhost/YDBan_DemoClient\" type=\"chat\"<body>ffffddd</body><active xmlns=\"http://jabber.org/protocol/chatstates<ext xmlns=\"ihelper:chat:text\"><orderID>f7f0fdc2-3856-4e25-9b75-a65901436cab</orderID></ext></message>";
    string rawXml ="<message to=""4e2676e1-5561-11e6-b7f0-001a7dda7106@localhost/YDBan_CustomerService"" from=""4d63d740-5561-11e6-b7f0-001a7dda7106@localhost/YDBan_DemoClient"" type=""chat""<body>ffffddd</body><active xmlns=""http://jabber.org/protocol/chatstates<ext xmlns=""ihelper:chat:text""><orderID>f7f0fdc2-3856-4e25-9b75-a65901436cab</orderID></ext></message>";
   
             */
        string rawXml = @"<message to=""4e2676e1-5561-11e6-b7f0-001a7dda7106@localhost/YDBan_CustomerService"" from=""4d63d740-5561-11e6-b7f0-001a7dda7106@localhost/YDBan_DemoClient"" type=""chat"" id=""d0ae54ba-8be4-4881-b3d7-a3b3d643f4bf""> <body>ffffddd</body><active xmlns=""http://jabber.org/protocol/chatstates"" /></message>";
        string rawXml2 = @"<message to=""4e2676e1-5561-11e6-b7f0-001a7dda7106@localhost/YDBan_CustomerService""
                                   from=""4d63d740-5561-11e6-b7f0-001a7dda7106@localhost/YDBan_DemoClient""
                                    type=""chat""
                                    id=""d0ae54ba-8be4-4881-b3d7-a3b3d643f4bf"">
                              <body>ffffddd</body>
                              <active xmlns=""http://jabber.org/protocol/chatstates""/>
                              <ext xmlns=""ihelper:chat:text"">
                                 <orderID>f7f0fdc2-3856-4e25-9b75-a65901436cab</orderID>
                              </ext>
                         </message>";
        [Test()]
        public void RawXmlToMessageTest()
        {
            

            MessageAdapter adapter = new MessageAdapter();
         Message m1 = adapter.RawXmlToMessage(rawXml2);
           
            
            Console.WriteLine(m1);
        }
    }
}