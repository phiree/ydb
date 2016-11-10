using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dianzhu.Web.RestfulApi.Controllers
{
    public class VersionController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            string v= System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            DateTime d = new DateTime(2000, 1, 1);
            //Console.WriteLine(d.AddDays(3125).AddSeconds(14653 * 2).ToString("yyyy/MM/dd HH:mm:ss")); 
            string[] str = v.Split('.');
            string t = d.AddDays(int.Parse (str[2])).AddSeconds(int.Parse(str[3]) * 2).ToString("yyyy/MM/dd HH:mm:ss");
#if dev
             string s = "1";
#else
            string s = "0";
#endif
            return new string[] { s+"版本号："+v, "发布时间："+t };
        }


        [Route("api/v1/getOutTime")]
        public IEnumerable<string> GetTest()
        {
            System.Threading.Thread.Sleep(600000);
            return new string[] { "测试时长：10分钟" };
        }

        // GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //    //return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        //}

        // POST api/values
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/values/5
        //public void Delete(int id)
        //{

        //}
    }
}
