using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// RestfulModel 的摘要说明
/// </summary>
public class RestfulModel
{
    public RestfulModel()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 请求路径
    /// </summary>
    /// <type>string</type>
    public string apiurl { get; set; }

    /// <summary>
    /// 请求方式
    /// </summary>
    /// <type>string</type>
    public string method { get; set; }

    /// <summary>
    /// 请求参数
    /// </summary>
    /// <type>string</type>
    public string content { get; set; }

    /// <summary>
    /// 请求 token
    /// </summary>
    /// <type>string</type>
    public string token { get; set; }

    
}