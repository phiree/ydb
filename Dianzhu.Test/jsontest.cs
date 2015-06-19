using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
namespace Dianzhu.Test
{
    [TestFixture]
    public class jsontest
    {
        [Test]
        public void jsonDeserializeTest()
        {
           string json = "{'Name':'袁飞','Age':18}";
           Employee e = JsonConvert.DeserializeObject<Employee>(json);
           Assert.AreEqual(e.Name, "袁飞");
           Assert.AreEqual(e.Age, 18);
        }
     
    }
    public class Employee
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
