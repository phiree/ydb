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
           Assert.AreEqual(e.Age, "18");
        }
        [Test]
        public void test_ignore_null_property()
        {
            Employee employee = new Employee { Name = "phiree"};

            string json = JsonConvert.SerializeObject(employee, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            Console.WriteLine(json);

        }
        [Test]
        public void test_serialize_none_property()
        {
            string json = "{'Name':'phiree',  'Department':{'DepartmentName':'dep1'}}";
            var obj = JsonConvert.DeserializeObject<Employee>(json);
            Assert.AreEqual("18", obj.Age);
            Assert.AreEqual("dep1", obj.Department.DepartmentName);

        }

        private class Employee
        {
            public Employee()
            {
                Age = "18";
            }
            public string Name { get; set; }
            public string Age { get; set; }
            public Department Department { get; set; }
        }
        private class Department
        {
            public string DepartmentName { get; set; }

        }
     
    }
   
}
