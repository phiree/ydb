﻿<%@ WebHandler Language="C#" Class="TabSelection" %>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
 
using Newtonsoft.Json;
using NPOI.POIFS.Properties;
using Newtonsoft.Json.Serialization;
       using Ydb.BusinessResource.Application;
     using Ydb.BusinessResource.DomainModel;
/// <summary>
/// 用于 tabselecttion 控件的ajax请求.
/// </summary>
public class TabSelection : IHttpHandler,System.Web.SessionState.IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context)
    {
        //权限判断
        if (!AjaxAuth.authAjaxUser(context)){ 
            context.Response.StatusCode = 400;
            context.Response.Clear();
            context.Response.Write("{\"result\":\"" + false + "\",\"msg\":\"unlogin\"}");
            return;
        }

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
            IServiceTypeService typeService= Bootstrap.Container.Resolve<IServiceTypeService>();
         
        IList<ServiceType> list=new List<ServiceType>();
        if (parentId == "0")
        {
            list = typeService.GetTopList();
        }
        else
        {
            ServiceType parentType = typeService.GetOne(new Guid(parentId));
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