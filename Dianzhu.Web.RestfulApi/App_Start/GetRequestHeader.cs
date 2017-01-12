using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianzhu.ApplicationService;
using System.Net.Http;
using System.Configuration;

//System.Net.Http  System.Web.Http;

namespace Dianzhu.Web.RestfulApi
{
    public class GetRequestHeader
    {
        public static Customer GetTraitHeaders(string apiUrl)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("Ydb."+apiUrl +".Rule.v1.RestfulApi.Web.Dianzhu");
            common_Trait_Headers headers = new common_Trait_Headers();
            HttpRequest req = HttpContext.Current.Request;
            headers.appName = req.Headers.GetValues("appName").FirstOrDefault();
            headers.token = req.Headers.GetValues("token").FirstOrDefault();
            var allowedOrigin = req.GetOwinContext().Get<string>("as:RequestMethodUriSign");
            //headers.sign = req.Headers.GetValues("sign").FirstOrDefault();
            headers.stamp_TIMES = req.Headers.GetValues("stamp_TIMES").FirstOrDefault();
            MySectionCollection mysection = (MySectionCollection)ConfigurationManager.GetSection("MySectionCollection");
            //string apiKey = mysection.KeyValues[headers.appName].Value;
            headers.apiKey = mysection.KeyValues[headers.appName].Value;

            //接口权限判断
            Customer customer = new Customer();
            customer = customer.getCustomer(headers.token, headers.apiKey, false);
            log.Info("Info(UserInfo)" + headers.stamp_TIMES + ":ApiRoute=" + apiUrl + ",UserName=" + customer.loginName+",UserId="+ customer.UserID + ",UserType=" + customer.UserType + ",RequestMethodUriSign=" + allowedOrigin.ToString ());
            string strRule = ConfigurationManager.AppSettings[apiUrl].ToString();
            //log.Info("Info(Route)" + headers.stamp_TIMES + ":" + apiUrl);
            if (strRule=="" || strRule.Contains("[" + customer.UserType + "]"))
            {
                return customer;
            }
            else
            {
                throw new Exception("没有此接口的访问权限！");
            }
            //switch (apiUrl)
            //{
            //    case "get/snapshots/{serviceID}":
            //        if (customer.UserType != "business")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/citys":
            //        break;
            //    case "get/citys/{code}":
            //        break;
            //    case "post/complaints":
            //        if (customer.UserType != "customer")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/complaints":
            //        break;
            //    case "get/complaints/count":
            //        break;
            //    case "get/complaints/{complaintID}":
            //        break;
            //    case "get/ads":
            //        break;
            //    case "post/apps/{appUUID}":
            //        break;
            //    case "delete/apps/{appUUID}":
            //        break;
            //    case "patch/apps/{appUUID}":
            //        break;
            //    case "get/reminds":
            //        if (customer.UserType != "customer")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/reminds/count":
            //        if (customer.UserType != "customer")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/reminds/{remindID}":
            //        if (customer.UserType != "customer")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "delete/reminds/{remindID}":
            //        if (customer.UserType != "customer")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "post/assigns":
            //        if (customer.UserType != "business")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/assigns":
            //        if (customer.UserType != "business")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/assigns/count":
            //        if (customer.UserType != "business")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "post/stores/{storeID}/services/{serviceID}/workTimes":
            //        if (customer.UserType != "business")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/stores/{storeID}/services/{serviceID}/workTimes":
            //        break;
            //    case "get/stores/{storeID}/services/{serviceID}/workTimes/count":
            //        break;
            //    case "get/stores/{storeID}/services/{serviceID}/workTimes/{workTimeID}":
            //        if (customer.UserType != "business")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "patch/stores/{storeID}/services/{serviceID}/workTimes/{workTimeID}":
            //        if (customer.UserType != "business")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "delete/stores/{storeID}/services/{serviceID}/workTimes/{workTimeID}":
            //        if (customer.UserType != "business")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/allServiceTypes":
            //        break;
            //    case "post/stores/{storeID}/services":
            //        if (customer.UserType != "business")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/stores/{storeID}/services":
            //        break;
            //    case "get/stores/{storeID}/services/count":
            //        break;
            //    case "get/stores/{storeID}/services/{serviceID}":
            //        break;
            //    case "patch/stores/{storeID}/services/{serviceID}":
            //        if (customer.UserType != "business")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "delete/stores/{storeID}/services/{serviceID}":
            //        if (customer.UserType != "business")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "post/stores/{storeID}/staffs":
            //        if (customer.UserType != "business")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/stores/{storeID}/staffs":
            //        if (customer.UserType != "business" && customer.UserType != "staff")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/stores/{storeID}/staffs/count":
            //        if (customer.UserType != "business" && customer.UserType != "staff")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/stores/{storeID}/staffs/{staffID}":
            //        break;
            //    case "patch/stores/{storeID}/staffs/{staffID}":
            //        if (customer.UserType != "business" && customer.UserType != "staff")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "delete/stores/{storeID}/staffs/{staffID}":
            //        if (customer.UserType != "business")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/allStores":
            //        break;
            //    case "get/allStores/count":
            //        break;
            //    case "post/stores":
            //        if (customer.UserType != "business")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/stores":
            //        if (customer.UserType != "business")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/stores/count":
            //        if (customer.UserType != "business")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/stores/{storeID}":
            //        break;
            //    case "patch/stores/{storeID}":
            //        if (customer.UserType != "business")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "delete/stores/{storeID}":
            //        if (customer.UserType != "business")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/orders/{orderID}/pays":
            //        if (customer.UserType != "customer")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/orders/{orderID}/pays/count":
            //        if (customer.UserType != "customer")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/orders/{orderID}/pays/{payID}":
            //        if (customer.UserType != "customer")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "patch/orders/{orderID}/pays/{payID}":
            //        if (customer.UserType != "customer")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/orders/{orderID}/pays/{payID}/pay3rdString":
            //        if (customer.UserType != "customer")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/orders/{orderID}/chats":
            //        break;
            //    case "get/orders/{orderID}/chats/count":
            //        break;
            //    case "get/orders":
            //        if (customer.UserType != "customer" && customer.UserType != "business")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/orders/count":
            //        if (customer.UserType != "customer" && customer.UserType != "business")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/orders/{orderID}":
            //        break;
            //    case "get/orders/{orderID}/allStatusList":
            //        break;
            //    case "patch/orders/{orderID}":
            //        if (customer.UserType != "customerservice" && customer.UserType != "business" && customer.UserType != "staff")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/orders/{orderID}/pushServices":
            //        if (customer.UserType != "customerservice" && customer.UserType != "customer")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "put/orders/{orderID}/confirmService":
            //        if (customer.UserType != "customer")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "put/orders/{orderID}/appraise":
            //        if (customer.UserType != "customer")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/orders/{orderID}/linkMan":
            //        if (customer.UserType != "customer")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "patch/orders/{orderID}/currentStatus":
            //        if (customer.UserType != "customer" && customer.UserType != "business" && customer.UserType != "staff")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/orders/{orderID}/refunds":
            //        break;
            //    case "post/orders/{orderID}/refunds":
            //        if (customer.UserType != "customer" && customer.UserType != "business" && customer.UserType != "staff")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "get/orders/{orderID}/forman":
            //        break;
            //    case "patch/orders/{orderID}/forman":
            //        if (customer.UserType != "business")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "post/customers":
            //        break;
            //    case "get/customers":
            //        break;
            //    case "post/customer3rds":
            //        break;
            //    case "get/customers/count":
            //        break;
            //    case "get/customers/{customerID}":
            //        break;
            //    case "patch/customers/{customerID}":
            //        if (customer.UserType != "customer")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "patch/customers/{customerID}/currentGeolocation":
            //        break;
            //    case "post/merchants":
            //        break;
            //    case "get/merchants":
            //        break;
            //    case "get/merchants/count":
            //        break;
            //    case "get/merchants/{merchantID}":
            //        break;
            //    case "patch/merchants/{merchantID}":
            //        if (customer.UserType != "business")
            //        {
            //            throw new Exception("没有此接口的访问权限！");
            //        }
            //        break;
            //    case "post/customerServices":
            //        break;
            //    case "get/customerServices":
            //        break;
            //    case "get/storages/images":
            //        break;
            //    case "get/storages/avatarImages":
            //        break;
            //    case "get/storages/audios":
            //        break;
            //}
            return customer;
        }
    }
}