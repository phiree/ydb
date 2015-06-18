<%@ WebHandler Language="C#" Class="DianzhuApi" %>

using System;
using System.Web;

public class DianzhuApi : IHttpHandler
{

    //-----------------------试卷--------------------------
    /// <summary>
    /// 获取试卷列表
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>


    public bool IsReusable
    {
        get { throw new NotImplementedException(); }
    }

    public void ProcessRequest(HttpContext context)
    {
        
    }
    private void BuildAPIRequest(HttpRequest request)
    { 
    }
}