using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dianzhu.Web.Log.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Web.Security;
using Ydb.Common.Infrastructure;

namespace Dianzhu.Web.Log.Controllers
{
    public class LogController : Controller
    {
        private readonly log4netDB db = new log4netDB(Bootstrap.Container.Resolve<IEncryptService>(), System.Configuration.ConfigurationManager
               .ConnectionStrings["MongoDB"].ConnectionString);
        // GET: Log
        [Authorize]
        public ActionResult Index(string logger,string level,string classname,string domain,string message,string begintime,string endtime)
        {
            ViewBag.Title = "Dianzhu.Web.Log";
            var logs = db.logs;

            var buildersFilter = Builders<log>.Filter;
            var filter = buildersFilter.Empty;
            if (!string.IsNullOrEmpty(logger))
            {
                //filter=filter& buildersFilter.Eq("logger", logger);
                filter = filter & buildersFilter.Regex("logger", "/" + logger + "/i" );
            }
            if (!string.IsNullOrEmpty(level)&& level!= "Level")
            {
                filter = filter & buildersFilter.Eq("level", level);
            }
            if (!string.IsNullOrEmpty(classname))
            {
                filter = filter & buildersFilter.Regex("classname", "/" + classname + "/i");
            }
            if (!string.IsNullOrEmpty(domain))
            {
                filter = filter & buildersFilter.Regex("domain", "/" + domain + "/i");
            }
            if (!string.IsNullOrEmpty(message))
            {
                filter = filter & buildersFilter.Regex("message", "/"+ message + "/i");
            }
            if (!string.IsNullOrEmpty(begintime))
            {
                filter = filter & buildersFilter.Gte("date", begintime);
            }
            if (!string.IsNullOrEmpty(endtime))
            {
                filter = filter & buildersFilter.Lte("date", endtime);
            }
            var sort = Builders<log>.Sort.Descending("date");
            var logList = logs.Find(filter).Sort(sort).Limit(100).ToListAsync().Result;
            return View(logList);

            //IList<log> logList=logs.Aggregate()
            //.Group(r => new { logger = r.logger }, g =>
            //new {
            //    Key = g.Key,
            //    maxThread = g.Max(x => x.thread),//.Select(x => x.thread),
            //    maxValue = g.Max(x => x.linenumber),
            //    maxDate = g.Max(x => x.date)
            //})
            //.Project(r => new log()
            //{
            //    logger = r.Key.logger,
            //    thread = r.maxThread,
            //    linenumber = r.maxValue,
            //    date = r.maxDate

            //})
            //.ToListAsync().Result;



            //string mapFunction = @"function(){  
            //                        emit(this.logger, this.level);  
            //                    };";

            //string reduceFunction = @"function(logger, level){  
            //                        var total = 0;  
            //                        total = Array.sum(level);  
            //                        return { sum: total };  
            //                    };";


            //// Execute map-reduce method  
            //var cusid_prices_results = logs.MapReduce(mapFunction, reduceFunction);




            //var map = new BsonJavaScript(@"function() {
            //var key = { logger: this.logger,
            //level : this.level}
            //emit(key, { date : 0 });}");

            //var reduce = new BsonJavaScript(@"
            //var reduce = function(key, values)
            //{
            //    var logs={};
            //    values.forEach(function(item)
            //    {
            //        if(item.date>date)
            //        {
            //            date=item.date;
            //            logs.Id=item._id;
            //            logs.date=item.date;
            //            logs.thread=item.thread;
            //            logs.level=item.level;
            //            logs.logger=item.logger;
            //            logs.message=item.message;
            //            logs.filename=item.filename;
            //            logs.linenumber=item.linenumber;
            //            logs.classname=item.classname
            //            logs.domain=item.domain;
            //        }
            //    });
            //    return logs;
            //};");

            //var options = new MapReduceOptions<log, log>();
            //options.OutputOptions = MapReduceOutputOptions.Inline;
            //var logList = logs.MapReduce(map, reduce, options);

            //I hate this!!
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

    public class SimpleReduceResult<T>
    {
        //public string Id { get; set; }

        public T value { get; set; }
    }
}