using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
/// <summary>
 ///orm接口 公用的类
/// </summary>

public class RespDataMERM_merObj
{
    public string userID { get; set; }
    public string alias { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string imgUrl { get; set; }
 
    public RespDataMERM_merObj Adapt(DZMembership membership)
    {
        this.userID = membership.Id.ToString();
        this.alias = membership.NickName ?? "";
        this.email = membership.Email ?? "";
        this.phone = membership.Phone ?? "";
        this.imgUrl =string.IsNullOrEmpty( membership.AvatarUrl)?string.Empty
                :( 
                  System.Configuration.ConfigurationManager.AppSettings["MediaGetUrl"]
                +membership.AvatarUrl);
       
        return this;

    }
}

public class RespDataMERM// 
{
    public RespDataMERM_merObj merObj { get; set; }
}

