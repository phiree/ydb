using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using System.Web.Security;
namespace Dianzhu.AdminBusinessMvc.Controllers
{
    public class BusinessController : Controller
    {
        DZMembershipProvider dzp = new DZMembershipProvider();
        BLLBusiness bllBusiness = new BLLBusiness();
        [ValidateInput(false)]
        [HttpGet]
        public ActionResult Index()
        {

            var member = (BusinessUser)dzp.GetUserByName(User.Identity.Name);
            Business b = member.BelongTo;
            ViewBag.Message = "修改此模板以快速启动你的 ASP.NET MVC 应用程序。";
            return Redirect("/Business/Edit/" + b.Id);
             
        }
        public ActionResult Edit(Guid id)
        {
            Business b = bllBusiness.GetOne(id);
            ViewBag.Message = "修改此模板以快速启动你的 ASP.NET MVC 应用程序。";
 
            return View(b);
        }
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection form)
        {
            Business b = bllBusiness.GetOne(id);
            b.Name = form["Name"];
            b.Address = form["Address"];
            b.Description = form["Description"];
            b.Phone = form["Phone"];
            b.Email = form["Email"];
            b.WorkingYears = int.Parse(form["WorkingYears"]);
            b.Contact = form["Contact"];
            b.StaffAmount =int.Parse( form["StaffAmount"]);
            b.ChargePersonIdCardType = (enum_IDCardType)int.Parse(form["ChargePersonIdCardType"]); ;
            b.ChargePersonIdCardNo = form["ChargePersonIdCardNo"];

            string strRawAddressFromMapAPI = form["RawAddressFromMapAPI"];
            AddressParser addressParser = new AddressParser(strRawAddressFromMapAPI);
            Area area;
            double latitude;
            double longtitude;
            addressParser.ParseAddress(out area, out latitude, out longtitude);
            b.RawAddressFromMapAPI = strRawAddressFromMapAPI;
            b.Longitude = longtitude;
            b.Latitude = latitude;
            b.AreaBelongTo = area;
            bllBusiness.Updte(b);
            ViewBag.SaveMessageSuc = "保存成功";
            return View(b);
        }
        public ActionResult About()
        {
            ViewBag.Message = "你的应用程序说明页。";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "你的联系方式页。";

            return View();
        }
    }
}
