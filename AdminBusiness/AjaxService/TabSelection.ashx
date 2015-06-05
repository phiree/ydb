<%@ WebHandler Language="C#" Class="TabSelection" %>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using Dianzhu.Model;
using Dianzhu.BLL;
using Newtonsoft.Json;
using NPOI.POIFS.Properties;
using Newtonsoft.Json.Serialization;
/// <summary>
/// 用于 tabselecttion 控件的ajax请求.
/// </summary>
public class TabSelection : IHttpHandler {
    
    public void ProcessRequest (HttpContext context)
    {

      string type=  context.Request.Params["type"];
        string id = context.Request.Params["id"];
        string result=String.Empty;
        
        switch (type)
        {
            case "servicetype":
                result = GetServiceTypeList(id);
                break;
            case "area":
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.Write(result);
    }

    private string  GetServiceTypeList(string parentId)
    {
        BLLServiceType bll=new BLLServiceType();
        IList<ServiceType> list=new List<ServiceType>();
        if (parentId == "0")
        {
            list = bll.GetTopList();
        }
        else
        {
            ServiceType parentType = bll.GetOne(new Guid(parentId));
            list = parentType.Children;
        }

        string result = JsonConvert.SerializeObject(list);
        return result;
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}