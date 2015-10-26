<%@ WebHandler Language="C#" Class="GetCustomerService" %>

using System;
using System.Web;

public class GetCustomerService : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");
        //get customer, orderid
        string pCustomerLoginId = context.Request["customerId"];
        string pOrderId = context.Request["orderId"];
        //get cslist, using xmpp

        //get order or create new order
        // return cs,and order 

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}