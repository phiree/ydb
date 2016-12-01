using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

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
        public void jsonFileDeserializeTest()
        {
            string con_file_path = Environment.CurrentDirectory;
            using (StreamReader sr = new StreamReader(con_file_path))
            {
                try
                {
                    //JsonSerializer serializer = new JsonSerializer();
                    //serializer.Converters.Add(new JavaScriptDateTimeConverter());
                    //serializer.NullValueHandling = NullValueHandling.Ignore;

                    //构建Json.net的读取流  
                    JsonReader reader = new JsonTextReader(sr);
                    //对读取出的Json.net的reader流进行反序列化，并装载到模型中  
                    //cfm = serializer.Deserialize<ConfigFileModel>(reader);

                    //Response.Write("<br/>");
                    //Response.Write(cfm.FileName + ", " + cfm.CreateDate);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }

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
        [Test]
        public void test_xmlSerialize()
        {
            string xml = @"<Employee>
                        <Name>Alan</Name>
                        <Age>18</Age>
                        <Department>
<DepartmentName>dep1</DepartmentName>
</Department>
                            </Employee>";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            string json = string.Empty;
            DateTime begin1 = DateTime.Now;
            for(int i = 0; i < 100000; i++) {
                 json = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);
                Employee em = JsonConvert.DeserializeObject<Employee>(json);
            }
            DateTime end1 = DateTime.Now;
            Console.WriteLine((end1-begin1).TotalMilliseconds);
            //Assert.AreEqual("18", em.Age);
            
            XmlSerializer serializer = new XmlSerializer(typeof(Employee));
            MemoryStream ms = new MemoryStream();
            doc.Save(ms);
            ms.Flush();
            ms.Position = 0;
            DateTime begin2 = DateTime.Now;
            for (int i = 0; i < 100000; i++)
            {
                var em2 =  serializer.Deserialize(ms);
                ms.Position = 0;
            }
            DateTime end2 = DateTime.Now;
            Console.WriteLine((end2 - begin2).TotalMilliseconds);
            //Assert.AreEqual("18", em2.Age);
        }

        public class Employee
        {
            public Employee()
            {
                Age = "18";
            }
            public string Name { get; set; }
            public string Age { get; set; }
            public Department Department { get; set; }
        }
        public class Department
        {
            public string DepartmentName { get; set; }

        }
     
    }
   
}
