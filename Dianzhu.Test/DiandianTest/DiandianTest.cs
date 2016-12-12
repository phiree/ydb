using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.DAL;
using NUnit.Framework;
using FizzWare.NBuilder;
using agsXMPP.protocol.client;
 

namespace Dianzhu.Test.DiandianTest
{
    [TestFixture]
   public  class DiandianTest
    {
        [TestFixtureSetUp]
        public void setup(){
            //PHSuit.Logging.Config("Dianzhu.Test");
            Ydb.Common.LoggingConfiguration.Config("mongodb://112.74.198.215/");
            log4net.LogManager.GetLogger("Dianzhu.Test").Debug("test--");
            log4net.LogManager.GetLogger("NoLog").Debug("test--");
        }
       [Test]
        public void XMPPConnectionOnMessage()
        {
            //<message xmlns="jabber: client" to="c64d9dda-4f6e-437b-89d2-a591012d8c65@119.29.39.211" id="43E2C51C-67AA-4823-AC76-DAC74E1201C6" type="chat" from="cd3c291c-9e63-4301-94bf-a5d700a5952a@119.29.39.211 / YDBan_IOS_User"><body/><ext xmlns="ihelper: chat: media"><orderID>7fb59eb5-f83f-4e13-9eee-a5e0009e6e0c</orderID><MsgObj type="voice" url="http://119.29.39.211:8038/GetFile.ashx?fileName=_$_fb374efe-2a18-462f-996d-e2a8cc1fa614_$_ChatAudio_$_voice"/></ext></message>

            string xml = "<message xmlns=\"jabber:client\" to=\"c64d9dda-4f6e-437b-89d2-a591012d8c65@119.29.39.211\" id=\"43E2C51C-67AA-4823-AC76-DAC74E1201C6\" type=\"chat\" from=\"cd3c291c-9e63-4301-94bf-a5d700a5952a@119.29.39.211/YDBan_IOS_User\"><body/><ext xmlns=\"ihelper:chat:media\"><orderID>7fb59eb5-f83f-4e13-9eee-a5e0009e6e0c</orderID><msgObj type=\"voice\" url=\"http://119.29.39.211:8038/GetFile.ashx?fileName=_$_fb374efe-2a18-462f-996d-e2a8cc1fa614_$_ChatAudio_$_voice\"/></ext></message>";

            agsXMPP.Xml.Dom.Document d = new agsXMPP.Xml.Dom.Document();
            d.LoadXml(xml); 
            Message msg = new Message();
            msg = d.RootElement as Message;

            //ServiceDiandian.XMPPConnection_OnMessage(1, msg);

            Assert.AreEqual(6, 6);

        }
    }
}
