using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Dianzhu.Test
{
    [TestFixture]
public     class form
    {
        /// <summary>
        /// 测试 Take<> 方法 是否会clone对象.
        /// </summary>
        [Test]
        public void update_item_from_take()
        {
            List<a> list = new List<a>();
            list.Add(new a {id=1,name="name1"});

            var a_taken = list.Take<a>(1).ToList();
            a_taken[0].name = "name1_updated";

            list.ForEach(x => x.id = 999);

            Assert.AreEqual("name1_updated", list[0].name);
            Assert.AreEqual(999, list[0].id);
            Assert.AreEqual(a_taken[0], list[0]);
        }
        class a
        {
           public int id { get; set; }
            public string name { get; set; }
        }
        [Test]
        public void linq_test()
        {
            List<a> aa = new List<a>();
            aa.Add(new a { id=1,name="aa" });
            aa.Add(new a { id = 2, name = "bb" });

           var result = from cc in aa 
                         select new a { id = cc.id, name = cc.name };


           List<a> result2 = result.ToList();
           Console.Write(result2[0].name);
            
        }
    }
}
