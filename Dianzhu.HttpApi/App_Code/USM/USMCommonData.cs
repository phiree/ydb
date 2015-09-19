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
using System.Configuration;
/// <summary>
 ///orm接口 公用的类
/// </summary>

public class RespDataUSM_userObj
{
    public string userID { get; set; }
    public string alias { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string imgUrl { get; set; }
    public string address { get; set; }
    public RespDataUSM_userObj Adapt(DZMembership membership)
    {
        this.userID = membership.Id.ToString();
        this.alias = membership.NickName ?? "";
        this.email = membership.Email ?? "";
        this.phone = membership.Phone ?? "";
        this.imgUrl = membership.AvatarUrl == null 
            ? string.Empty 
            : ConfigurationManager.AppSettings["media_server"] + "imagehandler.ashx?imagename="
                + membership.AvatarUrl;
        this.address =membership.Address?? "";
        return this;

    }
}
public class ReqDataUSM 
{
    public string email { get; set; }
    public string phone { get; set; }
    public string pWord { get; set; }

}
public class RespDataUSM// 
{
    public RespDataUSM_userObj userObj { get; set; }
}

