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

namespace Dianzhu.Test.DALTest
{
    [TestFixture]
   public  class DALReceptionChatDDTest
    {
        [TestFixtureSetUp]
        public void setup(){
            //PHSuit.Logging.Config("Dianzhu.Test");
            //Ydb.Common.LoggingConfiguration.Config("mongodb://112.74.198.215/");
            Bootstrap.Boot();
            log4net.LogManager.GetLogger("Dianzhu.Test").Debug("test--");
            log4net.LogManager.GetLogger("NoLog").Debug("test--");
        }
     
 
    }
}
