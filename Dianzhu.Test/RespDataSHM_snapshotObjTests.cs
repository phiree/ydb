using NUnit.Framework;
using Dianzhu.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using Newtonsoft;
using Newtonsoft.Json;
using Ploeh.AutoFixture;
namespace Dianzhu.Api.Model.Tests
{
    [TestFixture()]
    public class RespDataSHM_snapshotObjTests
    {
        [Test()]
        public void RespDataSHM_snapshotObjTest()
        {
            var fix = new Ploeh.AutoFixture.Fixture();
            fix.Behaviors.Add(new OmitOnRecursionBehavior());

         //   var resp = fix.Create<RespDataSHM_snapshotObj>(); //Ploeh.AutoFixture.Fixture  Builder<RespDataSHM_snapshotObj>.CreateNew().Build();
         //var jsonStr=            JsonConvert.SerializeObject(resp);
         //   Console.WriteLine(jsonStr);
        }
    }
}