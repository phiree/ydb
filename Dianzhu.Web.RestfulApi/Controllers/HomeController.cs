using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using Dianzhu.Web.RestfulApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Web.Security;
using Ydb.Common.Infrastructure;

namespace Dianzhu.Web.RestfulApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly log4netDB db = new log4netDB(Bootstrap.Container.Resolve<IEncryptService>(), System.Configuration.ConfigurationManager
               .ConnectionStrings["MongoDB"].ConnectionString);
        [Authorize]//(Roles="restful")
        public ActionResult Index()
        {
            ViewBag.Title = "Dianzhu.Web.RestfulApi";
            return View();
        }
        [Authorize]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult ApiInfo(string searchText)
        {
            IList<ApiInfo> apiInfoList = new List<ApiInfo>();
            var apiInfos = db.ApiInfos;//不填"ApiInfos"，则默认为ApiInfos
            searchText = string.IsNullOrEmpty(searchText) ? "" : searchText;
            apiInfoList = apiInfos.Find(a => a.ApiName.Contains(searchText)||a.ApiRoute.Contains(searchText)).ToListAsync().Result;
            //var logs = db.logs;


            ////分组聚合查询快
            //IList<ApiCount> apiCountList = logs.Aggregate()
            //.Match(r => r.logger2 == "Rule.v1.RestfulApi.Web.Dianzhu")
            //.Group(r => new { logger = r.logger }, g =>
            //new
            //{
            //    Key = g.Key,
            //    sumCount = g.Count()
            //})
            //.Project(r => new ApiCount()
            //{
            //    logger = r.Key.logger,
            //    apiCount = r.sumCount
            //})
            //.ToListAsync().Result;

            //foreach (ApiInfo api in apiInfoList)
            //{
            //    int c = 0;
            //    for (int i = 0; i < apiCountList.Count; i++)
            //    {
            //        if (apiCountList[i].logger == "Ydb." + api.ApiRoute + ".Rule.v1.RestfulApi.Web.Dianzhu")
            //        {
            //            var buildersFilter = Builders<ApiInfo>.Filter.Eq("ApiRoute", api.ApiRoute);
            //            var update = Builders<ApiInfo>.Update.Set("ApiRequestNum", apiCountList[i].apiCount);
            //            var result = apiInfos.UpdateOneAsync(buildersFilter, update).Result;
            //            api.ApiRequestNum = apiCountList[i].apiCount;
            //            break;
            //        }
            //        c++;
            //    }
            //    if (c == apiCountList.Count)
            //    {
            //        api.ApiRequestNum = 0;
            //    }
            //}

            //循环去查慢
            //for (int i = 0; i < apiInfoList.Count; i++)
            //{
            //    var buildersFilter = Builders<log>.Filter;
            //    //var filter = buildersFilter.Regex("logger", "/Dianzhu.Web.RestfulApi/") & buildersFilter.Regex("message", "/ApiRoute="+ apiInfoList[i].ApiRoute + "/");
            //    var filter = buildersFilter.Eq("logger", "Ydb." + apiInfoList[i].ApiRoute + ".Rule.v1.RestfulApi.Web.Dianzhu");
            //    //var logCount= logs.CountAsync(a => a.logger.Contains("Dianzhu.Web.RestfulApi") && a.logger.Contains("ApiRoute="+ apiInfoList[i].ApiRoute)).ToListAsync().Result;
            //    var logCount = logs.CountAsync(filter);
            //    apiInfoList[i].ApiRequestNum = logCount.Result;
            //}
            return View(apiInfoList.OrderByDescending(s=>s.ApiRequestNum).ToList());
        }
        [Authorize]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult ApiCount(string apiRoute)
        {
            //var logs = db.logs;
            //var buildersFilter = Builders<log>.Filter;
            ////var filter = buildersFilter.Regex("logger", "/Dianzhu.Web.RestfulApi/") & buildersFilter.Regex("message", "/ApiRoute=" + apiRoute + "/"); 
            //var filter = buildersFilter.Eq("logger", "Ydb."+apiRoute + ".Rule.v1.RestfulApi.Web.Dianzhu");
            //var logCount = logs.CountAsync(filter).Result;

            var apiInfos = db.ApiInfos;
            var buildersFilter = Builders<ApiInfo>.Filter;
            var filter = buildersFilter.Eq("ApiRoute", apiRoute);
            IList<ApiInfo> apiInfoList = apiInfos.Find(filter).ToListAsync().Result;
            if (apiInfoList.Count > 0)
            {
                return Content(apiInfoList[0].ApiRequestNum.ToString());
            }
            else
            {
                return Content("0");
            }
        }
        [Authorize]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult AddApi()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddApi(ApiInfo _ApiInfo)
        {
            if (ModelState.IsValid)
            {
                db.ApiInfos.InsertOneAsync(_ApiInfo);
                return RedirectToAction("ApiInfo");
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteApi(string Id)
        {
            var filter = Builders<ApiInfo>.Filter.Eq("Id", Id);
            var result = db.ApiInfos.DeleteOneAsync(filter).Result;
            return RedirectToAction("ApiInfo");
        }

        [Authorize]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult LogList(string apiRoute,string searchText,string beginTime,string endTime)
        {
            IList<log> logList = new List<log>();
            var logs = db.logs;//不填"ApiInfos"，则默认为ApiInfos
            var buildersFilter = Builders<log>.Filter;
            //var filter = buildersFilter.Regex("logger", "/Dianzhu.Web.RestfulApi/") & buildersFilter.Regex("message", "/ApiRoute=" + apiRoute + "/") & buildersFilter.Regex("message", "/" + searchText + "/");
            string strLogger = "logger2";

            //DateTime dtBeginTime = new DateTime();
            //DateTime dtEndTime = new DateTime();
            //db.posts.find({ created_on: {$gte: start, $lt: end} });
            var filter= buildersFilter.Eq("logger1", "v1.RestfulApi.Web.Dianzhu");
            if (apiRoute == "Unauthorized")
            {
                filter = filter& buildersFilter.Regex("message", "/用户认证失败/");
            }
            else
            {
                if (string.IsNullOrEmpty(apiRoute))
                {
                    strLogger = "logger2";
                    apiRoute = "Rule.v1.RestfulApi.Web.Dianzhu";
                }
                else
                {
                    strLogger = "logger";
                    apiRoute = "Ydb." + apiRoute + ".Rule.v1.RestfulApi.Web.Dianzhu";
                }
                filter = filter & buildersFilter.Eq(strLogger, apiRoute) & buildersFilter.Regex("message", "/" + searchText + "/");
            }
            //if (DateTime.TryParse(beginTime, out dtBeginTime))
            //{
            //    filter = filter & buildersFilter.Gte("date", dtBeginTime);
            //}
            //if (DateTime.TryParse(endTime, out dtEndTime))
            //{
            //    filter = filter & buildersFilter.Lte("date", dtEndTime);
            //}
            if (!string.IsNullOrEmpty(beginTime))
            {
                filter = filter & buildersFilter.Gte("date", beginTime);
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                filter = filter & buildersFilter.Lte("date", endTime);
            }
            var sort = Builders<log>.Sort.Descending("date");
            logList = logs.Find(filter).Sort(sort).Limit(30).ToListAsync().Result;
            return View(logList);
        }
        [Authorize]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult LogInfo(string apiTime)
        {
            IList<log> logList = new List<log>();
            var logs = db.logs;//不填"ApiInfos"，则默认为ApiInfos
            var buildersFilter = Builders<log>.Filter;
            var filter = buildersFilter.Eq("logger1", "v1.RestfulApi.Web.Dianzhu") & buildersFilter.Regex("message", "/" + apiTime + "/");
            var sort = Builders<log>.Sort.Ascending("date");
            logList = logs.Find(filter).Sort(sort).Limit(100).ToListAsync().Result;
            return View(logList);
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // GET: Login
        public ActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(user p_user, string returnUrl)
        {
            IList<user> userList = new List<user>();
            var users = db.users;
            var buildersFilter = Builders<user>.Filter;
            var filter = buildersFilter.Eq("username", p_user.username) & buildersFilter.Eq("password", p_user.password);
            userList = users.Find(filter).ToListAsync().Result;
            if (userList != null && userList.Count > 0)
            {
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                    1,
                    "login",
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    false,
                    userList[0].username
                    );
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                System.Web.HttpCookie authCookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);

                //测试System.Runtime.Caching.MemoryCache，能不能夸项目
                //if (!System.Runtime.Caching.MemoryCache.Default.Contains(userList[0].Id.ToString()))
                //    System.Runtime.Caching.MemoryCache.Default.Add(userList[0].Id.ToString(), "缓存是时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DateTimeOffset.UtcNow.AddSeconds(172800));
                //string str = System.Runtime.Caching.MemoryCache.Default[userList[0].Id.ToString()].ToString();
                //return Content(str);
                if (returnUrl == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction(returnUrl);
                }

            }
            else
            {
                return RedirectToAction("Error");
            }
            //Response.Redirect("~/");
            //return RedirectToAction("Index");
        }
    }
}
