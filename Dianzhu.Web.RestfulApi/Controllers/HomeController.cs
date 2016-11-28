using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using Dianzhu.Web.RestfulApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Dianzhu.Web.RestfulApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly log4netDB db = new log4netDB();
        public ActionResult Index()
        {
            ViewBag.Title = "Dianzhu.Web.RestfulApi";
            return View();
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult ApiInfo(string searchText)
        {
            IList<ApiInfo> apiInfoList = new List<ApiInfo>();
            var apiInfos = db.ApiInfos;//不填"ApiInfos"，则默认为ApiInfos
            searchText = string.IsNullOrEmpty(searchText) ? "" : searchText;
            apiInfoList = apiInfos.Find(a => a.ApiName.Contains(searchText)||a.ApiRoute.Contains(searchText)).ToListAsync().Result;
            var logs = db.logs;
            for (int i = 0; i < apiInfoList.Count; i++)
            {
                var buildersFilter = Builders<log>.Filter;
                var filter = buildersFilter.Regex("logger", "/Dianzhu.Web.RestfulApi/") & buildersFilter.Regex("message", "/ApiRoute="+ apiInfoList[i].ApiRoute + "/");
                //var logCount= logs.CountAsync(a => a.logger.Contains("Dianzhu.Web.RestfulApi") && a.logger.Contains("ApiRoute="+ apiInfoList[i].ApiRoute)).ToListAsync().Result;
                var logCount = logs.CountAsync(filter);
                apiInfoList[i].ApiRequstNum = logCount.Result.ToString();
            }
            
            return View(apiInfoList.OrderByDescending(s=>s.ApiRequstNum).ToList());
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult ApiCount(string apiRoute)
        {
            var logs = db.logs;
            var buildersFilter = Builders<log>.Filter;
            var filter = buildersFilter.Regex("logger", "/Dianzhu.Web.RestfulApi/") & buildersFilter.Regex("message", "/ApiRoute=" + apiRoute + "/");
            var logCount = logs.CountAsync(filter).Result;
            return Content(logCount.ToString());
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult AddApi()
        {
            return View();
        }

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


        [HttpPost]
        public ActionResult DeleteApi(string Id)
        {
            var filter = Builders<ApiInfo>.Filter.Eq("Id", Id);
            var result = db.ApiInfos.DeleteOneAsync(filter).Result;
            return RedirectToAction("ApiInfo");
        }


        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult LogList(string apiRoute,string searchText)
        {
            IList<log> logList = new List<log>();
            var logs = db.logs;//不填"ApiInfos"，则默认为ApiInfos
            var buildersFilter = Builders<log>.Filter;
            var filter = buildersFilter.Regex("logger", "/Dianzhu.Web.RestfulApi/") & buildersFilter.Regex("message", "/ApiRoute=" + apiRoute + "/") & buildersFilter.Regex("message", "/" + searchText + "/");
            var sort = Builders<log>.Sort.Descending("date");
            logList = logs.Find(filter).Sort(sort).Limit(100).ToListAsync().Result;
            return View(logList);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult LogInfo(string apiTime)
        {
            IList<log> logList = new List<log>();
            var logs = db.logs;//不填"ApiInfos"，则默认为ApiInfos
            var buildersFilter = Builders<log>.Filter;
            var filter = buildersFilter.Regex("logger", "/Dianzhu.Web.RestfulApi/") & buildersFilter.Regex("message", "/" + apiTime + "/");
            var sort = Builders<log>.Sort.Ascending("date");
            logList = logs.Find(filter).Sort(sort).Limit(100).ToListAsync().Result;
            return View(logList);
        }
    }
}
